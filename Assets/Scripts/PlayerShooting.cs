using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public BulletManager bulletManager;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        BulletData currentBulletData = bulletManager.GetCurrentBulletData();
        GameObject bullet = Instantiate(currentBulletData.bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<bulletScript>().Initialize(currentBulletData);
    }
}
