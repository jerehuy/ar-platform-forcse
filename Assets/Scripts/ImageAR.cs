
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAR
{
    //Variables
    private int id;
    private string trackedImage;
    private string text;
    private string audio;
    private string picture;

    //Builder
    public ImageAR (int pID, string pTrackedImage, string pText, string pAudio, string pPicture)
    {
        id = pID;
        trackedImage = pTrackedImage;
        text = pText;
        audio = pAudio;
        picture = pPicture;
    }

    //Accessors
    
    //ID
    public int ID
    {
        get { return id; }
        set { id = value; }
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
    public string Picture
    {
        get { return picture; }
        set { picture = value; }
    }
    
}