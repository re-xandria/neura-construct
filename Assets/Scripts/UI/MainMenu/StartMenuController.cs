using UnityEngine;
using UnityEngine.UIElements;

public class StartMenuController : ScreenBase
{
    public LevelSelectManager levelSelectManager;
    
    // void Start()
    // {
    //     levelSelectManager = GameObject.Find("LevelSelectManager").GetComponent<LevelSelectManager>();
    //     cursorManager = GameObject.Find("CursorManager").GetComponent<CursorUI>();

    //     VisualElement root = GetComponent<UIDocument>().rootVisualElement;
    //     VisualElement startButton = root.Q("startButton");
    //     startButton.RegisterCallback<ClickEvent, string>(levelSelectManager.StartLevel, "Level1");
    // }

    // We need to trigger a delay when switching canvases, for animations and sound fx
    protected override void OnEnable()
    {
        base.OnEnable();

        SmallButtons["START GAME"].OnClick = () => levelSelectManager.StartLevel("Level1");
        SmallButtons["RECORD LEVEL"].OnClick = () => levelSelectManager.RecordLevel();
        SmallButtons["EXIT GAME"].OnClick = () => Application.Quit();
    }

}