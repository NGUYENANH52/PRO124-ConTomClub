﻿using UnityEngine;
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
    private Coroutine slowCoroutine;
    private Coroutine burnCoroutine;
    [SerializeField] private BulletData bulletData;
    //Hiệu ứng
    [SerializeField] private Transform effectPosition;
    [SerializeField] private GameObject fireEffectSprite;
    [SerializeField] private GameObject iceEffectSprite;
    private GameObject currentFireEffect; // Để lưu trữ hiệu ứng hiện tại
    private GameObject currentIceEffect; // Để lưu trữ hiệu ứng hiện tại
    // Wave end
    private EnemySpawner enemySpawner;
    private CoinManager coinManager; // Thêm biến quản lý đồng xu
    public GameObject coinPrefab;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        currentHealth = enemyData.health;
        currentSpeed = enemyData.originalSpeed; // Khởi tạo với tốc độ gốc của quái vật
        scoreManager = FindObjectOfType<ScoreManager>();
        playerExperience = FindObjectOfType<PlayerExperience>();
        coinManager = FindObjectOfType<CoinManager>(); // Tìm đối tượng CoinManager trong scene
        enemySpawner = FindObjectOfType<EnemySpawner>();// Tìm đối tượng EnemySpawner trong scene
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
        // Gọi phương thức OnEnemyDestroyed của EnemySpawner khi quái vật bị tiêu diệt
        if (enemySpawner != null)
        {
            enemySpawner.OnEnemyDestroyed();
        }
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
           
            // Gọi phương thức OnEnemyDestroyed của EnemySpawner khi quái vật bị tiêu diệt
            if (enemySpawner != null)
            {
                enemySpawner.OnEnemyDestroyed();
            }
            DropCoins();
            Instantiate(enemyData.explosionEffectCoin, transform.position, transform.rotation);
            Instantiate(enemyData.explosionEffect, transform.position, transform.rotation);           
            Destroy(gameObject); // Hủy quái vật khi máu <= 0
        }
    }
    void DropCoins()
    {
        int numberOfCoins = enemyData.coinDropAmount; // Số lượng đồng xu rơi ra từ EnemyData
        for (int i = 0; i < numberOfCoins; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        // Cập nhật số lượng xu trong CoinManager
        if (coinManager != null)
        {
            coinManager.AddCoins(numberOfCoins);
        }
    }
    public void StartSlow(float slowDownPercentage, float duration)
    {
        if (!isSlowedDown)
        {
            // Giảm tốc độ theo tỷ lệ phần trăm được truyền vào
            currentSpeed = enemyData.originalSpeed * (1 - slowDownPercentage / 100f);// Đây là phần trăm tốc độ còn lại sau khi bị làm chậm. Với slowDownPercentage = 30, phần còn lại sẽ là 1 - 0.3 = 0.7, tương đương với 70% tốc độ gốc.
            isSlowedDown = true;
            slowDownEndTime = Time.time + duration;

            // Nếu có một Coroutine đang chạy, dừng nó lại
            if (slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
            }
            // Bắt đầu Coroutine làm chậm
            slowCoroutine = StartCoroutine(SlowDownCoroutine(duration));
            iceEffectSprite.transform.position = effectPosition.position;
            //Instantiate ra effect băng
            currentIceEffect = Instantiate(iceEffectSprite, effectPosition.position, Quaternion.identity);
            currentIceEffect.transform.SetParent(effectPosition);
        }
    }

    private IEnumerator SlowDownCoroutine(float duration)
    {
        while (isSlowedDown && Time.time < slowDownEndTime)
        {
            yield return null;
        }
        StopSlow(); // Kết thúc quá trình làm chậm
    }

    public void StopSlow()
    {
        currentSpeed = enemyData.originalSpeed;
        isSlowedDown = false;
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }
        //Hủy effect băng
        if (currentIceEffect != null)
        {
            Destroy(currentIceEffect);
        }
    }
    public void StartBurning(float damagePercentage, float duration)
    {
        if (!isBurning)
        {
            burnDamage = Mathf.RoundToInt(enemyData.health * damagePercentage); // Sử dụng giá trị damagePercentage truyền vào
            burnEndTime = Time.time + duration;
            isBurning = true;
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
            burnCoroutine = StartCoroutine(BurnDamageCoroutine());
            fireEffectSprite.transform.position = effectPosition.position;
            //Instantiate ra effect lửa
            currentFireEffect = Instantiate(fireEffectSprite, effectPosition.position, Quaternion.identity);
            currentFireEffect.transform.SetParent(effectPosition);
        }
    }

    private void StopBurning()
    {
        isBurning = false;
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }
        //Hủy effect lửa
        if (currentFireEffect != null)
        {
            Destroy(currentFireEffect);
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
