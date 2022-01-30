using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private MenuData.Menus menu = default;

    // Start is called before the first frame update
    void Start()
    {
        Button bttn = GetComponent<Button>();

        if (menu == MenuData.Menus.BACK)
        {
            bttn.onClick.AddListener(delegate { UIHandler.Instance.GoBackAMenu(); });
        }
        else
        {
            bttn.onClick.AddListener(delegate { SwitchMenu(); });
        }
    }

    void SwitchMenu()
	{
        SceneSwitchButton b = default;
        TryGetComponent<SceneSwitchButton>(out b);

        if (b != null)
        {
            SceneHandler.Instance.LoadNewScene(b.GetSceneName());
            SceneHandler.Instance.SetMenuToLoad(menu);
        }
		else
		{
            UIHandler.Instance.SwitchMenu(menu);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       if(AudioHandler.Instance != null)
		{
            AudioHandler.Instance.PlayButtonHover();
		}
    }
}
