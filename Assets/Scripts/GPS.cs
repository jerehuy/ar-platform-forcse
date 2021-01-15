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
    public List<string> processing = new List<string>();
    public bool flag = false;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());

        // Haetaan oliolista
        CoordsList = ResourceManager.GetGPSObjects();

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
        UnityEngine.Debug.Log("(" + Lat + "|" + Lon + ") PALAUTUS: " + (havR * havC));
        return (havR * havC);
    }

    void Check()
    {
        foreach (var corObject in CoordsList)
        {
            double distance = Distance(corObject.Latitude, corObject.Longitude);
            bool process = true;
            int ccp = 0;

            if (Distance(corObject.Latitude, corObject.Longitude) != 0
                && Distance(corObject.Latitude, corObject.Longitude) <= corObject.Radius)
            {
                // Tarkistetaan, että kyseisellä koordinaatitunnuksella ei ole prosessia jo käynnissä
                foreach (var corID in processing)
                {
                    if (corID.Equals(corObject.ID))
                    {
                        process = false;
                    }
                }

                //(concurrent processes = ccp)
                // jos prosesseja on useampi, niin vältä konflikti
                /* Tämä ei ole optimaali ratkaisu */
                if (ccp > 1)
                {
                    process = false;
                }

                // Tallennetaan prosessoitavan koordinaatin id listaan
                if (process)
                {
                    processing.Add(corObject.ID);
                    StartCoroutine(Waiting(corObject.Latitude, corObject.Longitude, corObject.Radius, corObject.Wait, corObject.Audio, corObject.ID));
                    ccp = processing.Count;
                }
            }
        }
    }

    private IEnumerator Waiting(float Lat, float Lon, float Radius, float Wait, string Audio, string ID)
    {
        float wait = Wait;
        float exitWait = Wait;
        while (wait > 0)
        {
            yield return new WaitForSeconds(1);
            wait--;
        }
        if (Distance(Lat, Lon) <= Radius)
        {
            // Hyödynnämme PlayOneShot -metodia vain testaustarkoituksessa
            AudioSource audio = gameObject.AddComponent<AudioSource>();
            audio.PlayOneShot((AudioClip)Resources.Load(Audio));
        }
        // Odota (poistumis)aika, jos poistut koordinaatista
        // Tällä hetkellä samaa muuttujaa käytetään poistumiseen ja varmistamiseen
        else
        {
            while (wait > 0)
            {
                yield return new WaitForSeconds(1);
                exitWait--;
            }
        }
        // Suoritettuamme prosessin poistamme sen listalta
        int ind = 0;
        foreach (var corID in processing)
        {
            ind++;
            if (corID == ID)
            {
                processing.RemoveAt(ind);
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
        if (flag == false)
        {
            status = /*"Etäisyys: " + distance.ToString() + " meters.\n" +*/ "Aikaleima: " + time;
        }
        else
        {
            status = "Pääsimme perille!";
        }

    }
}
