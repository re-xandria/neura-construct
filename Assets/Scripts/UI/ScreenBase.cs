using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ScreenBase : MonoBehaviour
{
    public UIDocument uiDocument;
    public CursorUI cursorManager;
    protected List<StyleSheet> stylesheets;

    //Update this base class each time we create a new component
    //Consider a dictionary of all components, that way we only need to update one dictionary for changes
    protected Dictionary<string, SmallButton> SmallButtons = new();
    protected Dictionary<string, Heading1> Headings1 = new();
    protected Dictionary<string, Heading2> Headings2 = new();
    protected Dictionary<string, Heading3> Headings3 = new();
    protected Dictionary<string, Heading4> Headings4 = new();
    protected Dictionary<string, Heading5> Headings5 = new();

    void Awake()
    {
        stylesheets = Resources.LoadAll("Styles").OfType<StyleSheet>().ToList();
    }

    protected virtual void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        ClearComponents();
        VisualElement root = uiDocument.rootVisualElement;

        ApplyStyleOverrides(root);
        FindSmallSmallButtons(root);
        FindHeadings(root);
    }

    private void ClearComponents()
    {
        SmallButtons.Clear();
        // Headings1.Clear();
        // Headings2.Clear();
        Headings3.Clear();
        Headings4.Clear();
        Headings5.Clear();

    }

    private void FindSmallSmallButtons(VisualElement root)
    {
        root.Query<VisualElement>(className: "small-button-default").ForEach(element =>
        {
            SmallButton wrapper = new SmallButton(element);
            Debug.Log(wrapper.Text);
            if (!string.IsNullOrEmpty(wrapper.Text))
            {
                SmallButtons[wrapper.Text] = wrapper;
            }
        });
    }

    private void ApplyStyleOverrides(VisualElement root)
    {
        Debug.Log("Stylesheets " + stylesheets.Count);
        foreach (StyleSheet sheet in stylesheets)
        {
            if (sheet != null)
            {
                root.styleSheets.Add(sheet);
            }
        }

    }

    private void FindHeadings(VisualElement root)
    {
        Debug.Log("Heading Elements: " + root.Query<VisualElement>().ToList().Count);
        root.Query<VisualElement>().ForEach(element =>
        {
            if (element.ClassListContains("heading-1"))
            {
                Heading1 wrapper = new Heading1(element);
                if (!string.IsNullOrEmpty(wrapper.text))
                {
                    Headings1[wrapper.text] = wrapper;
                }
            } 
            if (element.ClassListContains("heading-2"))
            {
                Heading2 wrapper = new Heading2(element);
                if (!string.IsNullOrEmpty(wrapper.text))
                {
                    Headings2[wrapper.text] = wrapper;
                }
            }
            if (element.ClassListContains("heading-3"))
            {
                Heading3 wrapper = new Heading3(element);
                if (!string.IsNullOrEmpty(wrapper.text))
                {
                    Headings3[wrapper.text] = wrapper;
                }
            }
            if (element.ClassListContains("heading-4"))
            {
                Heading4 wrapper = new Heading4(element);
                if (!string.IsNullOrEmpty(wrapper.text))
                {
                    Headings4[wrapper.text] = wrapper;
                }
                Debug.Log("Creating Score: " + wrapper);
            }
            if (element.ClassListContains("heading-5"))
            {
                Heading5 wrapper = new Heading5(element);
                if (!string.IsNullOrEmpty(wrapper.text))
                {
                    Headings5[wrapper.text] = wrapper;
                }
            }
        });
    }

}