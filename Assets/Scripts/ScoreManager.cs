using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    private int streak = 0;
    private int score = 0;

    [System.NonSerialized] public Heading4 scoreElement;
    [System.NonSerialized] public Heading5 streakElement;
    [System.NonSerialized] public Heading3 hitRatingElement;

    private ScoreSystem scoreSystem = new ScoreSystem();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(float pressedAt, float timeStamp)
    {

        if (pressedAt == 0)
        {
            score+= scoreSystem.missed;
            hitRatingElement.text = "Missed";
            Debug.Log($"pressedAt {pressedAt} and timeStamp {timeStamp} | MISSED");
            return;
        }
        // calc diff between beat player pressed and actual note time stamp
        float timeDiff = Mathf.Abs(pressedAt - timeStamp);
    
        if (timeDiff < 0.1f) {
            score += scoreSystem.perfect;
            hitRatingElement.text = "Perfect!";
            Debug.Log($"pressedAt {pressedAt} and timeStamp {timeStamp} | PERFECT");
        }
        else if (timeDiff < 0.25f) {
            score += scoreSystem.okay;
            hitRatingElement.text = "Okay";
            Debug.Log($"pressedAt {pressedAt} and timeStamp {timeStamp} | OKAY");
        }
        else {
            score += scoreSystem.missed;
            hitRatingElement.text = "Missed";
            Debug.Log($"pressedAt {pressedAt} and timeStamp {timeStamp} | MISSED");
        }

        scoreElement.text = score.ToString("D6");
        streakElement.text = $"1.{streak.ToString("D3")}";
    }
}

[System.Serializable]
public class ScoreSystem
{
    public enum scoreCat { Perfect, Okay, Missed }
    public int perfect;
    public int okay;
    public int missed;

    public ScoreSystem(int _perfect = 10, int _okay = 5, int _missed = 0)
    {
        perfect = _perfect;
        okay = _okay;
        missed = _missed;
    }

}