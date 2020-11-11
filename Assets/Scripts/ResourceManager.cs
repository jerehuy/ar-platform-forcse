using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public static class ResourceManager
{
  [Serializable]
  private class GPSData
  {
    public int id = 0;
    public string latitude = "";
    public string longitude = "";
    public string filename = "";
    public string radius = "";
  }

// ID, LAT, LON, MP3, RADIUS

  [Serializable]
  private class ImageData
  {
    public string description = "";
    public string filename = "";
  }

  public static List<Coords> GetGPSObjects() {

    NumberFormatInfo format = new CultureInfo("en-US").NumberFormat;

    string data = Resources.Load<TextAsset>("gps_data").ToString();
    GPSData[] gpsList = JsonHelper.FromJson<GPSData>(data);
    
    List<Coords> CoordsList = new List<Coords>();
    foreach (GPSData gps in gpsList) {
      
      int newId = gps.id;
      float newLatitude = float.Parse(gps.latitude, format);
      float newLongitude = float.Parse(gps.longitude, format);
      string newFilename = gps.filename;
      float newRadius = float.Parse(gps.radius, format);

      CoordsList.Add(new Coords(newId, newLatitude, newLongitude,newFilename, newRadius));
      //Debug.Log("GPS(" + newId + ", " + newLatitude + ", " + newLongitude + ", " + newFilename + ", " + newRadius + ")");
    }
    return CoordsList;
  }

  public static bool GetImageTrackingObjects() {
    var data = Resources.Load<TextAsset>("image_data");

    return true;
  }
}
