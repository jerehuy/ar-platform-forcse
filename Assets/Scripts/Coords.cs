/*
    Arttu Lehtola
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Coords
{
    // Variables
    private string id;
    private string name;
    private float dLatitude;
    private float dLongitude;
    private float radius;
    private String audio;
    private float wait;

    // Constructor
    public Coords (string pID, string pName, float pLat, float pLon, String pAudio, float pRadi, float pWait)
    {
        id = pID;
        name = pName;
        dLatitude = pLat;
        dLongitude = pLon;
        radius = pRadi;
        audio = pAudio;
        wait = pWait;
    }

    // Accessors
    // ID
    public string ID
    {
        get { return id; }
        set { id = value; }
    }
    // Name of the location
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    // Latitude
    public float Latitude
    {
        get { return dLatitude;     }
        set { dLatitude = value;    }
    }
    // Longitude
    public float Longitude
    {
        get { return dLongitude;    }
        set { dLongitude = value;   }
    }
    // Radius
    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }
    // Audio clip name
    public String Audio
    {
        get { return audio; }
        set { audio = value; }
    }
    // Waiting time
    public float Wait
    {
        get { return wait; }
        set { wait = value; }
    }
}