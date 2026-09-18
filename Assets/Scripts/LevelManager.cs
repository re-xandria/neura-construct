using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public ConductorController conductorController;

    // conductorController.songPosition for currSong position in seconds
    // conductorController.dspSongTime for song start time in seconds

    private float startTime;
    private float songPositionInBeats;
    private LevelData levelData = new LevelData();

    void Start()
    {
        startTime = (float)conductorController.dspSongTime;
        songPositionInBeats = conductorController.songPositionInBeats;
    }

    void Update()
    {
        songPositionInBeats = conductorController.songPositionInBeats;
    }


    // cannot attach parameterized function to game object, attaching as event listener
    public void saveButtonData(int buttonId, KeyCode keyCode)
    {
        ButtonData press = new ButtonData
        {
            buttonId = buttonId,
            beatPressed = songPositionInBeats,
            keyPressed = keyCode
        };

        levelData.buttonData.Add(press);
        print($"Button Pressed! Button ID: {press.buttonId} | Key Pressed: {press.keyPressed} | Beat Pressed On: {press.beatPressed}");
    }

}

// Reference to conductor to track song time 
// Start a timer to track the song 
// Watches for onButtonPress function triggers from ButtonController file 
// Each trigger of onButtonPress writes to buttonData list -> private []
// Create a JSON file called levelData.json, write list data to JSON file

// on button press create an object that records currBeat, button name/id, and key
// when we hit the stop button, store the buttonData to the json file

// create button press function, create an object with all the data points, then add it to button data list