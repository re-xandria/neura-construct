using System;
using UnityEngine;
using UnityEngine.UIElements;


public class Heading2 : Heading
{
    // may not need these files but can use later for additional logic
    public Heading2(VisualElement element)
    {
        Root = element;
        _label = Root.Q<Label>();
    }

}