using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private bool buffKey = false;
    private bool smolKey = false;

    [SerializeField]
    private string sceneToLoad = default;


    private bool buffGuy = false;
    private bool smolGuy = false;

    public void OnEnterDoor(CharacterType characterType)
    {
        Debug.Log(characterType.ToString() + " entered door");

        if (characterType == CharacterType.Buff)
            buffGuy = true;
        else if (characterType == CharacterType.Smol)
            smolGuy = true;

        //TODO
        //Handle endgame event
        CheckBothEntered();
    }

    public void OnExitDoor(CharacterType characterType)
    {
        Debug.Log(characterType.ToString() + " entered door");

        if (characterType == CharacterType.Buff)
            buffGuy = false;
        else if (characterType == CharacterType.Smol)
            smolGuy = false;
    }


    public void Unlock(CharacterType keyType)
    {
        if (keyType == CharacterType.Buff)
            buffKey = true;
        else if (keyType == CharacterType.Smol)
            smolKey = true;
    }

    void CheckBothEntered()
	{
        if(buffKey && smolKey && buffGuy && smolGuy)
		{
            SceneHandler.Instance.LoadNewScene(sceneToLoad);
		}
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ControllableCharacter>(out ControllableCharacter character))
        {
            OnEnterDoor(character.GetCharacterType());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ControllableCharacter>(out ControllableCharacter character))
        {
            OnExitDoor(character.GetCharacterType());
        }
    }
}
