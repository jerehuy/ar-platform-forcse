using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System.Globalization;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{

    private ARTrackedImageManager trackedImageManager;
    private Text currentImageText;
    private Text previousImageText;
    string nimi = "";
    int laskuri = 0;

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
            if (trackedImage.referenceImage.name != nimi)
            {
                UpdateImage(trackedImage);
                laskuri++;
            }
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            currentImageText.text = "Tracking: None";
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {

        nimi = trackedImage.referenceImage.name;
        currentImageText.text = "Tracked:" + nimi;
        previousImageText.text = "Counter: " + laskuri;

    }
}
