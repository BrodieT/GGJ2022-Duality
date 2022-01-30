using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance = default;

    [SerializeField]
    private AudioClip buttonHover = default;

    [SerializeField]
    private AudioClip buttonClick = default;

    [SerializeField]
    private AudioSource UISource = default;

    [SerializeField]
    private AudioSource source = default;

    [SerializeField]
    private AudioClip keyCollect = default;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void PlayButtonHover()
	{
        if (!UIHandler.Instance.GetSwitchingMenu())
        {
            UISource.clip = buttonHover;
            UISource.Play();
        }
	}

    public void PlayButtonClick()
	{
        if (!UIHandler.Instance.GetSwitchingMenu())
        {
            UISource.clip = buttonClick;
            UISource.Play();
        }
    }

    public void CollectKey()
	{
        source.clip = keyCollect;
        source.Play();
	}
}
