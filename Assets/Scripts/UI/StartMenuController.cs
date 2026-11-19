using UnityEngine;
using UnityEngine.UIElements;

public class StartMenuController : MonoBehaviour
{
    public LevelSelectManager levelSelectManager;
    public CursorUI cursorManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelSelectManager = GameObject.Find("LevelSelectManager").GetComponent<LevelSelectManager>();
        cursorManager = GameObject.Find("CursorManager").GetComponent<CursorUI>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement startButton = root.Q("startButton");
        startButton.RegisterCallback<ClickEvent, string>(levelSelectManager.StartLevel, "Level1");
        // can also assign the 3rd variation of the button on click with a delay before initiating screen transition animation

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
