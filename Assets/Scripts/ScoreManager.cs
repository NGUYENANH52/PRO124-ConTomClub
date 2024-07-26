using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ScoreData scoreData;

    private void Start()
    {
        // Đảm bảo rằng ScoreData đã được tham chiếu
        if (scoreData == null)
        {
            Debug.LogError("ScoreData is not assigned in ScoreManager.");
            return;
        }

        // Load high score khi bắt đầu trò chơi
        //scoreData.LoadHighScore();
    }

    public void AddScore(int points)
    {
        scoreData.score += points;
        if (scoreData.score > scoreData.highScore)
        {
            scoreData.highScore = scoreData.score;
            //scoreData.SaveHighScore();
        }
    }

    public void ResetScore()
    {
        scoreData.score = 0;
    }

    internal int GetHighScore()
    {
        throw new NotImplementedException();
    }
}