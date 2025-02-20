using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    public TextMeshProUGUI coinText;
    public GameSettings gameSettings;

    private int catCoins = 0;
    private int coinsPerClick;
    private bool isBoosterActive = false;
    private QuestManager questManager;

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
        LoadCoins();
        questManager = FindObjectOfType<QuestManager>();
        coinsPerClick = gameSettings.tapCoins;
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        catCoins += isBoosterActive ? amount * 2 : amount;
        SaveCoins();
        UpdateUI();
        questManager?.CheckQuestProgress();
    }

    public bool HasEnoughCoins(int amount) => catCoins >= amount;

    public void SpendCoins(int amount)
    {
        if (HasEnoughCoins(amount))
        {
            catCoins -= amount;
            SaveCoins();
            UpdateUI();
            questManager?.CheckQuestProgress();
        }
    }

    public int GetCoins() => catCoins;

    public int GetCoinsPerClick() => coinsPerClick;

    public void SetCoins(int amount)
    {
        catCoins = amount;
        SaveCoins();
        UpdateUI();
        questManager?.CheckQuestProgress();
    }

    public void ResetCoins()
    {
        SetCoins(0);
        coinsPerClick = gameSettings.tapCoins;
        SaveCoins();
        UpdateUI();
    }

    public void ActivateBooster(bool active)
    {
        isBoosterActive = active;
    }

    void UpdateUI()
    {
        if (coinText) coinText.text = "CatCoins: " + catCoins;
    }

    void SaveCoins()
    {
        PlayerPrefs.SetInt("CatCoins", catCoins);
        PlayerPrefs.SetInt("CoinsPerClick", coinsPerClick);
        PlayerPrefs.Save();
    }

    void LoadCoins()
    {
        catCoins = PlayerPrefs.GetInt("CatCoins", 0);
        coinsPerClick = gameSettings.tapCoins;
    }

    public void IncreaseCoinsPerClick(int amount) // Метод для увеличения монет за клик
    {
        coinsPerClick += amount;
        PlayerPrefs.SetInt("CoinsPerClick", coinsPerClick);
        PlayerPrefs.Save();
        UpdateUI();
    }
}

