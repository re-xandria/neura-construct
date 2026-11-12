using System.Collections.Generic;
using System.Linq;                // Required for .Where() and .ToList()
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public NoteData.BUTTONTYPE buttonType;
    public KeyCode _Key;
    public LevelManager levelManager;
    public ConductorController conductorController;
    public ScoreManager scoreManager;

    private Button _button;
    private Image image;
    private NoteData currentNote;

    void Start()
    {
        _button = GetComponent<Button>();
        image = GetComponent<Image>();
    }


    void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            image.color = Color.coral;

            if (conductorController.recordAudio)
            {
                currentNote = new NoteData(
                    buttonType,
                    levelManager.NoteList.Count,
                    GetCurrentBeat());
            }
        }

        if (Input.GetKeyUp(_Key))
        {
            image.color = Color.white;

            if (conductorController.recordAudio)
            {
                if (currentNote != null)
                {
                    currentNote.holdTime = GetCurrentBeat() - currentNote.timeStamp;
                    if (levelManager.NoteList.Where(n => n.noteId == currentNote.noteId).ToList().Count > 0)
                    {
                        currentNote.noteId = levelManager.NoteList.Count;
                        levelManager.StoreNoteData(currentNote);
                    }
                    else levelManager.StoreNoteData(currentNote);
                }
                currentNote = null;
            }
        }
    }

    private float GetCurrentBeat()
    {
        return conductorController.GetSongPositionInBeats();
    }

    public void HandleButtonPress(NoteData ndata)
    {
        // List<NoteData> notesOfSameLane = levelManager.NoteList.Where(n => n.buttonType == pressedButton.buttonType).ToList();
        if (ndata != null && ndata.isColliding && Input.GetKeyDown(_Key) /*&& notesOfSameLane[0] == ndata*/)
        Debug.Log($"This button is being deleted: {ndata.noteId}");
        {
            ndata.playerPressedAt = conductorController.GetSongPositionInBeats();
            // Debug.Log($"Player Pressed At: " + ndata.playerPressedAt);
            scoreManager.UpdateScore(ndata.playerPressedAt, ndata.timeStamp);
            ndata.isColliding = false;
            levelManager.DeleteNote(ndata);
            return;
        }
    }

}


[System.Serializable]
public class NoteData
{
    public enum BUTTONTYPE { One, Two, Three, Four }
    public BUTTONTYPE buttonType; // the type of button (either shape, or location)
    public int noteId; // the number this button is along the total number of buttons in the list
    public float timeStamp; // Beat at which the button was pressed, relative to song start.
    public float holdTime; // leave at zero if you just press, but if hold, then say how long to hold for (counts on release)
    public float playerPressedAt; // Beat at which the player pressed the button, relative to song start.
    public bool isColliding; // Whether the note is currently colliding with the button.

    public NoteData(BUTTONTYPE _btnType, int _noteId, float _timeStmp, float _holdTime = 0, float _playerPressedAt = 0, bool _isCol = false)
    {
        buttonType = _btnType;
        noteId = _noteId;
        timeStamp = _timeStmp;
        holdTime = _holdTime;
        playerPressedAt = _playerPressedAt;
        isColliding = _isCol;
    }

}

[System.Serializable]
public class NoteList
{
    public List<NoteData> noteList;

}
