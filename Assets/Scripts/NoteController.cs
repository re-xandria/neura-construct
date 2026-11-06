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

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if (ConductorController.Instance) _ConductorController = ConductorController.Instance;

        // if (!legacyNotebehavior) return;

        // spawnPosition = transform.position;
        // spawnBeat = conductorController.songPositionInBeats;
        // travelBeats = targetBeat - spawnBeat;
        // destroyBeat = travelBeats + 15;

        // if (travelBeats <= 0f)
        // {
        //     Debug.LogError($"Note targetBeat must be after its spawn beat. Spawn: {spawnBeat}, target: {targetBeat}");
        //     enabled = false;
        // }

    }

    // Update is called once per frame
    void FixedUpdate() // each note should be handled
    {
        // if (legacyNotebehavior)
        // {
        //     float currBeat = Mathf.FloorToInt(conductorController.songPositionInBeats);

        //     if (transform.position.y > judgementLine.transform.position.y)
        //     {
        //         float progress = Mathf.InverseLerp(spawnBeat, targetBeat, currBeat);

        //         transform.position = Vector3.Lerp(
        //             spawnPosition,
        //             judgementLine.transform.position,
        //             progress
        //         );
        //     }
        //     else
        //     {
        //         float progress = Mathf.InverseLerp(targetBeat, destroyBeat, currBeat);

        //         transform.position = Vector3.Lerp(
        //             judgementLine.transform.position,
        //             destroyLine.transform.position,
        //             progress
        //         );
        //     }
        // }
        // else
        // {
        //     // we'll do this from the manager
        //     // transform.Translate(Vector3.down * (_ConductorController.secPerBeat * _ConductorController.songBpm/10));
        // }

    }

    NoteData.BUTTONTYPE GetButtonType()
    {
        const float noteXPos = this.transform.position.x;
        switch (noteXPos)
        {
            case button1.transform.position.x:
                return NoteData.BUTTONTYPE.One;
            case button2.transform.position.x:
                return NoteData.BUTTONTYPE.Two;
            case button3.transform.position.x:
                return NoteData.BUTTONTYPE.Three;
            case button4.transform.position.x:
                return NoteData.BUTTONTYPE.Four;
            default:
                Debug.LogError($"Note is not aligned with any button.");
                return NoteData.BUTTONTYPE.One; // Return a default value
        }
    }

    void OnTriggerEnter(Collider button)
    {
        NoteData.BUTTONTYPE buttonType = GetButtonType();
        // determine button type based on x position of note
        if (button.collider.buttonType == buttonType) 
        {
            Debug.Log($"Note collided with button {button.collider.buttonType}");
        }
    }
}
