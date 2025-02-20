using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }

    private CoinManager coinManager;
    private QuestManager questManager;
    private UpgradeManager upgradeManager;
    private PrestigeManager prestigeManager;
    private AutoClicker autoClicker;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        questManager = FindObjectOfType<QuestManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
        prestigeManager = FindObjectOfType<PrestigeManager>();
        autoClicker = FindObjectOfType<AutoClicker>(); // ✅ Теперь учитываем автокликеры
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        coinManager?.ResetCoins();
        questManager?.ResetQuests();
        upgradeManager?.ResetUpgrades();
        prestigeManager?.ResetPrestige();
        autoClicker?.ResetAutoClickers(); // ✅ Теперь сбрасывает автокликеры

        Debug.Log("Игровой прогресс сброшен!");
    }
}

