using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float explosionDuration = 1f; // Thời gian tồn tại của hiệu ứng nổ

    void Start()
    {
        // Hủy đối tượng sau một khoảng thời gian
        Destroy(gameObject, explosionDuration);
    }
}
