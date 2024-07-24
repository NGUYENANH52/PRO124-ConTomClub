using UnityEngine;
using System.Collections;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float speed; // Tốc độ di chuyển của quái vật
    [SerializeField] private int damage; // Lượng sát thương gây ra cho thành trì
    [SerializeField] private float attackRate; // Tốc độ đánh của quái vật (đòn đánh mỗi giây)
    [SerializeField] private int health; // Máu của quái vật
    [SerializeField] private int armor; // Giáp của quái vật
    [SerializeField] private GameObject explosionEffect;

    private Rigidbody2D _rb;
    private Animator _animator;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D của quái vật
        _animator = GetComponent<Animator>();// Lấy thahf phần Animator của quái vật 
        _animator.SetBool("isRun", true);//Dặt animotion mặc định là chạy bộ

    }

    void FixedUpdate()
    {

        MoveDown();
    }

    void MoveDown()
    {
        // Di chuyển thẳng xuống theo trục y
        Vector2 newPosition = _rb.position + Vector2.down * speed * Time.fixedDeltaTime;

        // Di chuyển quái vật đến vị trí mới
        _rb.MovePosition(newPosition);       
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
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
        castleHealth.TakeDamage(damage);

        // Gọi hiệu ứng nổ
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Hủy quái vật
        Destroy(gameObject);
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

}
