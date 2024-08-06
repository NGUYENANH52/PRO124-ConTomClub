using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveData[] waves;// mang chua du lieu cua cac wave
    [SerializeField] private Transform spawnArea;//Diem turng tam ca khu vuc sinh quai
    [SerializeField] private Vector2 spawnAreaSize; // Kích thước của khu vực sinh quái vật
    [SerializeField] private float timeBetweenWaves;// Thời gian chờ giữa các wave
    [SerializeField] private string winSceneName;
    private int currentWaveIndex = 0;
    private int remainingEnemies = 0; // so luong quai vat con trong game
    private EnemyData enemyData;
    private bool wavesCompleted = false;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentWaveIndex < waves.Length)
        {
            WaveData currentWave = waves[currentWaveIndex];
            yield return StartCoroutine(SpawnWave(currentWave));

            currentWaveIndex++;
            if (currentWaveIndex < waves.Length)
            {
                Debug.Log("Chờ " + timeBetweenWaves + " giây trước khi bắt đầu wave tiếp theo...");
                yield return new WaitForSeconds(timeBetweenWaves);
            }            
            

        }
        wavesCompleted = true;
        Debug.Log("Tất cả các wave đã hoàn thành!");
        StartCoroutine(CheckRemainingEnemies());    
    }
    private IEnumerator SpawnWave(WaveData waveData)
    {
        int maxEnemies = 0;
        for (int i = 0; i < waveData.enemyCounts.Length; i++)
        {
            maxEnemies = Mathf.Max(maxEnemies, waveData.enemyCounts[i]);
        }
        for (int enemyIndex = 0; enemyIndex < maxEnemies; enemyIndex++)
        {
            for (int typeIndex = 0; typeIndex < waveData.enemyTypes.Length; typeIndex++) 
            {
                if (enemyIndex < waveData.enemyCounts[typeIndex])
                {
                    SpawnEnemy(waveData.enemyTypes[typeIndex]);
                }
            }
            yield return new WaitForSeconds(waveData.spawnInterval);
        }
        
    }
    private void SpawnEnemy(EnemyData enemyData)
    {
        // Tạo vị trí ngẫu nhiên trong khu vực sinh quái vật
        Vector3 spawnPosition = new Vector3
        (
            Random.Range(spawnArea.position.x - spawnAreaSize.x / 2, spawnArea.position.x + spawnAreaSize.x / 2),
            Random.Range(spawnArea.position.y - spawnAreaSize.y / 2, spawnArea.position.y + spawnAreaSize.y / 2),
            spawnArea.position.z
        );
        Debug.Log("Vị trí sinh quái vật: " + spawnPosition);

        if (enemyData.enemyPrefab != null)
        {
            Instantiate(enemyData.enemyPrefab, spawnPosition, Quaternion.identity);
            remainingEnemies++;
            Debug.Log("Sinh quái vật: " + enemyData.enemyName);
        }       
    }
    private IEnumerator CheckRemainingEnemies()
    {
        while (remainingEnemies > 0)
        {
            yield return null; // Đợi cho đến khi tất cả quái vật bị tiêu diệt
        }
        if (wavesCompleted && remainingEnemies <= 0) 
        {
            LoadWinScene();
        }
        
    }
    public void OnEnemyDestroyed()
    {
        remainingEnemies--; // Giảm số lượng quái vật hiện tại
        Debug.Log("Số quái vật còn lại: " + remainingEnemies);

        if (wavesCompleted && remainingEnemies <= 0)
        {
            // Nếu không còn quái vật và tất cả các wave đã hoàn thành
            LoadWinScene();
        }
    }
    private void LoadWinScene()
    {
        Time.timeScale = 1;
        Debug.Log("Chuyển sang màn hình chiến thắng...");
        SceneManager.LoadScene(winSceneName);
    }
    
}