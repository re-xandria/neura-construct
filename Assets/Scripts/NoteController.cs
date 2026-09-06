using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class NoteController : MonoBehaviour
{

    public float targetBeat = 15;
    public float speed;

    private GameObject notePrefab;
    public ConductorController conductorController;
    public GameObject judgementLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        notePrefab = gameObject;
        float distance = Vector3.Distance(notePrefab.transform.position, judgementLine.transform.position);
        speed = distance / targetBeat * 0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        int currBeat = Mathf.FloorToInt(conductorController.songPositionInBeats);
        notePrefab.transform.position = new Vector3(notePrefab.transform.position.x, notePrefab.transform.position.y - speed, notePrefab.transform.position.z);
        
        if (conductorController.lastBeat != currBeat)
        {
        }
    }
}

// Notes are spawned X amount of time before the beat they need to be at the judgement line
// Velocity of the notes should be determined by the time they spawn and the time they need to reach the judgement line
// Notes need to update based on beat/song time, not frames
// Speed should equal the distance until judgement line divided by time until judgement line FROM INITIAL SPAWN

// The current speed calculation is inaccurate
// The note needs to arrive at the line at the targetBeat
// Maybe we can do duration + song time like we did for flashUntil
// Speed would be determined by when the note spawns and how long it has to reach the line at the target beat
