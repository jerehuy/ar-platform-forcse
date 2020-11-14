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
    public string audioName = "";
    public string radius = "";
  }

// ID, LAT, LON, MP3, RADIUS

  [Serializable]
  private class ImageData
  {
    public int id = 0;
    public string trackedImageName = "";
    public string text = "";
    public string audioName = "";
    public string pictureName = "";
  }

  public static List<Coords> GetGPSObjects() {

    NumberFormatInfo format = new CultureInfo("en-US").NumberFormat;

    string data = Resources.Load<TextAsset>("gps_data").ToString();
    GPSData[] gpsData = JsonHelper.FromJson<GPSData>(data);
    
    List<Coords> coordsList = new List<Coords>();
    foreach (GPSData gps in gpsData) {
      
      int newId = gps.id;
      float newLatitude = float.Parse(gps.latitude, format);
      float newLongitude = float.Parse(gps.longitude, format);
      string newAudio = gps.audioName;
      float newRadius = float.Parse(gps.radius, format);

      coordsList.Add(new Coords(newId, newLatitude, newLongitude, newAudio, newRadius));
      //Debug.Log("GPS(" + newId + ", " + newLatitude + ", " + newLongitude + ", " + newAudioName + ", " + newRadius + ")");
    }
    return coordsList;
  }

  public static bool GetImageTrackingObjects() {

    string data = Resources.Load<TextAsset>("image_data").ToString();
    ImageData[] imageData = JsonHelper.FromJson<ImageData>(data);
    
    List<ImageAR> imagesList = new List<ImageAR>();
    foreach (ImageData image in imageData) {
      
      int newId = image.id;
      string newTrackedImage = image.trackedImageName;
      string newText = image.text;
      string newAudio = image.audioName;
      string newPicture = image.pictureName;

      imagesList.Add(new ImageAR(newId, newTrackedImage, newText, newAudio, newPicture));
      //Debug.Log("ImageAR(" + newId + ", " + newTrackedImage + ", " + newText + ", " + newAudio + ", " + newPicture + ")");
    }

    return true;
  }
}
