using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AutoClicker : MonoBehaviour, IResettable
{
    public TextMeshProUGUI autoClickerText;
    public Button autoClickerButton;
    public GameSettings gameSettings;

    private int autoClickers = 0;
    private CoinManager coinManager;
    private float autoClickerInterval;

    void Start()
    {
        coinManager = CoinManager.Instance;
        autoClickerInterval = gameSettings.baseAutoClickerIncome;
        autoClickerButton.onClick.AddListener(BuyAutoClicker);
        StartCoroutine(AutoClickCoroutine());
    }

    void BuyAutoClicker()
    {
        int cost = 50 + (autoClickers * 20);
        if (coinManager.HasEnoughCoins(cost))
        {
            coinManager.SpendCoins(cost);
            autoClickers++;
            UpdateUI();
        }
    }

    IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoClickerInterval);
            coinManager.AddCoins(autoClickers);
        }
    }

    public void ResetProgress()
    {
        autoClickers = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        autoClickerText.text = $"AutoClickers: {autoClickers}";
    }
}
