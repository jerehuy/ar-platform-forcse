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
    private string id;
    private string name;
    private float dLatitude;
    private float dLongitude;
    private float radius;
    private String audio;
    private float wait;
    private float exit;

    // Rakentaja
    public Coords(string pID, string pName, float pLat, float pLon, String pAudio,
        float pRadi = 5, float pWait = 5, float pExit = 5)
    {
        id = pID;
        name = pName;
        dLatitude = pLat;
        dLongitude = pLon;
        radius = pRadi;
        audio = pAudio;
        wait = pWait;
        exit = pExit;
    }

    // Aksessorit
    // ID
    public string ID
    {
        get { return id; }
        set { id = value; }
    }
    // Nimi
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    // Leveysaste
    public float Latitude
    {
        get { return dLatitude; }
        set { dLatitude = value; }
    }
    // Pituusaste
    public float Longitude
    {
        get { return dLongitude; }
        set { dLongitude = value; }
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
    public float Wait
    {
        get { return wait; }
        set { wait = value; }
    }
    // Poistumisen (odotusaika)
    public float Exit
    {
        get { return exit; }
        set { exit = value; }
    }
}