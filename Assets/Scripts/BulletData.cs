using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType
{
    Normal,
    Slow
}

[CreateAssetMenu(fileName = "BulletData",menuName = "GameData/BulletData",order = 10)]
public class BulletData : ScriptableObject 
{
    public string enemyName;
    public float speed;
    public float lifetime;
    public int damage;
    public GameObject expolsionEffect;
    public float slowDownDuration;
    public BulletType bulletType; // Thêm loại đạn

}
