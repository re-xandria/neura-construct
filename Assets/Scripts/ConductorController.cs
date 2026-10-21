using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConductorController : MonoBehaviour
{
    public static ConductorController Instance { get; private set; }

    public bool recordAudio = false; // if false does not record
    public bool playGame = false; // this should play back notes we have

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public double dspSongTime;

    private bool songStarted;
    private bool songRunning;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public Image indicator;

    public int lastBeat = -1;
    private float flashUntil;
    public float flashDuration = .5f;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        if (musicSource) musicSource.Stop();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        if (playGame) StartSong();

    }

    // FIXEDUpdate is called once per frame
    void FixedUpdate()
    {
        if (recordAudio || playGame)
        {
            if (!songRunning) StartSong();

            //determine how many seconds since the song started
            songPosition = GetSongPositionSeconds();

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;

            // Math functions for float
            int currBeat = Mathf.FloorToInt(songPositionInBeats);

            if (currBeat != lastBeat)
            {
                // print(currBeat + " | " + lastBeat);
                lastBeat = currBeat;
                // track via dsp time and curr beat for consistent value checking between frames
                flashUntil = songPosition + flashDuration;
            }

            indicator.color = songPosition < flashUntil ? Color.blue : Color.antiqueWhite;
        }
        else
        {
            if (songRunning)
            {
                songPosition = GetSongPositionSeconds();
                songPositionInBeats = songPosition / secPerBeat;
                musicSource.Pause();
                songRunning = false;
            }
        }

    }

    public void ToggleRecord()
    {
        if (playGame) return;

        recordAudio = !recordAudio;

        if (recordAudio)
        {
            StartSong();
        }
        else if (songRunning)
        {
            songPosition = GetSongPositionSeconds();
            songPositionInBeats = songPosition / secPerBeat;
            musicSource.Pause();
            songRunning = false;
        }
    }

    private void StartSong()
    {
        if (songRunning || !musicSource) return;

        if (songStarted)
        {
            dspSongTime = AudioSettings.dspTime - songPosition;
            musicSource.UnPause();
        }
        else
        {
            dspSongTime = AudioSettings.dspTime + 0.1;
            musicSource.PlayScheduled(dspSongTime);
            songStarted = true;
        }

        songRunning = true;
    }

    public float GetSongPositionInBeats()
    {
        return GetSongPositionSeconds() / secPerBeat;
    }

    private float GetSongPositionSeconds()
    {
        if (!songRunning) return songPosition;
        return Mathf.Max(0f, (float)(AudioSettings.dspTime - dspSongTime));
    }

}
