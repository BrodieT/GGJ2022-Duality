using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitchButton : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = default;

    // Start is called before the first frame update
    void Start()
    {
        Button bttn = GetComponent<Button>();
        bttn.onClick.AddListener(delegate { SceneHandler.Instance.LoadNewScene(sceneToLoad); });
    }

    public string GetSceneName()
	{
        return sceneToLoad;
	}
}
