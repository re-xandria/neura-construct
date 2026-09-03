using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConductorController : MonoBehaviour
{
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
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public Image indicator;

    private int lastBeat = -1;
    private double flashUntil;
    public float flashDuration = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        // currBeatInd = beatCount;

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        // Math functions for float
        int currBeat = Mathf.FloorToInt(songPositionInBeats);

        if (currBeat != lastBeat)
        {
            lastBeat = currBeat;
            // track via dsp time and curr beat for consistent value checking between frames
            flashUntil = AudioSettings.dspTime + flashDuration;
        }
        
        indicator.color = AudioSettings.dspTime < flashUntil ? Color.blue : Color.antiqueWhite;

    }

}
