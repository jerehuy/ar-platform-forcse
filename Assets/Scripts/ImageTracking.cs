using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;



[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{

    [SerializeField]
    XRReferenceImageLibrary runtimeImageLibrary;

    MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary;
    private ARTrackedImageManager trackedImageManager = null;
    private Text currentImageText;
    string name = "";
    public List<ImageAR> imageList = new List<ImageAR>();
    public AudioManager am;
    public UIManager uiM;
    public Text mytext = null;

    private void Start()
    {
        StartCoroutine(WaitForUIActivation());

        imageList = ResourceManager.GetImageTrackingObjects();
    }

    IEnumerator WaitForUIActivation()
    {
        while (!LoadingScene.mainViewActive)
        {
            yield return null;
        }

        currentImageText = GameObject.Find("CurrentImageName").GetComponent<Text>();
        
    }

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.referenceImage.name != name)
            {
                UpdateImage(trackedImage);
            }
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            currentImageText.text = "Tracking: None";
        }
    }

    public void changeText(string name)
    {
        foreach (var image in imageList)
        {
            if (image.TrackedImage == name)
            {
                mytext.text = image.Text;
                break;
            }
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        name = trackedImage.referenceImage.name;
        changeText(name);
        currentImageText.text = "Tracked:" + name;

        foreach (var image in imageList)
        {
            if (image.TrackedImage == name)
            {
                currentImageText.text = "Tracked:" + name;
            
                am.LoadClip(image.Audio);
                uiM.UpdateCurrentTargetText(image.Name, 2, "");
                break;
            }
        }
    }
}