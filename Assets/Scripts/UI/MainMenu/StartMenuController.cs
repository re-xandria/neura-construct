using UnityEngine;
using UnityEngine.UIElements;

public class StartMenuController : ScreenBase
{
    public UIManager uiManager;
    
    // void Start()
    // {
    //     UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    //     cursorManager = GameObject.Find("CursorManager").GetComponent<CursorUI>();

    //     VisualElement root = GetComponent<UIDocument>().rootVisualElement;
    //     VisualElement startButton = root.Q("startButton");
    //     startButton.RegisterCallback<ClickEvent, string>(UIManager.StartLevel, "Level1");
    // }

    // We need to trigger a delay when switching canvases, for animations and sound fx
    protected override void OnEnable()
    {
        base.OnEnable();

        SmallButtons["START GAME"].OnClick = () => uiManager.StartLevel("Level1");
        SmallButtons["RECORD LEVEL"].OnClick = () => uiManager.RecordLevel();
        SmallButtons["EXIT GAME"].OnClick = () => Application.Quit();
    }

}