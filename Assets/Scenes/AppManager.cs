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

        //loadingBar = GameObject.Find("LoadingBar").GetComponent<Slider>();

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        loadingScreen.gameObject.SetActive(true);

        loadingOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        yield return new WaitForSeconds(0.1f);

        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            if (loadingOperation.progress >= 0.9f)
            {
                
            }
//UnityEngine.Debug.Log("jumelis");
            yield return null;
        }
//UnityEngine.Debug.Log("päästiin läpi");
        yield return new WaitForSeconds(3);

                loadingScreen.gameObject.SetActive(false);
                loadingOperation.allowSceneActivation = true;
    }

    void Update()
    {
        if (loadingScreen.gameObject.active)
        {
            loadingBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        }
    }
}