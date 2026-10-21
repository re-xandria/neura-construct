using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public ConductorController conductorController;

    public List<NoteData> NoteList = new List<NoteData>();
    private List<Transform> spawnedNotesToPlay = new List<Transform>();
    private List<float> previousNoteY = new List<float>();
    private List<bool> crossingLogged = new List<bool>();

    private LevelData levelData = new LevelData();
    private string path => "D:/Unity Games/neura-construct/Assets/Scripts/Data/data.json";

    public Transform notePrefab; // the prefab to spawn that reflects the note for players
    public Transform noteParent; // where to spawn the notes under
    public List<Transform> noteCanvasButtons; // the buttons we can press

    private Vector3 spawnPos = new Vector3(0, 1000, 0); // pos to spawn the beats
    private float spawnBeat; // beat we start spawning notes
    private float previousBeat;

    public GameObject judgementLine;
    public GameObject timer;


    void Start()
    {
        if (!conductorController) conductorController = ConductorController.Instance;
        if (!conductorController) return;

        spawnBeat = conductorController.songPositionInBeats;
        previousBeat = GetCurrentBeat();
        if (conductorController.playGame) PopulateStoredNotes();
    }

    private void PopulateStoredNotes()
    {
        if (NoteList.Count == 0 || !notePrefab || !noteParent) { Debug.LogWarning("No notes to play or cannot spawn them"); return; }

        for (int i = 0; i < NoteList.Count; i++)
        {
            NoteData ndata = NoteList[i];
            Transform noteClone = Instantiate(notePrefab, noteParent);
            Vector3 lanePosition = noteCanvasButtons[(int)ndata.buttonType].position;
            noteClone.position = lanePosition + spawnPos;
            spawnedNotesToPlay.Add(noteClone);
            previousNoteY.Add(noteClone.position.y);
            crossingLogged.Add(false);
        }
    }

    void Update()
    {
        if (!conductorController || !judgementLine) return;

        float currentBeat = GetCurrentBeat();
        timer.GetComponent<TextMeshProUGUI>().text = $"{currentBeat}";

        if (spawnedNotesToPlay.Count > 0)
            for (int i = 0; i < spawnedNotesToPlay.Count; i++)
            {
                NoteData noteData = NoteList[i];
                float targetBeat = noteData.timeStamp;
                float progress = targetBeat <= spawnBeat
                    ? 1f
                    : Mathf.InverseLerp(spawnBeat, targetBeat, currentBeat);
                Vector3 lanePosition = noteCanvasButtons[(int)noteData.buttonType].position;
                Vector3 spawnPosition = lanePosition + spawnPos;
                Vector3 targetPosition = new Vector3(
                    lanePosition.x,
                    judgementLine.transform.position.y,
                    lanePosition.z);
                float oldY = previousNoteY[i];
                spawnedNotesToPlay[i].position = Vector3.Lerp(
                    spawnPosition,
                    targetPosition,
                    progress);

                float currentY = spawnedNotesToPlay[i].position.y;
                float lineY = judgementLine.transform.position.y;
                if (!crossingLogged[i] && oldY > lineY && currentY <= lineY)
                {
                    float crossingFraction = (oldY - lineY) / (oldY - currentY);
                    float crossingBeat = Mathf.Lerp(previousBeat, currentBeat, crossingFraction);
                    Debug.Log(
                        $"Note crossing | index={i} | timestamp={targetBeat:F4} beats | " +
                        $"crossingBeat={crossingBeat:F4} | error={crossingBeat - targetBeat:+0.0000;-0.0000;0.0000} beats | " +
                        $"currentBeat={currentBeat:F4} | frame={Time.frameCount} | " +
                        $"noteY={currentY:F3} | lineY={lineY:F3} | BPM={conductorController.songBpm:F2} | " +
                        $"notePosition={spawnedNotesToPlay[i].position}");
                    crossingLogged[i] = true;
                }
                previousNoteY[i] = currentY;
            }

        previousBeat = currentBeat;
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

    private float GetCurrentBeat()
    {
        return conductorController.GetSongPositionInBeats();
    }


    // cannot attach parameterized function to game object, attaching as event listener
    public void saveButtonData(int buttonId, KeyCode keyCode)
    {
        ButtonData press = new ButtonData
        {
            buttonId = buttonId,
            beatPressed = GetCurrentBeat(),
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