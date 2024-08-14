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
    private Coroutine slowCoroutine;
    private Coroutine burnCoroutine;
    [SerializeField] private BulletData bulletData;
    //Poison_Bullet
    private bool isPoisoned = false; // Đánh dấu nếu quái vật đang bị độc
    private float poisonEndTime;
    private float poisonDamage;
    private Coroutine poisonCoroutine;
    //Hiệu ứng
    [SerializeField] private Transform effectPosition;
    [SerializeField] private GameObject fireEffectSprite;
    [SerializeField] private GameObject iceEffectSprite;
    [SerializeField] private GameObject poisonEffectSprite;
    private GameObject currentFireEffect; // Để lưu trữ hiệu ứng hiện tại
    private GameObject currentIceEffect; // Để lưu trữ hiệu ứng hiện tại
    private GameObject currentPoisonEffect; // Để lưu trữ hiệu ứng hiện tại
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
        // Đầu tiên, thử giảm sát thương bằng giáp
        if (enemyData.armor > 0)
        {
            int damageToArmor = Mathf.Min(incomingDamage, enemyData.armor);
            enemyData.armor -= damageToArmor;
            incomingDamage -= damageToArmor;
        }

        // Sát thương còn lại sẽ được áp dụng vào máu
        if (incomingDamage > 0)
        {
            currentHealth -= incomingDamage;
        }

        Debug.Log("Quái vật nhận " + incomingDamage + " sát thương. Máu hiện tại: " + currentHealth + ", Giáp còn: " + enemyData.armor);

        // Kiểm tra nếu quái vật đã chết
        if (currentHealth <= 0)
        {
            StopSlow();
            StopBurning();
            StopPoison();

            if (scoreManager != null)
            {
                scoreManager.AddScore(enemyData.scoreValue);
            }
            if (playerExperience != null)
            {
                playerExperience.AddExperience(enemyData.expValue);
            }

            if (enemySpawner != null)
            {
                enemySpawner.OnEnemyDestroyed();
            }
            DropCoins();
            Instantiate(enemyData.explosionEffectCoin, transform.position, transform.rotation);
            Instantiate(enemyData.explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
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
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
            if (slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
            }
            if (poisonCoroutine != null)
            {
                StopCoroutine(poisonCoroutine);
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
            if (slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
            }
            if (poisonCoroutine != null)
            {
                StopCoroutine(poisonCoroutine);
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
    public void StartPoison(float damagePercentage, float poisonDuration, float delay, float slowDownPercentage, float slowDuration)
    {
        if (!isPoisoned)
        {
            poisonDamage = Mathf.RoundToInt(enemyData.health * damagePercentage);
            poisonEndTime = Time.time + poisonDuration;
            isPoisoned = true;

            // Dừng các hiệu ứng đốt và làm chậm hiện có để tránh xung đột
            StopBurning();
            StopSlow();

            // Bắt đầu gây sát thương độc
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
            if (slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
            }
            if (poisonCoroutine != null)
            {
                StopCoroutine(poisonCoroutine);
            }
            poisonCoroutine = StartCoroutine(PoisonDamageCoroutine(delay));

            // Chỉ hiển thị hiệu ứng độc
            poisonEffectSprite.transform.position = effectPosition.position;
            currentPoisonEffect = Instantiate(poisonEffectSprite, effectPosition.position, Quaternion.identity);
            currentPoisonEffect.transform.SetParent(effectPosition);

            // Áp dụng hiệu ứng làm chậm mà không hiển thị hiệu ứng băng
            currentSpeed = enemyData.originalSpeed * (1 - slowDownPercentage / 100f);
            isSlowedDown = true;
            slowDownEndTime = Time.time + slowDuration;
            if (slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
            }

            // Áp dụng sát thương đốt nhưng không hiển thị hiệu ứng lửa
            burnDamage = Mathf.RoundToInt(enemyData.health * bulletData.burnDamagePercentage);
            burnEndTime = Time.time + poisonDuration;
            isBurning = true;
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
            burnCoroutine = StartCoroutine(BurnDamageCoroutine());
        }
    }

    // Thêm Coroutine để xử lý sát thương độc
    private IEnumerator PoisonDamageCoroutine(float delay)
    {
        while (isPoisoned && Time.time < poisonEndTime && currentHealth > 0)
        {
            float poisonDamage = Mathf.RoundToInt(enemyData.health * bulletData.poisonDamagePercentage);
            TakeDamage((int)poisonDamage);
            Debug.Log("Quái vật nhận " + poisonDamage + " sát thương độc. Máu hiện tại: " + currentHealth);
            yield return new WaitForSeconds(delay);
        }
        StopPoison(); // Kết thúc quá trình độc
    }

    // Thêm phương thức StopPoison
    private void StopPoison()
    {
        isPoisoned = false;
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
        }
        // Hủy hiệu ứng độc
        if (currentPoisonEffect != null)
        {
            Destroy(currentPoisonEffect);
        }
    }
}
