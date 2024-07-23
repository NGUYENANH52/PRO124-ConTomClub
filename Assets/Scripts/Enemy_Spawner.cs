using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab của quái vật
    [SerializeField] private Transform spawnPoint; // Điểm xuất hiện của quái vật
    [SerializeField] private int totalEnemies = 1; // Tổng số lượng quái vật cần sinh
    [SerializeField] private float spawnInterval = 1f; // Thời gian giữa mỗi lần sinh quái vật

    private int spawnedEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < totalEnemies)
        {
            SpawnEnemy();
            spawnedEnemies++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
