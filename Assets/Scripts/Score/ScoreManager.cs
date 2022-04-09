using UnityEngine;
public class ScoreManager : MonoBehaviour
{

    public delegate void AddScore(int _score);
    public static AddScore scoreEvent;

    public static int currentScore;

    private void OnEnable()
    {
        scoreEvent += UpdateScore;
    }
    private void UpdateScore(int _score)
    {
        currentScore += _score;
        UIManager.updateScore(currentScore);
    }
    

    private void OnDisable()
    {
        scoreEvent -= UpdateScore;
    }
}
