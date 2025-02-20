using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Доход")]
    public int tapCoins = 1;
    public int baseAutoClickerIncome = 1;

    [Header("Престиж")]
    public int prestigeThreshold = 1000;
    public float prestigeMultiplier = 1.2f; // ✅ Теперь множитель настраивается
}
