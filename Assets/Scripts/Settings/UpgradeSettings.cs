using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "Settings/UpgradeSettings")]
public class UpgradeSettings : ScriptableObject
{
    [Header("Улучшения")]
    public int baseUpgradeCost = 10;
    public int upgradeBonusPerLevel = 1;
}
