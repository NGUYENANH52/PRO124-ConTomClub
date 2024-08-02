using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class bulletScript : MonoBehaviour
{
    [SerializeField] private BulletData bulletData; 
    Rigidbody2D _rb;
    //private EnemyData enemyData;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D
        Destroy(gameObject, bulletData.lifetime); // Hủy viên đạn sau một khoảng thời gian      
    }

    void Update()
    {
       
        
            // Di chuyển viên đạn theo trục y
            _rb.velocity = transform.up * bulletData.speed * Time.deltaTime;
        
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Va chạm phát hiện với: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Va chạm với quái!");

            // Gây sát thương cho quái
            EnemyMovement enemy = other.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                if (bulletData.bulletType == BulletType.Normal)
                {
                    enemy.TakeDamage(bulletData.damage);
                }
                else if (bulletData.bulletType == BulletType.Slow)
                {
                    enemy.TakeDamage(bulletData.damage);
                    enemy.SlowDown(bulletData.slowDownDuration);
                }
            }


            Destroy(gameObject);
            GameObject effectExplore = Instantiate(bulletData.expolsionEffect, transform.position, Quaternion.identity);
            Destroy(effectExplore, 0.1f);

          
        }
    }

}
