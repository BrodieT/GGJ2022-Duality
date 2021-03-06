using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwapParticles : MonoBehaviour
{
    public static CharacterSwapParticles Instance = default;

    [SerializeField]
    private ParticleSystem particleEffect = default;

    [SerializeField]
    private float speed = 2f;

    private Transform currentCharacter = default;
    private Transform smallCharacter = default;
    private Transform bigCharacter = default;

    private Vector3 startingPos = default;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        particleEffect.Stop();
    }

    public void SetParticleStartingPoint(Transform pos)
	{
        currentCharacter = pos;
        startingPos = pos.localPosition;
        particleEffect.gameObject.transform.position = startingPos;
	}

    public IEnumerator SwapCharacter(Transform nextCharacter)
    {
        particleEffect.gameObject.transform.position = currentCharacter.localPosition;

        particleEffect.Play();

        currentCharacter = nextCharacter;

        Transform goal = currentCharacter;

        float dist = Vector3.Distance(currentCharacter.position, particleEffect.gameObject.transform.position);

        while (dist > 0.3f)
        {
            dist = Vector3.Distance(currentCharacter.position, particleEffect.gameObject.transform.position);

            particleEffect.gameObject.transform.position = Vector3.MoveTowards(particleEffect.gameObject.transform.position, goal.position, Time.deltaTime * speed);
            yield return new WaitForFixedUpdate();
        }

        particleEffect.Stop();
    }
}
