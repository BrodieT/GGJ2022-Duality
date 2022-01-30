using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance = default;

    IEnumerator LoadScene()
    {
        isLoading = true;

        anim.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);

        if (menu != MenuData.Menus.NULL)
        {
            UIHandler.Instance.SwitchMenu(menu);
        }

        yield return new WaitForSeconds(1);

        //Wait until the scene is fully loaded before attempting anything
        while (!SceneManager.GetSceneByName(sceneToLoad).isLoaded)
        {
            yield return new WaitForEndOfFrame();
        }

        anim.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1);

        isLoading = false;

        menu = MenuData.Menus.NULL;
    }

    private string sceneToLoad = default;

    [SerializeField]
    private Animator anim = default;

    private bool isLoading = false;

    private Transform smallSpawn = default;

    private Transform bigSpawn = default;

    private MenuData.Menus menu = MenuData.Menus.NULL;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void LoadNewScene(string s)
    {
        sceneToLoad = s;
        StartCoroutine(LoadScene());
    }

    public void SetMenuToLoad(MenuData.Menus m)
	{
        menu = m;
	}
    public bool GetIfLoading()
    {
        return isLoading;
    }

    public Transform GetSmallSpawn()
	{
        return smallSpawn;
	}

    public Transform GetBigSpawn()
	{
        return bigSpawn;
	}
}
