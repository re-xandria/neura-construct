using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public ConductorController conductorController;

    // conductorController.songPosition for currSong position in seconds
    // conductorController.dspSongTime for song start time in seconds

    private float startTime;
    private float songPositionInBeats;
    private LevelData levelData = new LevelData();
    private string path => "D:/Unity Games/neura-construct/Assets/Scripts/Data/data.json";

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

    void OnApplicationQuit()
    {
        string json = JsonUtility.ToJson(levelData);
        print($"JSON: {json}");
        print(path);
        File.WriteAllText(path, json);
    }

}