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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayPushingSound()
    {
        source.loop = true;
        source.clip = clip;
        source.Play();
    }

    public void StopPushingSpund()
    {
        source.Stop();
    }

    public void AllowPushing(bool set)
    {
        if (set)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
		else
		{
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ControllableCharacter character = default;
        collision.transform.TryGetComponent<ControllableCharacter>(out character);

        if (character != null)
        {
            if (character.GetIfMoving())
            {
                if (character.GetSettings().weightThreshold >= entity.weight)
                {
                    AllowPushing(true);
                }
                else
                {
                    AllowPushing(false);
                }
            }
        }
    }
}
