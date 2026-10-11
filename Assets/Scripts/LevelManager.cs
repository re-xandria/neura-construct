using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public ConductorController conductorController;

    public List<NoteData> NoteList = new List<NoteData>();
    private List<Transform> spawnedNotesToPlay = new List<Transform>();

    // conductorController.songPosition for currSong position in seconds
    // conductorController.dspSongTime for song start time in seconds

    public float startTime { get; private set; }
    public float songPositionInBeats;
    private LevelData levelData = new LevelData();
    private string path => "D:/Unity Games/neura-construct/Assets/Scripts/Data/data.json";

    public Transform notePrefab; // the prefab to spawn that reflects the note for players
    public Transform noteParent; // where to spawn the notes under
    public List<Transform> noteCanvasButtons; // the buttons we can press

    private Vector3 spawnPos = new Vector3(0, 1000, 0); // pos to spawn the beats
    private float spawnBeat; // beat we start spawning notes

    public GameObject judgementLine;


    void Start()
    {
        startTime = (float)conductorController.dspSongTime;
        songPositionInBeats = conductorController.songPositionInBeats;
        spawnBeat = songPositionInBeats;
        if (conductorController && conductorController.playGame) PopulateStoredNotes();
        if (!conductorController) conductorController = ConductorController.Instance;
    }

    private void PopulateStoredNotes()
    {
        if (NoteList.Count == 0 || !notePrefab || !noteParent) { Debug.LogWarning("No notes to play or cannot spawn them"); return; }

        for (int i = 0; i < NoteList.Count; i++)
        {
            NoteData ndata = NoteList[i];
            Transform noteClone = Instantiate(notePrefab, noteParent);
            noteClone.transform.position = noteCanvasButtons[(int)ndata.buttonType].position; // look into this line
            spawnedNotesToPlay.Add(noteClone);
        }
    }

    void FixedUpdate()
    {
        float currBeat = Mathf.FloorToInt(conductorController.songPositionInBeats);

        // calc the progress mathf using inverse lerp from spawn beat, target beat which is timeStamp, and songPositionInBeats)
        // translate position using vector3 lerp, using spawnPos, pos of judgementLine, and progress
        // find a way to make better later without using lerp

        // translate the note such that it reaches the judgement line at the spawn beat
        // we need to delay the song starting for a few seconds to let the initial beats spawn as expected

        // notes are very choppy and likely not reaching the judegement line at the right time

        if (spawnedNotesToPlay.Count > 0)
            for (int i = 0; i < spawnedNotesToPlay.Count; i++)
            {
                float targetBeat = NoteList[i].timeStamp;
                float progress = Mathf.InverseLerp(spawnBeat, targetBeat, currBeat);
                spawnedNotesToPlay[i].position = Vector3.Lerp(noteCanvasButtons[(int)NoteList[i].buttonType].position + spawnPos, judgementLine.transform.position, progress); // look into this line
                print($"Note {i} | Pos {spawnedNotesToPlay[i].position} | Target beat {targetBeat} | Progess {progress}");
            }
    }

    public void StoreNoteData(NoteData _newNote)
    {
        if (_newNote == null || !conductorController) return;

        if (conductorController.recordAudio)
            NoteList.Add(_newNote);
        else if (conductorController.playGame) // if we are playing notes
        {
            //compare note against stored ones
        }
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