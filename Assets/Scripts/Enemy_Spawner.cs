using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab của quái vật
    [SerializeField] private Transform spawnPoint; // Điểm xuất hiện của quái vật
    [SerializeField] private int totalEnemies = 50; // Tổng số lượng quái vật cần sinh
    [SerializeField] private float spawnInterval = 2f; // Thời gian giữa mỗi lần sinh quái vật (2 giây)
    [SerializeField] private float spawnDistance = 1f; // Khoảng cách giữa các quái vật khi sinh

    private int spawnedEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < totalEnemies)
        {
            for (int i = 0; i < 1 && spawnedEnemies < totalEnemies; i++)
            {
                SpawnEnemy(i);
                spawnedEnemies++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(int index)
    {
        // Tạo vị trí mới để sinh quái vật
        Vector3 spawnPosition = spawnPoint.position + new Vector3(index * spawnDistance, 0, 0);
        Instantiate(enemyPrefab, spawnPosition, spawnPoint.rotation);
    }
}
