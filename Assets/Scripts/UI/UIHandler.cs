using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class MenuData
{
    public enum Menus { MAINMENU, SETTINGS, CONTROLS, GAMEUI, CREDITS, WIN, LEVELSELECT, PAUSE, BACK, NULL}
    public Menus menu = default;
    public GameObject menuObj = default;
    public bool allowPlayerMovement = false;
}

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance = default;

    [SerializeField]
    private List<MenuData> allMenus = new List<MenuData>();

    private MenuData currentMenu = default;
    private MenuData previousMenu = default;

    [SerializeField]
    private MenuData.Menus startingMenu = default;

    private bool switchingMenu = false;


    [SerializeField]
    private AudioMixer mixer = default;

    [SerializeField]
    private Slider masterSlider = default;

    [SerializeField]
    private Slider effectsSlider = default;

    [SerializeField]
    private Slider UISlider = default;


    [SerializeField]
    private Slider MusicSlider = default;

    [SerializeField]
    private AudioSource source = default;

    [SerializeField]
    private AudioClip menuChange = default;

    private bool movement = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        foreach (MenuData d in allMenus)
        {
            d.menuObj.SetActive(false);

            if (d.menu == startingMenu)
            {
                currentMenu = d;
            }
        }

        SwitchMenu(currentMenu.menu);

        mixer.SetFloat("MasterVolume", masterSlider.value);
        mixer.SetFloat("EffectsVolume", effectsSlider.value);
        mixer.SetFloat("UIVolume", UISlider.value);
        mixer.SetFloat("MusicVolume", MusicSlider.value);
    }

    public void SwitchMenu(MenuData.Menus m)
	{
        previousMenu = currentMenu;

        foreach (MenuData d in allMenus)
		{
            if(d.menu == m)
			{
                if (currentMenu.menu != m)
                {
                    StartCoroutine(HideCurrentMenu(currentMenu.menuObj));
                }

                movement = d.allowPlayerMovement;

                d.menuObj.SetActive(true);
                currentMenu = d;

                break;
			}
		}

        if (AudioHandler.Instance != null)
        {
            AudioHandler.Instance.PlayButtonHover();
        }

        source.clip = menuChange;
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }

    IEnumerator HideCurrentMenu(GameObject g)
	{
        switchingMenu = true;

        float time = 1f;

        g.GetComponent<Animator>().SetTrigger("MenuHide");

        while(time > 0)
		{
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
		}

        g.SetActive(false);
        switchingMenu = false;

	}

    public bool GetSwitchingMenu()
	{
        return switchingMenu;
	}

    public void GoBackAMenu()
	{
        SwitchMenu(previousMenu.menu);
	}

    public void SetCharacterMovement(bool set)
	{
        movement = set;
	}

    public bool GetCharacterMovement()
	{
        return movement;
	}

    public void UpdateMasterVolume()
	{
        mixer.SetFloat("MasterVolume", masterSlider.value);
	}

    public void UpdateEffectsVolume()
    {
        mixer.SetFloat("EffectsVolume", effectsSlider.value);
    }

    public void UpdateUIVolume()
    {
        mixer.SetFloat("UIVolume", UISlider.value);
    }

    public void UpdateMusicVolume()
    {
        mixer.SetFloat("MusicVolume", MusicSlider.value);
    }
}
