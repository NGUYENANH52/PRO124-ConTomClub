using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData; // Dữ liệu của quái vật

    private Rigidbody2D _rb;
    private Animator _animator;
    private int currentHealth;
    private float originalSpeed;
    private ScoreManager scoreManager;
    private PlayerExperience playerExperience;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D của quái vật
        _animator = GetComponent<Animator>(); // Lấy thành phần Animator của quái vật
        currentHealth = enemyData.health; // Khởi tạo máu hiện tại của quái vật
        originalSpeed = enemyData.speed;
        scoreManager = FindObjectOfType<ScoreManager>();
        playerExperience = FindObjectOfType<PlayerExperience>();
    }

    void FixedUpdate()
    {
 
            MoveDown();

    }

    void MoveDown()
    {
        // Di chuyển thẳng xuống theo trục y
        Vector2 newPosition = _rb.position + Vector2.down * enemyData.speed * Time.fixedDeltaTime;

        // Di chuyển quái vật đến vị trí mới
        _rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu quái vật chạm vào đối tượng có tag "Castle"
        if (collision.CompareTag("Castle"))
        {
            Debug.Log("Quái vật chạm vào thành trì");
            AttackCastle(collision.GetComponent<CastleHealth>());
        }
    }

    void AttackCastle(CastleHealth castleHealth)
    {
        Debug.Log("Quái vật tấn công thành trì");
        // Gây sát thương lên thành trì
        castleHealth.TakeDamage(enemyData.damage);

        // Gọi hiệu ứng nổ
        Instantiate(enemyData.explosionEffect, transform.position, transform.rotation);

        // Hủy quái vật
        Destroy(gameObject);
    }

    public void TakeDamage(int incomingDamage)
    {
        int actualDamage = Mathf.Max(incomingDamage - enemyData.armor, 0);
        currentHealth -= actualDamage;
        Debug.Log("Quái vật nhận " + actualDamage + " sát thương. Máu hiện tại: " + currentHealth);
        if (currentHealth <= 0)
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore(enemyData.scoreValue);
            }
            if (playerExperience != null)
            {
                playerExperience.AddExperience(enemyData.expValue);
            }

            Instantiate(enemyData.explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    public IEnumerator SlowDown(float duration , float slowDownfactor)
    {
        enemyData.speed *= slowDownfactor;       
        yield return new WaitForSeconds(duration); // cho doi torng khonag thoi gian
        enemyData.speed = originalSpeed; // khoi phuc toc do ban dau
    }
 public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
