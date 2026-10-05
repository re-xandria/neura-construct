using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class NoteController : MonoBehaviour
{

    public float targetBeat;

    private Vector3 spawnPosition;
    private float spawnBeat;
    private float travelBeats;
    private float destroyBeat;

    public ConductorController conductorController;
    public GameObject judgementLine;
    public GameObject destroyLine;

    public bool legacyNotebehavior;

    private ConductorController _ConductorController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ConductorController.Instance) _ConductorController = ConductorController.Instance;

        if (!legacyNotebehavior) return; 

        spawnPosition = transform.position;
        spawnBeat = conductorController.songPositionInBeats;
        travelBeats = targetBeat - spawnBeat;
        destroyBeat = travelBeats + 15;

        if (travelBeats <= 0f)
        {
            Debug.LogError($"Note targetBeat must be after its spawn beat. Spawn: {spawnBeat}, target: {targetBeat}");
            enabled = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate() // each note should be handled
    {
        if (legacyNotebehavior)
        {
            float currBeat = Mathf.FloorToInt(conductorController.songPositionInBeats);

            if (transform.position.y > judgementLine.transform.position.y)
            {
                float progress = Mathf.InverseLerp(spawnBeat, targetBeat, currBeat);

                transform.position = Vector3.Lerp(
                    spawnPosition,
                    judgementLine.transform.position,
                    progress
                );
            }
            else
            {
                float progress = Mathf.InverseLerp(targetBeat, destroyBeat, currBeat);

                transform.position = Vector3.Lerp(
                    judgementLine.transform.position,
                    destroyLine.transform.position,
                    progress
                );
            }
        }
        else
        {
            // we'll do this from the manager
            //transform.Translate(Vector3.down * (_ConductorController.secPerBeat * _ConductorController.songBpm/10));
        }

    }
}

// Note gets to the beat at the intended time but stops
// Note does not move smoothly, moves x distance on each beat
// How can we get the beat to move smoothly towards the line?
// How can we get the beat to continue towards the bottom of the screen after it gets to the line?

// Make a second judgement line that the note must get to after the first one, and delete it from the world when it gets to that line
