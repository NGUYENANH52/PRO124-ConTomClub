using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "GameData/ScoreData",order = 2)]
public class ScoreData : ScriptableObject
{
    public int score;
    public int highScore;

}
