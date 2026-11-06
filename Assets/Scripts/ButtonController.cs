using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public NoteData.BUTTONTYPE buttonType;
    public KeyCode _Key;
    public LevelManager levelManager;
    public ConductorController conductorController;

    private Button _button;
    private Image image;
    private NoteData currentNote;

    // eventually update to the new input system

    void Start()
    {
        _button = GetComponent<Button>();
        image = GetComponent<Image>();
        // add eventlistener here
        // _button.onClick.AddListener(() => levelManager.saveButtonData(buttonId, _Key));
    }
    

    void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            // If we attach an onClick function to button it will be invoked
            _button.onClick.Invoke();
            image.color = Color.coral;

            currentNote = new NoteData(
                buttonType,
                levelManager.NoteList.Count,
                GetCurrentBeat());
        }

        if (Input.GetKeyUp(_Key))
        {
            image.color = Color.white;

            if (currentNote != null) currentNote.holdTime = GetCurrentBeat() - currentNote.timeStamp;
            levelManager.StoreNoteData(currentNote);
            currentNote = null;
        }
    }

    private float GetCurrentBeat()
    {
        return conductorController.GetSongPositionInBeats();
    }

    public void handleButtonPress(NoteData noteData) 
    {
        noteData.playerPressedAt = GetCurrentBeat();
        toggleCollision(noteData);
        return;
    }

    public void toggleCollision(NoteData noteData)
    {
        noteData.isColliding = !noteData.isColliding;
        return;
    }


}


[System.Serializable]
public class NoteData
{
    public enum BUTTONTYPE { One, Two, Three, Four}
    public BUTTONTYPE buttonType; // the type of button (either shape, or location)
    public int buttonId; // the number this button is along the total number of buttons in the list
    public float timeStamp; // Beat at which the button was pressed, relative to song start.
    public float holdTime; // leave at zero if you just press, but if hold, then say how long to hold for (counts on release)
    public float playerPressedAt; // Beat at which the player pressed the button, relative to song start.
    public bool isColliding; // Whether the note is currently colliding with the button.

    public NoteData(BUTTONTYPE _btnType, int _btnId, float _timeStmp, float _holdTime = 0, float _playerPressedAt = 0, bool _isCol = false)
    {
        buttonType = _btnType;
        buttonId = _btnId;
        timeStamp = _timeStmp;
        holdTime = _holdTime;
        playerPressedAt = _playerPressedAt;
        isColliding = _isCol;
    }

}
