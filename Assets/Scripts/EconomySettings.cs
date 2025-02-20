using UnityEngine;

[CreateAssetMenu(fileName = "EconomySettings", menuName = "Settings/EconomySettings")]
public class EconomySettings : ScriptableObject
{
    [Header("Хард-валюта")]
    public int initialGems = 0;
    public int gemRewardForPrestige = 10;
}
