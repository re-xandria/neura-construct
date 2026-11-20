using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenBase : MonoBehaviour
{
    private UIDocument uiDocument;
    public CursorUI cursorManager;

    //Update this base class each time we create a new component
    //Consider a dictionary of all components, that way we only need to update one dictionary for changes
    protected readonly Dictionary<string, SmallButton> Buttons = new();

    protected virtual void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        Buttons.Clear();
        var root = uiDocument.rootVisualElement;

        var styleSheet = Resources.Load<StyleSheet>("Styles/SB_Override");
        if (styleSheet != null)
        {
            root.styleSheets.Add(styleSheet);
        }
        
        Debug.Log("Stylesheets " + root.styleSheets.count);

        root.Query<VisualElement>(className: "small-button-default").ForEach(element =>
        {
            var wrapper = new SmallButton(element);
            Debug.Log(wrapper.Text);
            if (!string.IsNullOrEmpty(wrapper.Text))
            {
                Buttons[wrapper.Text] = wrapper;
            }
        });
    }
}