using UnityEngine;

[CreateAssetMenu(fileName = "QuestSettings", menuName = "Settings/QuestSettings")]
public class QuestSettings : ScriptableObject
{
    [Header("Квесты")]
    public int baseQuestTarget = 100;
    public int baseQuestRewardGems = 5;
    public float questDifficultyMultiplier = 2f;
    public int questRewardIncrease = 5;
}
