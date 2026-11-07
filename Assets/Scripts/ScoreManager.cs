using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int streak = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class scoreSystem
{
    public enum scoreCat { Perfect, Okay, Missed }
    public float perfect;
    public float okay;
    public float missed;

    public scoreSystem(float _perfect = 10, float _okay = 5, float _missed = 0)
    {
        perfect = _perfect;
        okay = _okay;
        missed = _missed;
    }

}