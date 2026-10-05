using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public ConductorController conductorController;

    public List<NoteData> NoteList = new List<NoteData>();
    private List<Transform> spawnedNotesToPlay = new List<Transform>();

    // conductorController.songPosition for currSong position in seconds
    // conductorController.dspSongTime for song start time in seconds

    public float startTime { get; private set; }
    private float songPositionInBeats;
    private LevelData levelData = new LevelData();
    private string path => "D:/Unity Games/neura-construct/Assets/Scripts/Data/data.json";

    public Transform notePrefab; // the prefab to spawn that reflects the note for players
    public Transform noteParent; // where to spawn the notes under
    public List<Transform> noteCanvasButtons; // the buttons we can press

    void Start()
    {
        startTime = (float)conductorController.dspSongTime;
        songPositionInBeats = conductorController.songPositionInBeats;
        if (conductorController && conductorController.playGame) PopulateStoredNotes();
        if (!conductorController) conductorController = ConductorController.Instance;
    }

    private void PopulateStoredNotes()
    {
        if (NoteList.Count == 0 || !notePrefab || !noteParent){ Debug.LogWarning("No notes to play or cannot spawn them"); return; }

        for(int i = 0; i < NoteList.Count; i++)
        {
            NoteData ndata = NoteList[i];
            Transform noteClone = Instantiate(notePrefab, noteParent);
            noteClone.transform.position = noteCanvasButtons[(int)ndata.buttonType].position + new Vector3(0, i * Mathf.Abs(ndata.timeStamp), 0);
            spawnedNotesToPlay.Add(noteClone);
        }
    }

    void FixedUpdate()
    {
        songPositionInBeats = conductorController.songPositionInBeats;

        if(spawnedNotesToPlay.Count > 0) 
            for(int i = 0; i < spawnedNotesToPlay.Count; i++)
                spawnedNotesToPlay[i].Translate(Vector3.down * (conductorController.secPerBeat * conductorController.songBpm));
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