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

    [SerializeField]
    private GameObject[] placeablePrefabs;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private Text currentImageText;
    private Text previousImageText;

    private void Start()
    {
        currentImageText = GameObject.Find("CurrentImageName").GetComponent<Text>();
        previousImageText = GameObject.Find("PrevImageName").GetComponent<Text>();
    }

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject uusiPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            uusiPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, uusiPrefab);
            uusiPrefab.SetActive(false);
        }
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
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
            currentImageText.text = "Tracking: None";
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string nimi = trackedImage.referenceImage.name;
        Vector3 paikka = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[nimi];
        currentImageText.text = "Tracking: " + nimi;
        previousImageText.text = "Recognized: " + nimi;
        prefab.transform.position = paikka;
        prefab.SetActive(true);

        foreach(GameObject go in spawnedPrefabs.Values)
        {
            if(go.name != nimi)
            {
                go.SetActive(false);
            }
        }
    }
}
