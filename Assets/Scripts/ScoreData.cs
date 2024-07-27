using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "GameData/ScoreData",order = 2)]
public class ScoreData : ScriptableObject
{
    public int score;
    public int highScore;
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore",highScore);
        PlayerPrefs.Save();
    }
    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
