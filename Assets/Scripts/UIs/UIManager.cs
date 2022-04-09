using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public delegate void UpdateScore(int _score);
    public static UpdateScore updateScore;

    public delegate void GameOver();
    public static GameOver gameOver;

    public Text scoreText;

    public GameObject gameOverPanel;
    private void OnEnable()
    {
        gameOver += ActivateGameOverPanel;
        updateScore += ScoreChange;
    }

    void ScoreChange(int _score)
    {
        scoreText.text = string.Format("Score: {0}", _score.ToString());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ActivateGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        ScoreManager.currentScore = 0;
    }
    private void OnDisable()
    {
        updateScore -= ScoreChange;
        gameOver -= ActivateGameOverPanel;
    }

}
