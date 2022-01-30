using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private bool buffKey = false;
    private bool smolKey = false;

    [SerializeField]
    private string sceneToLoad = default;

    public void OnEnterDoor(CharacterType characterType)
    {
        Debug.Log(characterType.ToString() + " entered door");

        //TODO
        //Handle endgame event
        CheckBothEntered();
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
        if(buffKey && smolKey)
		{
            SceneHandler.Instance.LoadNewScene(sceneToLoad);
		}
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (buffKey && smolKey)
        {
            if (collision.TryGetComponent<ControllableCharacter>(out ControllableCharacter character))
            {
                OnEnterDoor(character.GetCharacterType());
            }
        }
    }
}
