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

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);

        //Wait until the scene is fully loaded before attempting anything
        while (!SceneManager.GetSceneByName(sceneToLoad).isLoaded)
        {
            yield return new WaitForEndOfFrame();
        }

        smallSpawn = GameObject.FindGameObjectWithTag("SmallSpawn").transform;
        bigSpawn = GameObject.FindGameObjectWithTag("BigSpawn").transform;

        anim.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1);

        isLoading = false;

    }

    private string sceneToLoad = default;

    [SerializeField]
    private Animator anim = default;

    private bool isLoading = false;

    private Transform smallSpawn = default;

    private Transform bigSpawn = default;

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
