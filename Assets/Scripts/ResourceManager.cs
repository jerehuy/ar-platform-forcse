using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ResourceManager
{
  [Serializable]
  private class GPSData
  {
    public string latitude = "";
    public string longitude = "";
    public string description = "";
    public string filename = "";
  }

  [Serializable]
  private class ImageData
  {
    public string description = "";
    public string filename = "";
  }

  public static bool GetGPSObjects() {
    string data = Resources.Load<TextAsset>("gps_data").ToString();
    GPSData[] gpsList = JsonHelper.FromJson<GPSData>(data);
    
    foreach (GPSData gps in gpsList) {
      Debug.Log("filename: " + gps.filename + ", description: " + gps.description + ", latitude: " + gps.latitude + ", longitude: " + gps.longitude);
    }
    return true;
  }

  public static bool GetImageTrackingObjects() {
    var data = Resources.Load<TextAsset>("image_data");

    return true;
  }
}
