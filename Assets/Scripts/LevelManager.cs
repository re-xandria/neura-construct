using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public ConductorController conductorController;

    // conductorController.songPosition for currSong position in seconds
    // conductorController.dspSongTime for song start time in seconds

    private float startTime;
    private float songPosition;

    void Start()
    {
        startTime = conductorController.songPosition;
        songPosition = (float)conductorController.dspSongTime;
    }

    void Update()
    {
        
    }
}

// Reference to conductor to track song time 
// Start a timer to track the song 
// Watches for onButtonPress function triggers from ButtonController file 
// Each trigger of onButtonPress writes to buttonData list -> private []
// Create a JSON file called levelData.json, write list data to JSON file
