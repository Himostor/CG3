using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour, IResettable
{
    public Button upgradeButton;
    public TextMeshProUGUI upgradeText;
    public UpgradeSettings upgradeSettings;

    private CoinManager coinManager;
    private int upgradeLevel;
    private int upgradeBonus;
    private int baseUpgradeCost;

    void Start()
    {
        coinManager = CoinManager.Instance;

        if (upgradeSettings == null)
        {
            Debug.LogError("UpgradeManager: UpgradeSettings не привязан в Inspector!");
            return;
        }

        baseUpgradeCost = upgradeSettings.baseUpgradeCost;

        upgradeButton.onClick.AddListener(BuyUpgrade);
        LoadUpgrade();
        UpdateUI();
    }

    void BuyUpgrade()
    {
        int cost = baseUpgradeCost * upgradeLevel;
        if (coinManager.HasEnoughCoins(cost))
        {
            coinManager.SpendCoins(cost);
            upgradeLevel++;
            upgradeBonus += upgradeSettings.upgradeBonusPerLevel;
            coinManager.IncreaseCoinsPerClick(upgradeBonus);
            SaveUpgrade();
            UpdateUI();
        }
    }

    public void ResetProgress()
    {
        upgradeLevel = 1;
        upgradeBonus = 0;
        baseUpgradeCost = upgradeSettings.baseUpgradeCost;
        SaveUpgrade();
        UpdateUI();
    }

    void UpdateUI()
    {
        upgradeText.text = $"Upgrade: {baseUpgradeCost * upgradeLevel} CatCoins\n+{upgradeBonus} per click";
    }

    void SaveUpgrade()
    {
        PlayerPrefs.SetInt("UpgradeLevel", upgradeLevel);
        PlayerPrefs.SetInt("UpgradeBonus", upgradeBonus);
        PlayerPrefs.Save();
    }

    void LoadUpgrade()
    {
        upgradeLevel = PlayerPrefs.GetInt("UpgradeLevel", 1);
        upgradeBonus = PlayerPrefs.GetInt("UpgradeBonus", 0);
        baseUpgradeCost = upgradeSettings.baseUpgradeCost;
    }
}
