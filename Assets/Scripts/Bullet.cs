using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class bulletScript : MonoBehaviour
{
    public float _speed;
    public float _lifeTime;
    public GameObject _effectBullet;
    public int _damage; // Biến lưu trữ sát thương của viên đạn
    public ScoreData diemLuu; // Thêm biến ScoreData
    public TMP_Text scoreText; // Thêm biến TMP_Text để hiển thị điểm số
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, _lifeTime);
    }

    void Update()
    {
        _rb.velocity = transform.up * _speed * Time.deltaTime;
        UpdateScoreUI();
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
                enemy.TakeDamage(_damage);
                // Nếu quái vật bị tiêu diệt, cộng thêm điểm và cập nhật UI
                if (enemy.IsDead())
                {
                    diemLuu.score++;
                    UpdateScoreUI();
                }
            }
         

            Destroy(this.gameObject);
            GameObject effectExplore = Instantiate(_effectBullet, transform.position, Quaternion.identity);
            Destroy(effectExplore, 0.1f);

          
        }
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Điểm: " + diemLuu.score;
        }
    }
}
