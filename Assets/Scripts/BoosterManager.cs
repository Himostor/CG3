using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoosterManager : MonoBehaviour
{
    public Button boosterButton;
    public TextMeshProUGUI boosterText;

    private bool isBoosterActive = false;
    private float boosterDuration = 10f;
    private CoinManager coinManager;

    void Start()
    {
        coinManager = CoinManager.Instance;
        boosterButton.onClick.AddListener(ActivateBooster);
    }

    public void ActivateBooster()
    {
        if (!isBoosterActive)
        {
            isBoosterActive = true;
            coinManager.ActivateBooster(true);
            boosterText.text = "Booster Active!";
            Invoke(nameof(DeactivateBooster), boosterDuration);
        }
    }

    void DeactivateBooster()
    {
        isBoosterActive = false;
        coinManager.ActivateBooster(false);
        boosterText.text = "Booster Ready!";
    }
}

