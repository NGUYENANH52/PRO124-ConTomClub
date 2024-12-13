using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData; // Dữ liệu của quái vật

    private Rigidbody2D _rb;
    private Animator _animator;
<<<<<<< HEAD
    private bool isAttacking = false;
    public Scoredata diemluu;
=======
    private int currentHealth;
>>>>>>> origin/Final

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D của quái vật
<<<<<<< HEAD
        _animator = GetComponent<Animator>();// Lấy thahf phần Animator của quái vật 
        _animator.SetBool("Enemy_1_Run", true);//Dặt animotion mặc định là chạy bộ

        

=======
        _animator = GetComponent<Animator>(); // Lấy thành phần Animator của quái vật
        _animator.SetBool("isRun", true); // Đặt animation mặc định là đi bộ
        currentHealth = enemyData.health; // Khởi tạo máu hiện tại của quái vật
>>>>>>> origin/Final
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
            
            // Hủy quái vật khi máu giảm xuống 0
<<<<<<< HEAD
            Destroy(gameObject);
            diemluu.score++;    
=======
            Destroy(gameObject);           
>>>>>>> origin/Final
        }
    }
 public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
