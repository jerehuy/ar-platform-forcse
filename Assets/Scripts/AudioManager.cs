using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

// Kovakoodattu audiomanageri!!! Täytyy muokata rajusti

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private int currentClip;
    private AudioSource source;

    void Start ()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if(source.isPlaying)
        {
            return;
        }

        currentClip--;
        if (currentClip < 0)
        {
            currentClip = audioClips.Length - 1;
        }

        StartCoroutine(WaitForMusicEnd());
    }

    IEnumerator WaitForMusicEnd()
    {
        while (source.isPlaying)
        {
            yield return null;
        }
        NextClip();
    }

    public void NextClip()
    {
        source.Stop();
        currentClip++;

        if (currentClip > audioClips.Length - 1)
        {
            currentClip = 0;
        }
        source.clip = audioClips[currentClip];
        source.Play();

        StartCoroutine(WaitForMusicEnd());
    }

    public void StopMusic()
    {
        StopCoroutine("WaitForMusicEnd");
        source.Stop();
    }
}
