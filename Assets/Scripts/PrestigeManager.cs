using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrestigeManager : MonoBehaviour
{
    public Button prestigeButton;
    public TextMeshProUGUI prestigeText;
    public GameSettings gameSettings;

    private int prestigePoints = 0;
    private CoinManager coinManager;
    private UpgradeManager upgradeManager;
    private AutoClicker autoClicker;

    void Start()
    {
        coinManager = CoinManager.Instance;
        upgradeManager = FindObjectOfType<UpgradeManager>();
        autoClicker = FindObjectOfType<AutoClicker>();

        if (prestigeButton != null)
            prestigeButton.onClick.AddListener(Prestige);

        LoadPrestige();
        UpdateUI();
    }

    void Prestige()
    {
        if (coinManager.GetCoins() >= gameSettings.prestigeThreshold)
        {
            int prestigeGained = coinManager.GetCoins() / gameSettings.prestigeThreshold;
            prestigePoints += prestigeGained;

            coinManager.SetCoins(0);
            upgradeManager.ResetUpgrades();
            autoClicker.ResetAutoClickers();

            coinManager.IncreaseCoinsPerClick(Mathf.RoundToInt(prestigePoints * gameSettings.prestigeMultiplier));

            SavePrestige();
            UpdateUI();
        }
    }

    public void ResetPrestige() // ✅ Теперь престиж тоже сбрасывается
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

