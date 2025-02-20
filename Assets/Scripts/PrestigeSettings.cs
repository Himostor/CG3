using UnityEngine;

[CreateAssetMenu(fileName = "PrestigeSettings", menuName = "Settings/PrestigeSettings")]
public class PrestigeSettings : ScriptableObject
{
    public int prestigeThreshold = 1000; // Сколько монет нужно для престижа
    public float prestigeMultiplier = 1.2f; // Множитель дохода после престижа
    public int prestigeBonusPoints = 5; // Очки престижа за один престиж
    public int extraClickBonus = 2; // Дополнительные клики за престиж
}
