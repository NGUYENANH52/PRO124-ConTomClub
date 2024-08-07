using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType
{
    Normal,
    Ice_Bullet,
    Fire_Bullet
}

[CreateAssetMenu(fileName = "BulletData",menuName = "GameData/BulletData",order = 10)]
public class BulletData : ScriptableObject 
{
    public string Name;
    public float speed;
    public float lifetime;
    public int damage;
    public GameObject expolsionEffect;
    //Ice_Bullet
    public float slowDownDuration;
    //Fire_Bullet
    public float burnDamagePercentage; // Phần trăm sát thương đốt
    public float burnDuration; // Thời gian đốt
    public float burnDelay; // Thời gian giữa các lần gây sát thương đốt
    //Type_Bullet
    public BulletType bulletType; // Thêm loại đạn
    public GameObject bulletPrefab;
}
