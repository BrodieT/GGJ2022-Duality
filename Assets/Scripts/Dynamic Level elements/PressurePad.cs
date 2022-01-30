using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField, Tooltip("The moving platform this pad is connected to")]
    private MovablePlatform platform = default;

    private int currentWeight = 0; // How much weight is currently on the pressure pad

    [SerializeField]
    private AudioSource source = default;

    [SerializeField]
    private AudioClip clip = default;

	private void Start()
	{
        source.clip = clip;
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Entity>(out Entity entity))
        {
            currentWeight += entity.weight;

            if (platform)
            {
                platform.UpdateHeight(currentWeight);
            }

            source.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out Entity entity))
        {
            currentWeight -= entity.weight;

            if (platform)
            {
                platform.UpdateHeight(currentWeight);
            }

            source.Play();
        }
    }
}
