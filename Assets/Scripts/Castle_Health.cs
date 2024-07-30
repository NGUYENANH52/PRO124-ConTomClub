using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{
    [SerializeField] private int health; // Máu của thành trì
    private int currentHealth;
    [SerializeField] private Slider healthSlider;
    

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

        if (currentHealth <= 0)
        {
            // Xử lý khi thành trì bị phá hủy
            Destroy(gameObject);
        }
    }

    private void UpdateHealthUI()
    {
      
        healthSlider.value = (float)currentHealth / 1 ;
    }
}
