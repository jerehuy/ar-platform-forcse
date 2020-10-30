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
    public double etaisyys;
    public String aika;
    public string tilanne;
    LocationService palvelin = Input.location;

    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());  
        
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            UnityEngine.Debug.Log("GPS has not been enabled by the user");
            tilanne += "GPS has not been enabled by the user\n";
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
            tilanne = "Timeout\n";
            yield break;
        }

        if (palvelin.status == LocationServiceStatus.Failed)
        {
            UnityEngine.Debug.Log("Unable to determine device's location");
            tilanne = "Unable to determine device's location\n";
            yield break;
        }
        else
        {
            latitude = palvelin.lastData.latitude;
            longitude = palvelin.lastData.longitude;
            aika = System.DateTime.Now.ToString();
            yield break;
        }

    }

    void Sijainti()
    {
        if (palvelin.status != LocationServiceStatus.Failed)
        {
            // Nouda uusimmat arvot
            latitude = palvelin.lastData.latitude;
            longitude = palvelin.lastData.longitude;
            aika = System.DateTime.Now.ToString();
        }
        else
        {
            tilanne = "Laitteen sijaintia ei voida päätellä.\n";
        }
    }
    
    void Kytkin()
    {
        if (etaisyys <= 5 && etaisyys != 0)
        {
            AudioSource audio = gameObject.AddComponent<AudioSource>();
            audio.PlayOneShot((AudioClip)Resources.Load("audio1"));
            //UnityEngine.Debug.Log("ETÄISYYS: " + etaisyys);
        }
        /*else
        {
            UnityEngine.Debug.Log("En päässy!");
        }*/
    }
    
    // Update is called once per frame
    void Update()
    {
        // Päivitetään sijaintitiedot
        Sijainti();
        Kytkin();
        // S-marketin edustan koordinaatit (syötä tähän mitkä tahansa haluamasi koordinaatit)
        double latitudeDest = 61.494306;
        double longitudeDest = 23.811462;
        // Vakioiden ja muutujien konversiota radiaaneiksi
        // Laskemme haversinen kaavalla etäisyytemme kohdekoordinaateista
        double havR = 6371e3; // metriä
        double latYk = latitude * Math.PI / 180;
        double latKa = latitudeDest * Math.PI / 180;
        // Lasketaan erotus
        double latDe = (latitudeDest - latitude) * Math.PI / 180;
        double lonDe = (longitudeDest - longitude) * Math.PI / 180;
        double havA = Math.Sin(latDe / 2) * Math.Sin(latDe / 2) +
                        Math.Cos(latYk) * Math.Cos(latKa) *
                        Math.Sin(lonDe / 2) * Math.Sin(lonDe / 2);
        //Vaihtoehtoinen tapa laskea c: double havC = 2 * Math.Atan2(Math.Sqrt(havA), Math.Sqrt(1 - havA));
        double havC = 2 * Math.Asin(Math.Min(1,Math.Sqrt(havA)));
        etaisyys = (havR * havC);
        tilanne = "Etäisyys: " + etaisyys.ToString() + " meters.\n" + "Aikaleima: " + aika;
        
    }
}
