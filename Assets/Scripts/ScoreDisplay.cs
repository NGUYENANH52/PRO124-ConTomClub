using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private void Start()
    {
        // Đảm bảo rằng ScoreManager đã được tham chiếu
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager == null)
            {
                Debug.LogError("ScoreManager is not assigned or found in the scene.");
                return;
            }
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + scoreManager.scoreData.score;
        highScoreText.text = "High Score: " + scoreManager.scoreData.highScore;
    }
}
