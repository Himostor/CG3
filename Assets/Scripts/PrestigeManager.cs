using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrestigeManager : MonoBehaviour, IResettable
{
    public Button prestigeButton;
    public TextMeshProUGUI prestigeText;
    public GameSettings gameSettings;
    public EconomySettings economySettings;

    private int prestigePoints = 0;
    private CoinManager coinManager;
    private UpgradeManager upgradeManager;
    private AutoClicker autoClicker;

    void Start()
    {
        coinManager = CoinManager.Instance;
        upgradeManager = FindObjectOfType<UpgradeManager>();
        autoClicker = FindObjectOfType<AutoClicker>();

        prestigeButton?.onClick.AddListener(Prestige);

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
            autoClicker.ResetProgress();

            coinManager.IncreaseCoinsPerClick(Mathf.RoundToInt(prestigePoints * gameSettings.prestigeMultiplier));

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

