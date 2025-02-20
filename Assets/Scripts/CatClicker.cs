using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatClicker : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public Button clickButton;

    private CoinManager coinManager;

    void Start()
    {
        coinManager = CoinManager.Instance;
        if (clickButton != null)
            clickButton.onClick.AddListener(OnCatClicked);
    }

    public void OnCatClicked()
    {
        coinManager.AddCoins(coinManager.GetCoinsPerClick());
    }
}
