using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class bulletScript : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    public ScoreManager scoreManager; // Thêm biến ScoreData
    
    Rigidbody2D _rb;
    //private EnemyData enemyData;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D

        if (_rb == null)
        {
            Debug.LogError("Rigidbody2D is not found on the bullet.");
        }

        Destroy(gameObject, bulletData.lifetime); // Hủy viên đạn sau một khoảng thời gian

        // Tìm đối tượng ScoreManager trong scene
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("Không tìm thấy ScoreManager trong cảnh này.");
        }
    }

    void Update()
    {
        if (_rb != null)
        {
            // Di chuyển viên đạn theo trục y
            _rb.velocity = transform.up * bulletData.speed * Time.deltaTime;
        }
        else
        {
            Debug.LogError("Rigidbody2D có giá trị null trong phương thức Cập nhật.");
        }
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
                enemy.TakeDamage(bulletData.damage);
                StartCoroutine(enemy.SlowDown(bulletData.slowDownDuration, bulletData.slowDownFactor));
                // Nếu quái vật bị tiêu diệt, cộng thêm điểm và cập nhật UI
                if (enemy.IsDead())
                {
                    if(scoreManager != null)
                    {
                        scoreManager.AddScore(1);
                    }
                    
                    
                }
            }
         

            Destroy(gameObject);
            GameObject effectExplore = Instantiate(bulletData.expolsionEffect, transform.position, Quaternion.identity);
            Destroy(effectExplore, 0.1f);

          
        }
    }

}
