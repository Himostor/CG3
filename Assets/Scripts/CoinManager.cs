using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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

    public void ResetCoins()
    {
        catCoins = 0;
        coinsPerClick = gameSettings.tapCoins;
        SaveCoins();
        UpdateUI();
        Debug.Log("Монеты сброшены");
    }

    void UpdateUI()
    {
        if (coinText)
            coinText.text = "CatCoins: " + catCoins;
        else
            Debug.LogWarning("coinText не назначен в инспекторе!");
    }

    void SaveCoins()
    {
        PlayerPrefs.SetInt("CatCoins", catCoins);
        PlayerPrefs.SetInt("CoinsPerClick", coinsPerClick);
        PlayerPrefs.Save();
        Debug.Log("Сохранены данные: CatCoins=" + catCoins + ", CoinsPerClick=" + coinsPerClick);
    }

    void LoadCoins()
    {
        catCoins = PlayerPrefs.GetInt("CatCoins", 0);
        coinsPerClick = gameSettings.tapCoins;
        Debug.Log("Загружены данные: CatCoins=" + catCoins + ", CoinsPerClick=" + coinsPerClick);
    }
}
