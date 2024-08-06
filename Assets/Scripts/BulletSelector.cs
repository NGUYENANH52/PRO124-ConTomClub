using UnityEngine;
using UnityEngine.UI;

public class BulletSelector : MonoBehaviour
{
    public BulletManager bulletManager;
    public Button[] bulletButtons; // Các nút UI để chọn loại đạn

    void Start()
    {
        // Đăng ký sự kiện cho các nút
        for (int i = 0; i < bulletButtons.Length; i++)
        {
            int index = i; // Sao chép giá trị của i vào biến index
            bulletButtons[i].onClick.AddListener(() => SelectBullet(index));
        }
    }

    public void SelectBullet(int index)
    {
        bulletManager.ChangeBullet(index);
    }
}

