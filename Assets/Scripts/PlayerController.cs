using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ControllableCharacter character1 = default;

    private int moveDirection = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (character1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                character1.JumpButtonPressed();
            }

            float dir = Input.GetAxisRaw("Horizontal");
           moveDirection = dir > 0.0f ? 1 : (dir < 0.0f ? -1 : 0);
        }
    }

    private void FixedUpdate()
    {
        character1.MoveX(moveDirection);
        character1.DoJump();
    }


}
