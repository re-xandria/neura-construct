using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Heading : VisualElement
{

    public VisualElement Root { get; set; }

    public Label _label;

    public string text
    {
        get => _label?.text;
        set { if (_label != null) _label.text = value; }
    }

}