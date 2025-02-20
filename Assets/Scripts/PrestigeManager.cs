using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrestigeManager : MonoBehaviour, IResettable
{
    public Button prestigeButton;
    public TextMeshProUGUI prestigeText;
    public GameSettings gameSettings;
    
    public int prestigeBonusPoints = 5; // Сколько очков престижа дается за престиж
    public int extraClickBonus = 2; // Сколько доп. кликов дается за престиж

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
        if (coinManager.GetCoins() >= gameSettings.prestigeThreshold)
        {
            int prestigeGained = coinManager.GetCoins() / gameSettings.prestigeThreshold;
            prestigePoints += prestigeGained * prestigeBonusPoints;

            coinManager.SetCoins(0);
            coinManager.IncreaseCoinsPerClick(prestigeGained * extraClickBonus);

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


