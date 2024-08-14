using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData")]
public class LevelData : ScriptableObject
{
    public int[] experienceToLevelUp; // Mảng chứa EXP yêu cầu cho mỗi cấp độ
    

    // Bạn có thể thêm các thuộc tính khác liên quan đến cấp độ nếu cần
}
