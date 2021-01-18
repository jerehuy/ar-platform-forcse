using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour
{

    void Start()
    {
        //Load text from a JSON file (Assets/Resources/Text/jsonFile01.json
        {
            var jsonTextFile = Resources.Load<TextAsset>("ImageData");
            //Then use JsonUtility.FromJson<T>() to deserialize jsonTextFile into an object
        }
        //Load a Texture (Assets/Resources/Textures/texture01.png)
            var texture = Resources.Load<Texture2D>("image1");
    }
}