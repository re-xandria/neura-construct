using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public int buttonId;
    public KeyCode _Key;
    public LevelManager levelManager;

    private Button _button;
    private Image image;
    private NoteData currentNote;

    // eventually update to the new input system

    void Start()
    {
        _button = GetComponent<Button>();
        image = GetComponent<Image>();
        // add eventlistener here
        _button.onClick.AddListener(() => levelManager.saveButtonData(buttonId, _Key));
    }
    

    void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            // If we attach an onClick function to button it will be invoked
            _button.onClick.Invoke();
            image.color = Color.coral;

            currentNote = new NoteData((NoteData.BUTTONTYPE)buttonId, levelManager.NoteList.Count, Time.time - levelManager.startTime);
        }

        if (Input.GetKeyUp(_Key))
        {
            image.color = Color.white;

            if (currentNote != null) currentNote.holdTime = Time.time - currentNote.timeStamp;
            levelManager.StoreNoteData(currentNote);
            currentNote = null;
        }
    }

}


[System.Serializable]
public class NoteData
{
    public enum BUTTONTYPE { One, Two, Three, Four}
    public BUTTONTYPE buttonType; // the type of button (either shape, or location)
    public int buttonId; // the number this button is along the total number of buttons in the list
    public float timeStamp; // when this button was pressed from start
    public float holdTime; // leave at zero if you just press, but if hold, then say how long to hold for (counts on release)

    public NoteData(BUTTONTYPE _btnType, int _btnId, float _timeStmp, float _holdTime = 0)
    {
        buttonType = _btnType;
        buttonId = _btnId;
        timeStamp = _timeStmp;
        holdTime = _holdTime;
    }

}

