using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class NoteController : MonoBehaviour
{

    public float targetBeat;
    private Vector3 spawnPosition;
    private float spawnBeat;
    private float travelBeats;

    public ConductorController conductorController;
    public GameObject judgementLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosition = transform.position;
        spawnBeat = conductorController.songPositionInBeats;
        travelBeats = targetBeat - spawnBeat;

        if (travelBeats <= 0f)
        {
            Debug.LogError($"Note targetBeat must be after its spawn beat. Spawn: {spawnBeat}, target: {targetBeat}");
            enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float currBeat = Mathf.FloorToInt(conductorController.songPositionInBeats);
        float progress = Mathf.InverseLerp(spawnBeat, targetBeat, currBeat);

        transform.position = Vector3.Lerp(
            spawnPosition,
            judgementLine.transform.position,
            progress
        );
        
    }
}

// Note gets to the beat at the intended time but stops
// Note does not move smoothly, moves x distance on each beat
// How can we get the beat to
