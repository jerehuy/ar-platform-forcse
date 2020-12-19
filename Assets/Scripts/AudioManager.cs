﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioClip audioClip;
    private AudioSource source;

    public Text clipTimeText;

    private int fullLength;
    private int playTime;
    private int seconds;
    private int minutes;

    public GameObject playButton;
    public GameObject pauseButton;

    void Start ()
    {
        StartCoroutine(WaitForUIActivation());
    }

    IEnumerator WaitForUIActivation()
    {
        while (!LoadingScene.mainViewActive)
        {
            yield return null;
        }

        pauseButton.SetActive(false);

        source = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if (audioClip != null)
        {
            source.clip = audioClip;
            fullLength = (int)source.clip.length;
            source.Play();

            playButton.SetActive(false);
            pauseButton.SetActive(true);

            StartCoroutine("WaitForAudioEnd");
        }
    }

    IEnumerator WaitForAudioEnd()
    {
        while (source.isPlaying)
        {
            playTime = (int)source.time;
            ShowPlayTime();
            yield return null;
        }

        //pauseButton.SetActive(false);
        //playButton.SetActive(true);
    }

    public void PauseAudio()
    {
        source.Pause();
        StopCoroutine("WaitForAudioEnd");

        pauseButton.SetActive(false);
        playButton.SetActive(true);
    }

    public void ResetTrack()
    {
        source.Stop();
        StopCoroutine("WaitForAudioEnd");

        PlayAudio();
    }

    void ShowPlayTime()
    {
        seconds = playTime % 60;
        minutes = (playTime / 60) % 60;
        clipTimeText.text = minutes + ":" + seconds.ToString("D2") + "/" + ((fullLength / 60) % 60 + ":" + (fullLength % 60).ToString("D2"));
    }

    public void LoadClip(string c)
    {//StartCoroutine(WaitForUIActivation());
        if (audioClip == null || audioClip.name != c)
        {UnityEngine.Debug.Log("ladataan " + c);
            audioClip = Resources.Load(c) as AudioClip;
            //source.clip = audioClip;
            //UnityEngine.Debug.Log(audioClip.name);

            //ShowPlayTime();
        }
    }
}
