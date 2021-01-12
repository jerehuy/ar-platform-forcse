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
    public string id = "";
    public string name = "";
    public string latitude = "";
    public string longitude = "";
    public string audioName = "";
    public string radius = "";
    public string activation = "";
    public string deactivation = "";
  }

// ID, LAT, LON, MP3, RADIUS

  [Serializable]
  private class ImageData
  {
    public string id = "";
    public string name = "";
    public string trackedImageName = "";
    public string text = "";
    public string audioName = "";
    public string contentImageNames = "";
  }

  public static List<Coords> GetGPSObjects() {

    NumberFormatInfo format = new CultureInfo("en-US").NumberFormat;

    string data = Resources.Load<TextAsset>("gps_data").ToString();
    GPSData[] gpsData = JsonHelper.FromJson<GPSData>(data);
    
    List<Coords> coordsList = new List<Coords>();
    foreach (GPSData gps in gpsData) {
      
      string newId = gps.id;
      string newName = gps.name;
      float newLatitude = float.Parse(gps.latitude, format);
      float newLongitude = float.Parse(gps.longitude, format);
      string newAudio = gps.audioName;
      float newRadius = float.Parse(gps.radius, format);
      float newActivation = float.Parse(gps.activation, format);
      float newDeactivation = float.Parse(gps.deactivation, format);

      coordsList.Add(new Coords(newId, newName, newLatitude, newLongitude, newAudio, newRadius ,newActivation/*, newDeactivation */));
      //Debug.Log("GPS(" + newId + ", " + newLatitude + ", " + newLongitude + ", " + newAudio + ", " + newRadius + ", " + newActivation + ", " + newDeactivation + ")");
    }
    return coordsList;
  }

  public static List<ImageAR>  GetImageTrackingObjects() {

        string data = Resources.Load<TextAsset>("image_data").ToString();
    ImageData[] imageData = JsonHelper.FromJson<ImageData>(data);
    
    List<ImageAR> imagesList = new List<ImageAR>();
    foreach (ImageData image in imageData) {
      
      string newId = image.id;
      string newName = image.name;
      string newTrackedImage = image.trackedImageName;
      string newText = image.text;
      string newAudio = image.audioName;
      string newContentImageNames = image.contentImageNames;

      imagesList.Add(new ImageAR(newId, newName, newTrackedImage, newText, newAudio, newContentImageNames));
      //Debug.Log("ImageAR(" + newId + ", " + newTrackedImage + ", " + newText + ", " + newAudio + ", " + newPicture + ")");
    }

    return imagesList;
  }
}
