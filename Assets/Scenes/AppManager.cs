using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;
    public GameObject loadingScreen;

    AsyncOperation loadingOperation;
    public Slider loadingBar;

    private void Awake()
    {
        instance = this;

        loadingScreen.gameObject.SetActive(true);
        
        loadingOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        StartCoroutine(WaitForLoad());
    }

    IEnumerator WaitForLoad()
    {
        while (!loadingOperation.isDone)
        {
            yield return null;
        }

        loadingScreen.gameObject.SetActive(false);
    }

    /*void Update()
    {
        loadingBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }*/
}