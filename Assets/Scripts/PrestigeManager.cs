using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrestigeManager : MonoBehaviour, IResettable
{
    public Button prestigeButton;
    public TextMeshProUGUI prestigeText;
    public PrestigeSettings prestigeSettings; // Используем PrestigeSettings вместо GameSettings

    private int prestigePoints = 0;
    private CoinManager coinManager;

    void Start()
    {
        coinManager = CoinManager.Instance;
        prestigeButton?.onClick.AddListener(Prestige);
        LoadPrestige();
        UpdateUI();
    }

    void Prestige()
    {
        // Изменены ссылки на prestigeSettings вместо gameSettings
        if (coinManager.GetCoins() >= prestigeSettings.prestigeThreshold)
        {
            int prestigeGained = coinManager.GetCoins() / prestigeSettings.prestigeThreshold;
            prestigePoints += prestigeGained * prestigeSettings.prestigeBonusPoints;

            coinManager.SetCoins(0);
            coinManager.IncreaseCoinsPerClick(prestigeGained * prestigeSettings.extraClickBonus);

            SavePrestige();
            UpdateUI();
        }
    }

    public void ResetProgress()
    {
        prestigePoints = 0;
        SavePrestige();
        UpdateUI();
    }

    void UpdateUI()
    {
        prestigeText.text = $"Prestige Points: {prestigePoints}";
    }

    void SavePrestige()
    {
        PlayerPrefs.SetInt("PrestigePoints", prestigePoints);
        PlayerPrefs.Save();
    }

    void LoadPrestige()
    {
        prestigePoints = PlayerPrefs.GetInt("PrestigePoints", 0);
        UpdateUI();
    }
}

