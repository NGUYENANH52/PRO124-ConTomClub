using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public BulletData normalBulletData; // Thông tin đạn thường
    public BulletData iceBulletData;    // Thông tin đạn băng
    public BulletData fireBulletData;   // Thông tin đạn lửa

    private BulletData currentBulletData;

    void Start()
    {
        currentBulletData = normalBulletData; // Mặc định sử dụng đạn thường
    }
        
    public BulletData GetCurrentBulletData()
    {
        return currentBulletData;
    }

    public void ChangeBullet(int index)
    {
        switch (index)
        {
            case 0:
                currentBulletData = normalBulletData;
                break;
            case 1:
                currentBulletData = iceBulletData;
                break;
            case 2:
                currentBulletData = fireBulletData;
                break;
            default:
                Debug.LogWarning("Chỉ số loại đạn không hợp lệ: " + index);
                break;
        }
    }
}
