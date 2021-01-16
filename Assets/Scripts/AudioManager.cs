using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioClip audioClip = null;
    private AudioSource source;

    public Text clipTimeText;

    private int fullLength;
    private int playTime;
    private int seconds;
    private int minutes;

    public GameObject playButton;
    public GameObject pauseButton;

    public TabGroup tabs;
    private int audioControlButton = 0;

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
            //source.clip = audioClip;
            //fullLength = (int)source.clip.length;
            source.Play();

            tabs.ClearNotification(audioControlButton);

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
    {
        if (audioClip == null || audioClip.name != c)
        {
            audioClip = Resources.Load(c) as AudioClip;
            source.clip = audioClip;

            tabs.Notify(audioControlButton);
            
            fullLength = (int)source.clip.length;
            ShowPlayTime();
        }
    }

    public void ClearClip()
    {
        pauseButton.SetActive(false);
        playButton.SetActive(true);

        audioClip = null;
        source.clip = null;

        fullLength = 0;
        playTime = 0;
        ShowPlayTime();

        tabs.ClearNotification(audioControlButton);
    }
}
