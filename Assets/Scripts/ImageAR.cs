
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAR
{
    //Variables
    private string id;
    private string name;
    private string trackedImage;
    private string text;
    private string audio;
    private string[] pictures;

    //Builder
    public ImageAR (string pID, string pName, string pTrackedImage, string pText, string pAudio, string[] pPictures)
    {
        id = pID;
        name = pName;
        trackedImage = pTrackedImage;
        text = pText;
        audio = pAudio;
        pictures = pPictures;
    }

    //Accessors
    
    //ID
    public string ID
    {
        get { return id; }
        set { id = value; }
    }

    //Name
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string TrackedImage
    {
        get { return trackedImage; }
    }

    //Text
    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    //Audio
    public string Audio
    {
        get { return audio; }
        set { audio = value; }
    }

    //Picture
    public string[] Pictures
    {
        get { return pictures; }
        set { pictures = value; }
    }
    
}