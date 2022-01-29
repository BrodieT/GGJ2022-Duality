using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("A list of all characters that can be controlled by the player. Will switch characters round robin.")]
    private List<ControllableCharacter> controllableCharacters = default;

 
    private int moveDirection = 0;
    private int currentCharacter = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controllableCharacters.Count > 0 && controllableCharacters.Count > currentCharacter)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                controllableCharacters[currentCharacter].JumpButtonPressed();
            }

            float dir = Input.GetAxisRaw("Horizontal");
           moveDirection = dir > 0.0f ? 1 : (dir < 0.0f ? -1 : 0);
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCharacter();
        }
    }

    private void FixedUpdate()
    {
        if (controllableCharacters.Count > 0 && controllableCharacters.Count > currentCharacter)
        {
            controllableCharacters[currentCharacter].MoveX(moveDirection);
            controllableCharacters[currentCharacter].DoJump();
        }
    }

    private void SwitchCharacter()
    {
        currentCharacter++;
        
        if(currentCharacter >= controllableCharacters.Count)
        {
            currentCharacter = 0;
        }
    }


}
