using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject mainView;
    public GameObject currentTargetBox;
    public Text targetDetectionMethod;
    public Text targetName;
    public GameObject backButton;
    
    public AudioManager am;
    public TabGroup tabs;
    public ImageTracking it;
    public Text mytext = null;

    private string lastDetectedLocation = "";
    private string lastLocationAudio = "";

    void Awake()
    {
        StartCoroutine(WaitForSceneToLoad());
    }

    IEnumerator WaitForSceneToLoad()
    {
        mainView.SetActive(false);

        while (!LoadingScene.loadingReady)
        {
            yield return null;
        }
        
        mainView.SetActive(true);
        currentTargetBox.SetActive(false);
        LoadingScene.mainViewActive = true;
    }

    public void emptyText()
    {
        mytext.text = "";
        tabs.ClearNotification(2);
    }

    public void UpdateCurrentTargetText(string name, int method, string audio)
    {
        if (name != null && method == 1)
        {
            lastDetectedLocation = name;
            lastLocationAudio = audio;
            currentTargetBox.SetActive(true);
            backButton.SetActive(false);

            targetDetectionMethod.text = "Sijainti tunnistettu:";
            targetName.text = name;
        }
        else if (name != null && method == 2)
        {
            currentTargetBox.SetActive(true);
            backButton.SetActive(true);

            targetDetectionMethod.text = "Kuva tunnistettu:";
            targetName.text = name;
        }
    }

    public void BackToLocation()
    {

        emptyText();
        
        if (lastDetectedLocation != "")
        {
            UpdateCurrentTargetText(lastDetectedLocation , 1, lastLocationAudio);
            am.LoadClip(lastLocationAudio);
            tabs.ClearNotification(2);
        }
        else {
            currentTargetBox.SetActive(false);
            am.ClearClip();
            tabs.ClearNotification(2);
        }
    }

    public void HideCurrentTarget()
    {
        currentTargetBox.SetActive(false);
        lastDetectedLocation = "";
    }
}
