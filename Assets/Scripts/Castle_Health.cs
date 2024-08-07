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
        UpdateHealthUI();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthUI();
        if (currentHealth == 0)
        { 
            GameOver(); 
        }
       
    }
      
    private void UpdateHealthUI()
    {
      
        healthSlider.value = (float)currentHealth / 1 ;
    }

    private void GameOver()
    {
        Lose.SetActive(true);
        Time.timeScale = 0;
        
        
    }






}
