using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // Mảng các prefab quái vật
    [SerializeField] private Transform spawnArea; // Điểm trung tâm của khu vực sinh quái vật
    [SerializeField] private Vector2 spawnAreaSize; // Kích thước của khu vực sinh quái vật (rộng và cao)
    [SerializeField] private int totalEnemies = 50; // Tổng số lượng quái vật cần sinh
    [SerializeField] private float spawnInterval = 2f; // Thời gian giữa mỗi lần sinh quái vật (2 giây)

    private int spawnedEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < totalEnemies)
        {
            for (int i = 0; i < 5 && spawnedEnemies < totalEnemies; i++)
            {
                SpawnEnemy();
                spawnedEnemies++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Tạo vị trí ngẫu nhiên trong khu vực sinh quái vật
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnArea.position.x - spawnAreaSize.x / 2, spawnArea.position.x + spawnAreaSize.x / 2),
            Random.Range(spawnArea.position.y - spawnAreaSize.y / 2, spawnArea.position.y + spawnAreaSize.y / 2),
            spawnArea.position.z
        );
        Debug.Log("Vị trí sinh quái vật: " + spawnPosition);

        // Chọn ngẫu nhiên một loại quái vật từ mảng enemyPrefabs
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];

        Debug.Log("Sinh quái vật: " + randomEnemyPrefab.name);

        // Sinh quái vật tại vị trí ngẫu nhiên
        Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
    }
}
