﻿using UnityEngine;
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
        Debug.Log("Hướng di chuyển: " + direction);
        // Tính toán vị trí mới
        Vector2 newPosition = _rb.position + direction * speed * Time.fixedDeltaTime;

        // Di chuyển quái vật đến vị trí mới
        _rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        // Kiểm tra nếu quái vật chạm vào đối tượng có tag "Castle"
        if (collision.CompareTag("Castle"))
        {
            Debug.Log("Quái vật chạm vào thành trì");
            isAttacking = true;
            StartCoroutine(AttackCastle(collision.GetComponent<CastleHealth>()));
        }
    }

    IEnumerator AttackCastle(CastleHealth castleHealth)
    {
        while (isAttacking)
        {
            Debug.Log("Quai vat tan cong thanh tri");
            // Gây sát thương lên thành trì
            castleHealth.TakeDamage(damage);

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

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            Debug.Log("Quái vật ngừng tấn công thành trì"); // Kiểm tra xem có ngừng tấn công hay không
            isAttacking = false;
            StopCoroutine(AttackCastle(collision.GetComponent<CastleHealth>()));
        }
    }
}
