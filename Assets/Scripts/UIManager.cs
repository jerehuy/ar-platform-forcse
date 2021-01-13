using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject mainView;
    public GameObject menu;

    void Awake()
    {
        StartCoroutine(WaitForSceneToLoad());
    }

    IEnumerator WaitForSceneToLoad()
    {
        //LoadingScene.mainViewActive = true; //prev false
        mainView.SetActive(false);

        while (!LoadingScene.loadingReady)
        {
            yield return null;
        }
        
        mainView.SetActive(true);
        LoadingScene.mainViewActive = true;
    }

}
