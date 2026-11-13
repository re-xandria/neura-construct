using System.Collections.Generic;
using System.Linq;                // Required for .Where() and .ToList()

using UnityEngine;

public class NoteController : MonoBehaviour
{
    public LevelManager levelManager;

    public bool legacyNotebehavior;

    public Transform laneButton;
    public NoteData ndata;

    public void InitializeLane(NoteData noteData, Transform buttonTransform)
    {
        ndata = noteData;
        laneButton = buttonTransform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // levelManager = GameObject.Find("LevelManagerPrefab(Clone)").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // how can we ensure only 1 note is removed each button press?
    // the collision is active for all notes in the collider area
    // maybe we can only set isColliding as true if no other note in that lane has the value as true

    void OnCollisionEnter2D(Collision2D button)
    {
        ButtonController collidedButton = button.collider.GetComponentInParent<ButtonController>();
        if (!collidedButton) return;

        ndata.isColliding = true;
    }

    // we should get rid of this callback and instead add an input handler on press of the key that only deletes the note nearest the judgement ine
    // but how can we pass the note data to the button?
    // maybe in ButtonController Update() we can keep checking NoteList
    // we get the notes where isColliding is true, and call handle button press and pass in the note data

    // void OnCollisionStay2D(Collision2D button)
    // {
    //     ButtonController collidedButton = button.collider.GetComponentInParent<ButtonController>();

    //     // This is an expensive operation to perform each frame for each note, need to find a way to improve
    //     // List<NoteData> notesOfSameLane = levelManager.NoteList.Where(n => n.buttonType == collidedButton.buttonType).ToList();
    //     // bool areOtherNotesColliding = notesOfSameLane.Where(n => n.isColliding == true).ToList().Count > 0 ? true : false; 

    //     // ndata.isColliding = !areOtherNotesColliding ?  true : false;
    //     if (collidedButton != null) collidedButton.HandleButtonPress(ndata);
    // }

}
