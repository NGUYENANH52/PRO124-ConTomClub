using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int currentExperience;
    public int currentCoin;

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        // Cập nhật UI hoặc thực hiện các hành động khác khi nhận được EXP
        Debug.Log("Current EXP: " + currentExperience);
    }
    //public void Coins(int amount) 
    //{
    //    currentCoin += amount;

    //}
}
