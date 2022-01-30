using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField, Tooltip("The character that can pickup this key")]
    private CharacterType characterType = 0;

    [SerializeField, Tooltip("The door linked with this key")]
    private DoorManager door = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (door)
        {
            if (collision.TryGetComponent<ControllableCharacter>(out ControllableCharacter character))
            {
                if (characterType == character.GetCharacterType())
                {
                    door.Unlock(characterType);

                    if (AudioHandler.Instance != null)
                    {
                        AudioHandler.Instance.CollectKey();
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}
