using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class LevelEndController : ScreenBase
{
    private ScoreManager scoreManager;
    private readonly UIManager uiManager;

    protected override void OnEnable()
    {
        base.OnEnable();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        Headings5["000000"].text = "this one!";

        SmallButtons["NEXT LEVEL"].OnClick = () => uiManager.ShowMainMenu();
    }
}