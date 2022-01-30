using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBox : MonoBehaviour
{
    [SerializeField]
    private AudioSource source = default;

    [SerializeField]
    private AudioClip clip = default;

    [SerializeField]
    private Entity entity = default;

    [SerializeField]
    private Rigidbody2D rb = default;

    private bool isPushing = false;

    private Animator anim = default;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity == Vector2.zero && !isPushing)
		{
            StopPushingSound();
		}
    }

    public void PlayPushingSound()
    {
        Debug.Log("PLAYING");

        source.loop = true;
        source.clip = clip;
        source.Play();
    }

    public void StopPushingSound()
    {
        source.Stop();
        if(anim != null)
		{
            ChangePushingAnim(false);
        }
    }

    public void AllowPushing(bool set)
    {
        if (set)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;


            PlayPushingSound();
        }
		else
		{
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}

    }

    void ChangePushingAnim(bool set)
	{
        Debug.Log("CALLED");
        anim.SetBool("Push", set);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        ControllableCharacter character = default;
        collision.transform.TryGetComponent<ControllableCharacter>(out character);

        if (character != null)
        {
            if (character.GetIfMoving())
            {
                if (character.GetSettings().weightThreshold >= entity.weight && !isPushing)
                {
                    if(anim == null)
					{
                        anim = character.GetComponentInChildren<Animator>();
					}

                    AllowPushing(true);
                    ChangePushingAnim(true);
                    isPushing = true;
                }
                else
                {
                    AllowPushing(false);
                    isPushing = false;
                }
            }
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        ControllableCharacter character = default;
        collision.transform.TryGetComponent<ControllableCharacter>(out character);

        if (character != null)
        {
            if (character.GetIfMoving())
            {
                StopPushingSound();
            }
        }
    }
	
}
