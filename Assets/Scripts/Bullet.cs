﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class bulletScript : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bulletData.lifetime);
        // update 
        bulletData = FindObjectOfType<BulletManager>().GetCurrentBulletData(); // Lấy dữ liệu đạn hiện tại từ BulletManager
    }

    void FixedUpdate()
    {
        //_rb.velocity = transform.up * bulletData.speed * Time.deltaTime;
        _rb.AddForce(transform.up * bulletData.speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
    // update
    public void Initialize(BulletData data)
    {
        bulletData = data;
        Destroy(gameObject, bulletData.lifetime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Va chạm phát hiện với: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Va chạm với quái!");

            EnemyMovement enemy = other.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                switch (bulletData.bulletType)
                {
                    case BulletType.Normal: // Đạn thường
                        enemy.TakeDamage(bulletData.damage);
                        break;
                    case BulletType.Ice_Bullet: // Đạn băng
                        enemy.TakeDamage(bulletData.damage);
                        enemy.StartSlow(bulletData.slowDownPercentage, bulletData.slowDownDuration);
                        break;
                    case BulletType.Fire_Bullet: // Loại đạn lửa
                        enemy.TakeDamage(bulletData.damage);
                        enemy.StartBurning(bulletData.burnDamagePercentage, bulletData.burnDuration);
                        break;
                    case BulletType.Poison_Bullet: // Đạn độc
                        enemy.TakeDamage(bulletData.damage);
                        enemy.StartPoison(bulletData.poisonDamagePercentage, bulletData.poisonDuration, bulletData.poisonDelay, bulletData.slowDownPercentage, bulletData.slowDownDuration);
                        break;
                }
            }

            Destroy(gameObject);
            GameObject effectExplore = Instantiate(bulletData.expolsionEffect, transform.position, Quaternion.identity);
            Destroy(effectExplore, 0.1f);
        }
    }
}
