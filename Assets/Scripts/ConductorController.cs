using System;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        
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
        if (Math.Floor(songPositionInBeats % 4) == 0) { print("BEAT!"); indicator.color = Color.blue; } else { indicator.color = Color.antiqueWhite; }

    }
}
