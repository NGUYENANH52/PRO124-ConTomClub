using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataEnemy" , menuName = "GameData/DataEnemy")]

public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float speed;
    public int damage;
    public float attackRate;
    public int health;
    public int armor;
    public GameObject explosionEffect;
    public int scoreValue; // so diem khi tieu diet quai
    public int expValue; // so exp

}

