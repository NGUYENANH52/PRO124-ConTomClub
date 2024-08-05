using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float currentHealth;
    private ScoreManager scoreManager;
    private PlayerExperience playerExperience;
    //Làm chậm với Ice_Bullet
    private bool isSlowedDown = false;
    private float slowDownEndTime;
    private float currentSpeed; // Tốc độ hiện tại của đối tượng quái vật
    //Đốt với Fire_Bullet
    private bool isBurning = false; // Đánh dấu nếu quái vật đang bị đốt
    private float burnEndTime;
    private float burnDamage;
    private Coroutine burnCoroutine;
    private BulletData bulletData;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        currentHealth = enemyData.health;
        currentSpeed = enemyData.originalSpeed; // Khởi tạo với tốc độ gốc của quái vật
        scoreManager = FindObjectOfType<ScoreManager>();
        playerExperience = FindObjectOfType<PlayerExperience>();
    }

    void FixedUpdate()
    {
        MoveDown();

        if (isSlowedDown && Time.time > slowDownEndTime)
        {
            StopSlow();
        }
    }

    void MoveDown()
    {
        Vector2 newPosition = _rb.position + Vector2.down * currentSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            Debug.Log("Quái vật chạm vào thành trì");
            AttackCastle(collision.GetComponent<CastleHealth>());
        }
    }

    void AttackCastle(CastleHealth castleHealth)
    {
        Debug.Log("Quái vật tấn công thành trì");
        castleHealth.TakeDamage(enemyData.damage);
        Instantiate(enemyData.explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(int incomingDamage)
    {
        int actualDamage = Mathf.Max(incomingDamage - enemyData.armor, 0);
        currentHealth -= actualDamage;
        Debug.Log("Quái vật nhận " + actualDamage + " sát thương. Máu hiện tại: " + currentHealth);

        if (currentHealth <= 0)
        {
            // Dừng quá trình đốt nếu có
            StopSlow();
            StopBurning();

            if (scoreManager != null)
            {
                scoreManager.AddScore(enemyData.scoreValue);
            }
            if (playerExperience != null)
            {
                playerExperience.AddExperience(enemyData.expValue);
            }

            Instantiate(enemyData.explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject); // Hủy quái vật khi máu <= 0
        }
    }

    public void StartSlow(float duration)
    {
        if (!isSlowedDown)
        {
            currentSpeed /= 2; // Giảm tốc độ đi một nửa
            isSlowedDown = true;
            slowDownEndTime = Time.time + duration;  
        }
    }

    public void StopSlow()
    {
        currentSpeed = enemyData.originalSpeed;
        isSlowedDown = false;
    }
    public void StartBurning(float damagePercentage, float duration)
    {
        if (!isBurning)
        {
            bulletData.burnDamagePercentage = damagePercentage;
            burnEndTime = Time.time + duration;
            isBurning = true;
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
            burnCoroutine = StartCoroutine(BurnDamageCoroutine());
        }
    }

    private void StopBurning()
    {
        isBurning = false;
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }
    }

    private IEnumerator BurnDamageCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // Delay 1s before starting to burn damage

        while (isBurning && Time.time < burnEndTime && currentHealth > 0)
        {
            // Tính toán sát thương đốt
            float burnDamage = Mathf.RoundToInt(enemyData.health * bulletData.burnDamagePercentage);
            TakeDamage((int)burnDamage); // Gọi TakeDamage để kiểm tra tình trạng chết của quái vật
            Debug.Log("Quái vật nhận " + burnDamage + " sát thương đốt. Máu hiện tại: " + currentHealth);
            yield return new WaitForSeconds(bulletData.burnDelay); // Thời gian giữa các lần gây sát thương đốt
        }

        StopBurning(); // Kết thúc quá trình đốt
    }

    /*public bool IsDead()
    {
        return currentHealth <= 0;
    }*/
}
