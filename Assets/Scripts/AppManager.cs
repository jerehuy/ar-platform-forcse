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

    private List<ImageAR> imageList = new List<ImageAR>();

    private void Awake()
    {
        instance = this;

        //loadingBar = GameObject.Find("LoadingBar").GetComponent<Slider>();
        //DontDestroyOnLoad(loadingScreen);

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        LoadingScene.loadingReady = false;
        loadingScreen.gameObject.SetActive(true);

        loadingOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while (!loadingOperation.isDone)
        {

            yield return null;
        }
//UnityEngine.Debug.Log("päästiin läpi");
        yield return new WaitForSeconds(1);

        LoadingScene.loadingReady = true;
        loadingScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (loadingScreen.gameObject.active)
        {
            loadingBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        }
    }
}