using System;
using UnityEngine;
using UnityEngine.UIElements;


public class Heading5 : Heading
{
    // may not need these files but can use later for additional logic
    public Heading5(VisualElement element)
    {
        Root = element;
        _label = Root.Q<Label>();
    }
}