
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Images
{
    //Variables
    private int id;
    private String text;
    private String audio;
    private String picture;

    //Builder
    public Images (int pID, String pText, String pAudio, String pPicture)
    {
        id = pID;
        text = pText;
        audio = pAudio;
        picture = pPicture
    }

    //Accessors
    
    //ID
    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    //Text
    public String Text
    {
        get { return text; }
        set { text = value; }
    }

    //Audio
    public String Audio
    {
        get { return audio; }
        set { audio = value; }
    }

    //Picture
    public String Picture
    {
        get { return picture; }
        set { picture = value; }
    }
    
}