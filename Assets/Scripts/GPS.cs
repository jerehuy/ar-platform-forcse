/*
    Arttu Lehtola
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GPS : MonoBehaviour
{
    public static GPS Instance { set; get; }
    public float latitude;
    public float longitude;
    public double distance;
    public String time;
    public string status;
    LocationService palvelin = Input.location;
    // Luodaan testilista olioille
    public List<Coords> CoordsList = new List<Coords>();
    public bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
<<<<<<< HEAD
        // SYÖTETÄÄN LISTAAN OLIOITA (testitarkoituksessa)
        // ID, LAT, LON, MP3, RADIUS
        CoordsList.Add(new Coords(1, 25f, 35f, "audio1", 5));
        CoordsList.Add(new Coords(2, 45f, 55f, "audio1", 5));
        CoordsList.Add(new Coords(3, 65f, 75f, "audio1", 5));
        CoordsList.Add(new Coords(4, 85f, 95f, "audio1", 5));
        // Testi
        CoordsList.Add(new Coords(5, 61.494306f, 23.811462f, "audio1", 15));

=======

        //ResourceManager.GetGPSObjects();
>>>>>>> a4fa84453dc96fa345d054debf9bb099d7b64f27
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            UnityEngine.Debug.Log("GPS has not been enabled by the user");
            status += "GPS has not been enabled by the user\n";
            yield break;
        }

        Input.location.Start(0.1f, 1);
        int maxWait = 20;
        while (palvelin.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            UnityEngine.Debug.Log("Timeout");
            status = "Timeout\n";
            yield break;
        }

        if (palvelin.status == LocationServiceStatus.Failed)
        {
            UnityEngine.Debug.Log("Unable to determine device's location");
            status = "Unable to determine device's location\n";
            yield break;
        }
        else
        {
            latitude = palvelin.lastData.latitude;
            longitude = palvelin.lastData.longitude;
            time = System.DateTime.Now.ToString();
            yield break;
        }
    }

    void Location()
    {
        if (palvelin.status != LocationServiceStatus.Failed)
        {
            // Nouda uusimmat arvot
            latitude = palvelin.lastData.latitude;
            longitude = palvelin.lastData.longitude;
            time = System.DateTime.Now.ToString();
        }
        else
        {
            status = "Laitteen sijaintia ei voida päätellä.\n";
        }
    }

    double Distance(float Lat, float Lon)
    {
        // Etäisyyden laskeminen haversinen kaavalla
        double latitudeDest = Lat;
        double longitudeDest = Lon;
        double havR = 6371e3;
        double lat1 = latitude * Math.PI / 180;
        double lat2 = latitudeDest * Math.PI / 180;
        double latDe = (latitudeDest - latitude) * Math.PI / 180;
        double lonDe = (longitudeDest - longitude) * Math.PI / 180;
        double havA = Math.Sin(latDe / 2) * Math.Sin(latDe / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(lonDe / 2) * Math.Sin(lonDe / 2);
        double havC = 2 * Math.Asin(Math.Min(1, Math.Sqrt(havA)));
        // Palautetaan etäisyys (metreinä)
        UnityEngine.Debug.Log("("+Lat+"|"+Lon+") PALAUTUS: " + (havR * havC));
        return (havR * havC);
    }

    //IEnumerable<Coords> CoordsList
    void Check()
    {
        foreach (var corObject in CoordsList)
        {
            if (Distance(corObject.Latitude, corObject.Longitude) != 0
                && Distance(corObject.Latitude, corObject.Longitude) <= corObject.Radius)
            {
                AudioSource audio = gameObject.AddComponent<AudioSource>();
                audio.PlayOneShot((AudioClip)Resources.Load(corObject.Audio));
                flag = true;
            }
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
        // Päivitetään sijaintitiedot
        Location();
        // Tarkista koordinaattien etäisyys
        Check();
        
        

        // S-marketin edustan koordinaatit (syötä tähän mitkä tahansa haluamasi koordinaatit)
        if (flag == false) {
            status = /*"Etäisyys: " + distance.ToString() + " meters.\n" +*/ "Aikaleima: " + time;
        }
        else
        {
            status = "Pääsimme perille!";
        }
        

        
        
    }
}
