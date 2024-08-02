using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager scoreManager;

    private void Start()
    {
        // Tìm đối tượng ScoreManager trong scene
        scoreManager = FindObjectOfType<ScoreManager>();

        // Reset điểm khi bắt đầu màn chơi mới
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }
    }

    // Giả sử có một phương thức để bắt đầu màn chơi mới
    public void StartNewLevel()
    {
        // Reset điểm khi bắt đầu màn chơi mới
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        // Các hành động khác để bắt đầu màn chơi mới...
    }
    public void StartNewMap()
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.RestoreOriginalSpeed();
        }
    }
    //public void EndGame()
    //{
    //    // Save high score khi kết thúc trò chơi
    //    if (scoreManager != null)
    //    {
    //        scoreManager.SaveHighScore();
    //    }

    //    // Các hành động khác để kết thúc trò chơi...
    //}
}
