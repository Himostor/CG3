using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Доход")]
    public int tapCoins = 1;
    public int baseAutoClickerIncome = 1;
}
