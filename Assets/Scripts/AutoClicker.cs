using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AutoClicker : MonoBehaviour
{
    public TextMeshProUGUI autoClickerText;
    public Button autoClickerButton;

    private int autoClickers = 0;
    private float autoClickerInterval = 1f;
    private CoinManager coinManager;

    void Start()
    {
        coinManager = CoinManager.Instance;
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

    public void ResetAutoClickers() // ✅ Теперь автокликеры сбрасываются при ResetProgress()
    {
        autoClickers = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        autoClickerText.text = $"AutoClickers: {autoClickers}";
    }
}

