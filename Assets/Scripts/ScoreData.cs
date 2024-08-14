using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "GameData/ScoreData", order = 2)]
public class ScoreData : ScriptableObject
{
    public int score;
    public int highScore;

    // Hàm này sẽ lưu high score của một level cụ thể
    public void SaveHighScore(int level)
    {
        string key = "HighScore_Level_" + level;
        PlayerPrefs.SetInt(key, highScore);
        PlayerPrefs.Save();
    }

    // Hàm này sẽ load high score của một level cụ thể
    public void LoadHighScore(int level)
    {
        string key = "HighScore_Level_" + level;
        highScore = PlayerPrefs.GetInt(key, 0);
    }
}