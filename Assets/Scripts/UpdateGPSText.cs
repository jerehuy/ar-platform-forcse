/*
    Arttu Lehtola
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSText : MonoBehaviour
{
    public Text coordinates;
    public Text situation;

    void Update()
    {
        coordinates.text = "Lat:" + GPS.Instance.latitude.ToString() + "\nLong:" + GPS.Instance.longitude.ToString();

        situation.text = "Status:\n" + GPS.Instance.status;
    }
}
