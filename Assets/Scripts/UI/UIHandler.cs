using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MenuData
{
    public enum Menus { MAINMENU, SETTINGS, CONTROLS, GAMEUI, CREDITS, WIN, LEVELSELECT}
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
    }

    public void SwitchMenu(MenuData.Menus m)
	{
        foreach(MenuData d in allMenus)
		{
            if(d.menu == m)
			{
                if (currentMenu.menu != m)
                {
                    StartCoroutine(HideCurrentMenu(currentMenu.menuObj));
                }

                d.menuObj.SetActive(true);
                currentMenu = d;

                break;
			}
		}

        previousMenu = currentMenu;
    }

    IEnumerator HideCurrentMenu(GameObject g)
	{
        float time = 1f;

        g.GetComponent<Animator>().SetTrigger("MenuHide");

        while(time > 0)
		{
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
		}

        g.SetActive(false);
	}

    public bool GetCharacterMovement()
	{
        return currentMenu.allowPlayerMovement;
	}
}
