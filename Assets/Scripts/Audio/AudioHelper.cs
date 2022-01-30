using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [SerializeField]
    private AudioSource source = default;

    [SerializeField]
    private AudioClip clip = default;

    [SerializeField]
    private bool randomPitch = false;

    [SerializeField]
    private float lowerPitch = 0;

    [SerializeField]
    private float higherPitch = 0;
    // Start is called before the first frame update
    void Start()
    {
        source.clip = clip;
    }

    public void PlayClip()
	{
        if (UIHandler.Instance.GetCharacterMovement())
        {
            if (randomPitch)
            {
                source.pitch = Random.Range(lowerPitch, higherPitch);
            }

            source.Play();
        }
	}
}
