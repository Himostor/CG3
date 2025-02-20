using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel; // ✅ Панель магазина
    public Button openShopButton, closeShopButton;
    public Button buyHatButton, buyShirtButton, buyPantsButton;
    public TextMeshProUGUI hatPriceText, shirtPriceText, pantsPriceText;

    private int hatPrice = 100, shirtPrice = 150, pantsPrice = 200;
    private CoinManager coinManager;
    private WardrobeManager wardrobeManager;

    void Start()
    {
        coinManager = CoinManager.Instance;
        wardrobeManager = FindObjectOfType<WardrobeManager>();

        buyHatButton.onClick.AddListener(BuyHat);
        buyShirtButton.onClick.AddListener(BuyShirt);
        buyPantsButton.onClick.AddListener(BuyPants);
        openShopButton.onClick.AddListener(OpenShop); // ✅ Открытие магазина
        closeShopButton.onClick.AddListener(CloseShop); // ✅ Закрытие магазина

        shopPanel.SetActive(false); // ✅ Изначально скрываем магазин
        UpdateUI();
    }

    void OpenShop() // ✅ Метод открытия магазина
    {
        shopPanel.SetActive(true);
    }

    void CloseShop() // ✅ Метод закрытия магазина
    {
        shopPanel.SetActive(false);
    }

    void BuyHat()
    {
        if (coinManager.HasEnoughCoins(hatPrice))
        {
            coinManager.SpendCoins(hatPrice);
            wardrobeManager.UnlockHat();
            UpdateUI();
        }
    }

    void BuyShirt()
    {
        if (coinManager.HasEnoughCoins(shirtPrice))
        {
            coinManager.SpendCoins(shirtPrice);
            wardrobeManager.UnlockShirt();
            UpdateUI();
        }
    }

    void BuyPants()
    {
        if (coinManager.HasEnoughCoins(pantsPrice))
        {
            coinManager.SpendCoins(pantsPrice);
            wardrobeManager.UnlockPants();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        hatPriceText.text = $"Hat: {hatPrice} CatCoins";
        shirtPriceText.text = $"Shirt: {shirtPrice} CatCoins";
        pantsPriceText.text = $"Pants: {pantsPrice} CatCoins";
    }
}

