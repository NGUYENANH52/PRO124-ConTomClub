using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform castle; // Vị trí của thành trì
    [SerializeField] private float speed; // Tốc độ di chuyển của quái vật
    [SerializeField] private int damage; // Lượng sát thương gây ra cho thành trì
    [SerializeField] private float attackRate; // Tốc độ đánh của quái vật (đòn đánh mỗi giây)
    [SerializeField] private int health; // Máu của quái vật
    [SerializeField] private int armor; // Giáp của quái vật

    private Rigidbody2D _rb;
    private bool isAttacking = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D của quái vật
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            MoveToCastle();
        }
    }

    void MoveToCastle()
    {
        // Tính toán vector hướng từ vị trí hiện tại đến thành trì
        Vector2 direction = (castle.position - transform.position).normalized;

        // Tính toán vị trí mới
        Vector2 newPosition = _rb.position + direction * speed * Time.deltaTime;

        // Di chuyển quái vật đến vị trí mới
        _rb.MovePosition(newPosition);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu quái vật chạm vào thành trì
        if (collision.transform == castle)
        {
            isAttacking = true;
            StartCoroutine(AttackCastle());
        }
    }

    IEnumerator AttackCastle()
    {
        while (isAttacking)
        {
            // Gây sát thương lên thành trì
            castle.GetComponent<CastleHealth>().TakeDamage(damage);

            // Chờ đợi theo tốc độ đánh
            yield return new WaitForSeconds(1f / attackRate);
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        int actualDamage = Mathf.Max(incomingDamage - armor, 0);
        health -= actualDamage;
        if (health <= 0)
        {
            // Hủy quái vật khi máu giảm xuống 0
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == castle)
        {
            isAttacking = false;
            StopCoroutine(AttackCastle());
        }
    }
}
