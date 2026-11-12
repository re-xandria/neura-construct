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

    public Transform notePrefab; // the prefab to spawn that reflects the note for players
    public Transform noteParent; // where to spawn the notes under
    public List<Transform> noteCanvasButtons; // the buttons we can press

    private Vector3 spawnPos = new Vector3(0, 500, 0); // pos to spawn the beats
    private float spawnBeat; // beat we start spawning notes
    private float previousBeat;

    public GameObject judgementLine;
    public GameObject timer;

    private float delayTime;

    void Start()
    {
        if (!conductorController) conductorController = ConductorController.Instance;
        if (!conductorController) return;

        spawnBeat = conductorController.songPositionInBeats;
        previousBeat = GetCurrentBeat();

        delayTime = conductorController.secPerBeat;

        if (conductorController.playGame) PopulateStoredNotes();
    }

    private void PopulateStoredNotes()
    {
        if (NoteList.Count == 0 || !notePrefab || !noteParent) { Debug.LogWarning("No notes to play or cannot spawn them"); return; }

        for (int i = 0; i < NoteList.Count; i++)
        {
            NoteData ndata = NoteList[i];
            Transform noteClone = null;
            // Transform noteClone = Instantiate(notePrefab, noteParent);
            // Vector3 lanePosition = laneButton.position;
            // noteClone.position = lanePosition + spawnPos;
            // NoteController noteController = noteClone.GetComponent<NoteController>();
            // if (noteController) noteController.InitializeLane(ndata, laneButton);
            spawnedNotesToPlay.Add(noteClone);
            previousNoteY.Add(0f);
            crossingLogged.Add(false);
        }
    }

    void Update()
    {
        if (!conductorController || !judgementLine) return;

        float currentBeat = GetCurrentBeat();
        
        var timerText = timer.GetComponent<TextMeshProUGUI>();
        timerText.text = currentBeat.ToString("F2");


        float travelSeconds = 2f;
        float travelBeats = travelSeconds / conductorController.secPerBeat;
        NoteData noteToDelete = null;

        if (spawnedNotesToPlay.Count > 0)
            for (int i = 0; i < spawnedNotesToPlay.Count; i++)
            {
                NoteData noteData = NoteList[i];
                float noteStartBeat = noteData.timeStamp - travelBeats;

                if (crossingLogged[i] && currentBeat >= noteData.timeStamp + delayTime)
                {
                    noteToDelete = noteData;
                }

                if (spawnedNotesToPlay[i] == null)
                {
                    if (currentBeat < noteStartBeat) continue;

                    spawnedNotesToPlay[i] = InstantiateNote(noteData);
                    previousNoteY[i] = spawnedNotesToPlay[i].position.y;
                }

                else if (currentBeat >= noteStartBeat) // only let beats spawn such that they reach the line in travelSeconds amount of time
                {
                    float targetBeat = noteData.timeStamp;
                    float progress = Mathf.Clamp01((currentBeat - noteStartBeat) / travelBeats);
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
                    // Debug.Log(
                    //     $"Note placement | frame={Time.frameCount} | noteIndex={i} | " +
                    //     $"buttonType={noteData.buttonType} | " +
                    //     $"laneButton={noteCanvasButtons[(int)noteData.buttonType].name} | " +
                    //     $"laneX={lanePosition.x:F3} | noteX={spawnedNotesToPlay[i].position.x:F3}");

                    float currentY = spawnedNotesToPlay[i].position.y;
                    float lineY = judgementLine.transform.position.y;
                    if (oldY > lineY && currentY <= lineY)
                    {
                        float crossingFraction = (oldY - lineY) / (oldY - currentY);
                        float crossingBeat = Mathf.Lerp(previousBeat, currentBeat, crossingFraction);
                        // Debug.Log(
                        //     $"Note crossing | index={i} | timestamp={targetBeat:F4} beats | " +
                        //     $"crossingBeat={crossingBeat:F4} | error={crossingBeat - targetBeat:+0.0000;-0.0000;0.0000} beats | " +
                        //     $"currentBeat={currentBeat:F4} | frame={Time.frameCount} | " +
                        //     $"noteY={currentY:F3} | lineY={lineY:F3} | BPM={conductorController.songBpm:F2} | " +
                        //     $"notePosition={spawnedNotesToPlay[i].position}");
                        crossingLogged[i] = true;
                    }
                    previousNoteY[i] = currentY;
                }

            }

        previousBeat = currentBeat;
        DeleteNote(noteToDelete);
    }

    public void StoreNoteData(NoteData _newNote)
    {
        if (_newNote == null || !conductorController) return;

        if (conductorController.recordAudio)
            NoteList.Add(_newNote);
    }

    private float GetCurrentBeat()
    {
        return conductorController.GetSongPositionInBeats();
    }

    private Transform InstantiateNote(NoteData ndata)
    {
        Transform noteClone = Instantiate(notePrefab, noteParent);
        Transform laneButton = noteCanvasButtons[(int)ndata.buttonType];
        Vector3 lanePosition = laneButton.position;
        noteClone.position = lanePosition + spawnPos;
        NoteController noteController = noteClone.GetComponent<NoteController>();
        if (noteController) noteController.InitializeLane(ndata, laneButton);
        return noteClone;
    }

    public void DeleteNote(NoteData noteData)
    {
        int index = NoteList.IndexOf(noteData);
        if (index >= 0)
        {
            Destroy(spawnedNotesToPlay[index].gameObject);
            crossingLogged.RemoveAt(index);
            previousNoteY.RemoveAt(index);
            spawnedNotesToPlay.RemoveAt(index);
            NoteList.RemoveAt(index);
        }
    }


    public NoteController GetClone(int noteId)
    {
        if (spawnedNotesToPlay[noteId] != null) return spawnedNotesToPlay[noteId].GetComponent<NoteController>();
        return (NoteController)null;
    }

    private void OnApplicationQuit()
    {
        if (conductorController.recordAudio)
        {
            string filePath = Path.Combine(Application.dataPath, $"Scripts/LevelData/RecordedLevel.json");
            NoteList noteListWrapper = new NoteList();
            noteListWrapper.noteList = NoteList;
            string noteJson = JsonUtility.ToJson(noteListWrapper, true); // 2nd arg to format json nicely
            Debug.Log(noteJson);
            File.WriteAllText(filePath, noteJson);
        }
    }

}