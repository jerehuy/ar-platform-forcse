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
    // Muuttujat
    private int id;
    private float dLatitude;
    private float dLongitude;
    private float radius;
    private String audio;
    private int wait;

    // Rakentaja
    public Coords (int pID, float pLat, float pLon, String pAudio, float pRadi = 5, int pWait = 5)
    {
        id = pID;
        dLatitude = pLat;
        dLongitude = pLon;
        radius = pRadi;
        audio = pAudio;
        wait = pWait;
    }

    // Aksessorit
    // ID
    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    // Leveysaste
    public float Latitude
    {
        get { return dLatitude;     }
        set { dLatitude = value;    }
    }
    // Pituusaste
    public float Longitude
    {
        get { return dLongitude;    }
        set { dLongitude = value;   }
    }
    // Säde
    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }
    // Audiopätkä
    public String Audio
    {
        get { return audio; }
        set { audio = value; }
    }
    // Odotusaika
    public int Wait
    {
        get { return wait; }
        set { wait = value; }
    }
}