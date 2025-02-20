using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public TextMeshProUGUI gemsText;
    public Button claimQuestRewardButton;
    public QuestSettings questSettings;
    public EconomySettings economySettings;

    private CoinManager coinManager;
    private int questTarget;
    private int questRewardGems;
    private int gems;
    private bool questCompleted;

    void Start()
    {
        coinManager = CoinManager.Instance;
        if (coinManager == null)
        {
            Debug.LogError("CoinManager is null. QuestManager cannot function properly.");
            return;
        }

        if (claimQuestRewardButton != null)
            claimQuestRewardButton.onClick.AddListener(ClaimQuestReward);

        LoadQuest();
        UpdateUI();
    }

    public void CheckQuestProgress()
    {
        if (coinManager.GetCoins() >= questTarget && !questCompleted)
        {
            questCompleted = true;
            questText.text = "✔ Quest Completed! Claim Reward!";

            claimQuestRewardButton.interactable = true;
        }
        else
        {
            questText.text = $"Quest: Earn {questTarget} CatCoins ({coinManager.GetCoins()}/{questTarget})";
        }
    }

    void ClaimQuestReward()
    {
        if (questCompleted)
        {
            gems += questRewardGems + economySettings.gemRewardForPrestige;
            questTarget = Mathf.RoundToInt(questTarget * questSettings.questDifficultyMultiplier);
            questRewardGems += questSettings.questRewardIncrease;
            questCompleted = false;
            claimQuestRewardButton.interactable = false;
            SaveQuest();
            UpdateUI();
        }
    }

    public void ResetQuests()
    {
        questTarget = questSettings.baseQuestTarget;
        questRewardGems = questSettings.baseQuestRewardGems;
        gems = economySettings.initialGems;
        questCompleted = false;

        SaveQuest();
        UpdateUI();
    }

    void UpdateUI()
    {
        questText.text = $"Quest: Earn {questTarget} CatCoins ({coinManager.GetCoins()}/{questTarget})";
        gemsText.text = $"Gems: {gems}";
    }

    void SaveQuest()
    {
        PlayerPrefs.SetInt("QuestTarget", questTarget);
        PlayerPrefs.SetInt("QuestRewardGems", questRewardGems);
        PlayerPrefs.SetInt("Gems", gems);
        PlayerPrefs.SetInt("QuestCompleted", questCompleted ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadQuest()
    {
        questTarget = PlayerPrefs.GetInt("QuestTarget", questSettings.baseQuestTarget);
        questRewardGems = PlayerPrefs.GetInt("QuestRewardGems", questSettings.baseQuestRewardGems);
        gems = PlayerPrefs.GetInt("Gems", economySettings.initialGems);
        questCompleted = PlayerPrefs.GetInt("QuestCompleted", 0) == 1;
        UpdateUI();
    }
}

