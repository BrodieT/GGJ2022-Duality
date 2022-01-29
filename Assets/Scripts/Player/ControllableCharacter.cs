using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ControllableCharacter : MonoBehaviour
{
    [SerializeField, Tooltip("The settings used for controlling this character")]
    private CharacterSettings settings = default;

    [SerializeField, Tooltip("The animator used for this character")]
    private Animator anim = default;

    [SerializeField, Tooltip("The audio source used for playing character SFX")]
    private AudioSource audioSource = default;

    private Rigidbody2D rb = default; //Local cache of rigidbody component
    private Collider2D col = default; //Local cache of collider component
    private SpriteRenderer[] sprites;


    private float groundedMemoryTimer = 0.0f; //The timer tracking whether the player was recently grounded
    private float jumpPressMemoryTimer = 0.0f; //The timer tracking if the jump button was recently pressed

    private bool isGrounded = false; //Tracks whether the character is touching the ground or not
    private bool prevIsGrounded = false; //Remembers the grounded state from the previous frame
    private bool isJumping = false; //Tracks whether the character is in the process of a jump - determines the difference between a jump & walking off a ledge

    private Vector2 externalForce = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        //Check the settings have been assigned and throw an error if it has not
        if(!settings)
        {
            Debug.LogError("No Character Settings assigned to this character", this);
        }

        //Cache the rigidbody and colliders - safe to use GetComponent as they are required by this script
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();

        rb.gravityScale = settings.gravityScale;
    }

  

    private void Update()
    {

        //Decrement the two memory timers for the jump
        jumpPressMemoryTimer -= Time.deltaTime;
        groundedMemoryTimer -= Time.deltaTime;

        //Update the grounded status
        GroundCheck();
    }

    public void JumpButtonPressed()
    {
        jumpPressMemoryTimer = settings.jumpPressRememberTime;
    }




    private void GroundCheck()
    {
        //The character is considered grounded if their feet are touching the ground
        //OR the memory timer has not yet elapsed
        isGrounded = IsGrounded();

        //Update the ground check timer and if the character has just grounded then call Onland
        if (isGrounded)
        {
            groundedMemoryTimer = settings.groundCheckRememberTime;

            if (!prevIsGrounded)
                OnLand();
        }
        //If the character is not grounded but was previously then call OnExitGround
        else if (prevIsGrounded)
        {
            OnExitGround();
        }

        //Update the previously grounded flag
        prevIsGrounded = isGrounded;
    }

    //This function is called when the character exits the ground (either via jumping or falling)
    private void OnExitGround()
    {
        //Play the appropriate animations for when the character exits the ground
        if (anim)
        {
            if (settings.landAnim != "")
            {
                anim.ResetTrigger(settings.landAnim);
            }

            if (!isJumping && settings.fallAnim != "")
            {
                anim.SetTrigger("IsFalling");
            }
        }
    }

    //This function is called when the character lands after falling
    private void OnLand()
    {
        //Update the animations accordingly where appropriate
        if (anim)
        {
            if (settings.landAnim != "")
            {
                anim.SetTrigger(settings.landAnim);
            }

            if (settings.fallAnim != "")
            {
                anim.ResetTrigger(settings.fallAnim);
            }
        }

        //Play the landing sound effect if possible
        if (audioSource && settings.landSFX)
        {
            audioSource.PlayOneShot(settings.landSFX);
        }

        //Reset the is jumping flag
        isJumping = false;
    }

    //This function performs the jump itself
    public void DoJump()
    {
        //If the player was recently grounded AND recently pressed the jump key, then perform a jump
        if (groundedMemoryTimer >= 0.0f && jumpPressMemoryTimer > 0.0f)
        {
            isJumping = true;

            //Reset the timers
            groundedMemoryTimer = 0.0f;
            jumpPressMemoryTimer = 0.0f;


            //Play the animation if applicable
            if (anim && settings.jumpAnim != "")
            {
                anim.SetTrigger(settings.jumpAnim);
            }

            //Update the velocity
            rb.velocity = new Vector2(rb.velocity.x, settings.jumpAmount);
        }
    }

    //This function determines if the character is grounded or not
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + settings.feetPosition, settings.groundCheckRadius, settings.groundLayer);
    }

    //This function handles the horizontal movement for the character
    public void MoveX(int direction = 0)
    {
        //If we can animate this character with a walk animation
        if(anim && settings.walkAnim != "")
        {
            //Set the walk param accordingly 
            anim.SetBool(settings.walkAnim, direction != 0);
        }

        //if moving then update the sprites facing direction
        //otherwise leave them facing the last move direction
        if (direction != 0)
        {
            FlipSprites((settings.flipSprites && direction < 0) ||
                (!settings.flipSprites && direction > 0));
        }

        //Move the character
        rb.velocity = new Vector2(direction * settings.characterMoveSpeed, rb.velocity.y) + externalForce;
        externalForce = Vector2.zero;
    }

    public void ApplyForce(Vector2 force)
    {
        externalForce = force;
    }

    private void FlipSprites(bool flip)
    {
        if(sprites.Length > 0)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.flipX = flip;
            }
        }
    }

    //This function draws debug gizmos for this character
    private void OnDrawGizmos()
    {
        //Show the grounded detection circle and change colour accordingly at runtime
        Gizmos.color = Color.red;
        if (Application.isPlaying)
            Gizmos.color = IsGrounded() ? Color.green : Color.red;

        if (settings)
            Gizmos.DrawWireSphere(transform.position + settings.feetPosition, settings.groundCheckRadius);
    }


    public CharacterType GetCharacterType()
    {
        return settings.characterType;
    }
}
