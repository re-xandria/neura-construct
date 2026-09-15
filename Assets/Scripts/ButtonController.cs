using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public KeyCode _Key;

    private Button _button;
    private Image image;

    void Awake()
    {
        _button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            // If we attach an onClick function to button it will be invoked
            _button.onClick.Invoke();
            print("Button was pressed!");
            image.color = Color.coral;
        }

        if (Input.GetKeyUp(_Key))
        {
            image.color = Color.white;
        }
    }

}
