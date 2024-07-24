using UnityEngine;

public class CastleHealth : MonoBehaviour
{
    [SerializeField] private int health; // Máu của thành trì

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Thành trì nhận sát thương: " + damage + " Máu còn lại: " + health); // Kiểm tra sát thương và máu còn lại       
            if (health <= 0)
        {
            // Xử lý khi thành trì bị phá hủy (có thể là kết thúc trò chơi hoặc bất kỳ hành động nào khác)
            Destroy(gameObject);
            Debug.Log("Castle Destroyed!");
        }
    }
}
