using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataEnemy" , menuName = "GameData/DataEnemy")]

public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float speed;
    public float originalSpeed;// Tốc độ gốc của quái vật
    public float currentSpeed;// Tốc độ hiện tại của quái vật
    public int damage;    
    public int health;
    public int armor;
    public GameObject explosionEffect;
    public GameObject explosionEffectCoin;
    public int coinDropAmount; // Số lượng xu rơi ra
    public int scoreValue; // so diem khi tieu diet quai
    public int expValue; // so exp
    public GameObject enemyPrefab;
    private void OnEnable()
    {
        originalSpeed = speed; // Khởi tạo tốc độ ban đầu
        currentSpeed = speed; // Khởi tạo tốc độ hiện tại
    }
}

