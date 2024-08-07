using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="WaveData",menuName ="GameData/WaveData",order = 11)]
public class WaveData : ScriptableObject
{
    public EnemyData[] enemyTypes; // Các loại quái vật trong wave
    public int[] enemyCounts; // Số lượng từng loại quái vật trong wave
    public float spawnInterval; // Thời gian chờ giữa các lần sinh quái trong wave 
}

