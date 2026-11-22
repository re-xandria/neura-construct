using UnityEngine;
using UnityEngine.UIElements;

public class LevelCanvasController : ScreenBase
{
    private ScoreManager scoreManager;

    protected override void OnEnable()
    {
        base.OnEnable();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        VisualElement root = uiDocument.rootVisualElement;

        HandleScore();
        HandleStreak();
        HandleHitRating();

        // only use this for non-gameplay visuals, its easier to use the canvas
        // export other icons as pngs and place over the canvas elements, rewire the other elements
    }
    
    // find the elements and add event listener to scoremanager
    private void HandleScore()
    {
        scoreManager.scoreElement = Headings4["000000"];
    }

    private void HandleStreak()
    {
        scoreManager.streakElement = Headings5["1.xxx"];
    }
    
    private void HandleHitRating()
    {
        scoreManager.hitRatingElement = Headings3["Perfect!"];
    }
}