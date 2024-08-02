using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public ScoreData scoreData;
    public TMP_Text scoreText;

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
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        scoreData.score += points;
        if (scoreData.score > scoreData.highScore)
        {
            scoreData.highScore = scoreData.score;
            //scoreData.SaveHighScore();
        }
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        scoreData.score = 0;
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreData.score;
        }
    }

    //internal int GetHighScore()
    //{
    //    throw new NotImplementedException();
    //}

    //internal void SaveHighScore()
    //{
    //    throw new NotImplementedException();
    //}
}