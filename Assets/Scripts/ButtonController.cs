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
        }

        if (Input.GetKeyUp(_Key))
        {
            image.color = Color.white;
        }
    }

}

