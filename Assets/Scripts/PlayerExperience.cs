using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int currentExperience;
    public int currentLevel = 1;
    public LevelData levelData;

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        Debug.Log("Current EXP: " + currentExperience);

        while (currentLevel < levelData.experienceToLevelUp.Length && currentExperience >= levelData.experienceToLevelUp[currentLevel])
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentExperience -= levelData.experienceToLevelUp[currentLevel];
        currentLevel++;

        Debug.Log("Level Up! New Level: " + currentLevel);
    }
}
