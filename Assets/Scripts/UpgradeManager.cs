using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public Button upgradeButton;
    public TextMeshProUGUI upgradeText;
    public UpgradeSettings upgradeSettings; // ✅ Используем UpgradeSettings

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

        if (upgradeText == null)
        {
            Debug.LogError("UpgradeManager: upgradeText не привязан в Inspector!");
            return;
        }

        if (upgradeButton == null)
        {
            Debug.LogError("UpgradeManager: upgradeButton не привязан в Inspector!");
            return;
        }

        baseUpgradeCost = upgradeSettings.baseUpgradeCost; // ✅ Загружаем базовую стоимость апгрейдов

        upgradeButton.onClick.AddListener(BuyUpgrade);
        LoadUpgrade();
        UpdateUI();
    }

    void BuyUpgrade()
    {
        int cost = baseUpgradeCost * upgradeLevel; // ✅ Используем корректную стоимость
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

    public void ResetUpgrades()
    {
        upgradeLevel = 1; // ✅ Теперь сбрасывает правильно
        upgradeBonus = 0;
        baseUpgradeCost = upgradeSettings.baseUpgradeCost; // ✅ Восстанавливаем стоимость из UpgradeSettings
        SaveUpgrade();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (upgradeText == null)
        {
            Debug.LogError("UpgradeManager: upgradeText не привязан в Inspector!");
            return;
        }

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
        baseUpgradeCost = upgradeSettings.baseUpgradeCost; // ✅ Загружаем базовую стоимость
    }
}

