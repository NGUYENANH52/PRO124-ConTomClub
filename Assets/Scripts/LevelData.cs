using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData")]
public class LevelData : ScriptableObject
{
    public int[] experienceToLevelUp; // Mảng chứa EXP yêu cầu cho mỗi cấp độ
    public float levelMultiplier = 1.5f; // Hệ số tăng EXP yêu cầu sau mỗi lần lên cấp

    // Bạn có thể thêm các thuộc tính khác liên quan đến cấp độ nếu cần
}
