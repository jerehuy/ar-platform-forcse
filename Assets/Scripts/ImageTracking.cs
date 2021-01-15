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

    [SerializeField]
    private Button addPicture;

    [SerializeField]
    private Button changeLibraryButton;

    MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary;
    private ARTrackedImageManager trackedImageManager = null;
    private Text currentImageText;
    private Text previousImageText;
    private Button captureImage;
    string name = "";
    int counter = 0;
    int buttonCounter = 0;
    int pictureCounter = 0;

    public List<ImageAR> imageList = new List<ImageAR>();
    public AudioManager am;
    public UIManager uiM;

    /* 
    * Method for changing the referenceImageLibrary.
    * This method creates a runtime referenceImageLibrary, and changes to it.
    * If button is pressed again, this method does nothing.
    */
    public string changeLibrary()
    {
        var lib = secondLibrary;
        trackedImageManager.referenceLibrary = trackedImageManager.CreateRuntimeLibrary(lib);
        trackedImageManager.enabled = true;
        previousImageText.text = "Library cleared";
        counter = 0;
        pictureCounter = 0;
        runtimeImageLibrary = null;
        mutableRuntimeReferenceImageLibrary = null;
        string empty = "";
        return empty;
    }

    /* 
    * Method for taking and adding pictures to runtime referenceImageLibrary.
    * This method creates a picture, and adds it toruntime referenceImageLibrary.
    */
    private IEnumerator CaptureImage()
    {

        pictureCounter++;
        string PictureName = "Picture: " + pictureCounter;

        yield return new WaitForEndOfFrame();

        var texture = ScreenCapture.CaptureScreenshotAsTexture();

        var texture2d = texture as Texture2D;

        previousImageText.text = "Image Taken";

        mutableRuntimeReferenceImageLibrary = trackedImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;

        Unity.Jobs.JobHandle jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageJob(texture2d, PictureName, 0.2f);
        jobHandle.Complete();

        previousImageText.text = "Lib count: " + mutableRuntimeReferenceImageLibrary.count;
        currentImageText.text = "Texture count: " + mutableRuntimeReferenceImageLibrary.supportedTextureFormatCount;

        trackedImageManager.referenceLibrary = mutableRuntimeReferenceImageLibrary;
        trackedImageManager.enabled = true;

    }

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
        previousImageText = GameObject.Find("PrevImageName").GetComponent<Text>();

        addPicture.onClick.AddListener(() => StartCoroutine(CaptureImage()));

        changeLibraryButton.onClick.AddListener(() => StartCoroutine(changeLibrary()));
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
                counter++;
                UpdateImage(trackedImage);
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