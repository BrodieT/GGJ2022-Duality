using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private MenuData.Menus menu = default;

    // Start is called before the first frame update
    void Start()
    {
        Button bttn = GetComponent<Button>();
        bttn.onClick.AddListener(delegate { UIHandler.Instance.SwitchMenu(menu); });
    }
}
