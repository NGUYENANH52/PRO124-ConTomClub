using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData",menuName = "GameData/BulletData",order = 10)]
public class BulletData : ScriptableObject 
{
    public float speed;
    public float lifetime;
    public int damage;
    public GameObject expolsionEffect;
    public float slowDownDuration;// Thoi gian lam cham
    public float slowDownFactor; // he so lam cham ( vi du 0,5 de giam toc (50%)
}
