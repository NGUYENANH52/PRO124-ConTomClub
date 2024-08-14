using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CastleHealth : MonoBehaviour
{
    [SerializeField] private int health; // Máu của thành trì
    private int currentHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject Lose;
    private void Start()
    {
        currentHealth = health;
        healthSlider.value = currentHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        if (healthSlider.value <= 0)
        { 
            GameOver(); 
        }
       
    }

    private void GameOver()
    {
        Lose.SetActive(true);
        Time.timeScale = 0; 
    }
}
