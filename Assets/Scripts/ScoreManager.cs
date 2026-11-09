using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int streak = 0;
    private float score = 0;

    public GameObject scoreBoard;
    private ScoreSystem scoreSystem = new ScoreSystem();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreBoard.GetComponent<TextMeshProUGUI>().text = $"Score: {score}";
    }

    public void UpdateScore(float pressedAt, float timeStamp)
    {
        // calc diff between beat player pressed and actual note time stamp
        float timeDiff = Mathf.Abs(pressedAt - timeStamp);
    
        if (timeDiff < 0.25f) score += scoreSystem.perfect;
        else if (timeDiff < 0.5f) score += scoreSystem.okay;
        else score += scoreSystem.missed;

    }
}

[System.Serializable]
public class ScoreSystem
{
    public enum scoreCat { Perfect, Okay, Missed }
    public float perfect;
    public float okay;
    public float missed;

    public ScoreSystem(float _perfect = 10, float _okay = 5, float _missed = 0)
    {
        perfect = _perfect;
        okay = _okay;
        missed = _missed;
    }

}