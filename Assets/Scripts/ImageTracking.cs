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

    [SerializeField]
    XRReferenceImageLibrary secondLibrary = null;

    private ARTrackedImageManager trackedImageManager = null;
    private Text currentImageText;
    private Text previousImageText;
    string name = "";
    int counter = 0;

    private void Start()
    {
        currentImageText = GameObject.Find("CurrentImageName").GetComponent<Text>();
        previousImageText = GameObject.Find("PrevImageName").GetComponent<Text>();
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
                counter++;
            }
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            currentImageText.text = "Tracking: None";
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {

        name = trackedImage.referenceImage.name;
        currentImageText.text = "Tracked:" + name;
        previousImageText.text = "Counter: " + counter;

        if (counter == 6)
        {
            var lib = secondLibrary;
            trackedImageManager.referenceLibrary = trackedImageManager.CreateRuntimeLibrary(lib);
            trackedImageManager.enabled = true;

            lib = runtimeImageLibrary;
            trackedImageManager.referenceLibrary = trackedImageManager.CreateRuntimeLibrary(lib);
            trackedImageManager.enabled = true;

            previousImageText.text = "Change happens";

            counter = 0;
        }


    }
}
