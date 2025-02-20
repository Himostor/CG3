using UnityEngine;

public class WardrobeManager : MonoBehaviour
{
    public SpriteRenderer hatLayer;
    public SpriteRenderer shirtLayer;
    public SpriteRenderer pantsLayer;

    public Sprite[] hats;
    public Sprite[] shirts;
    public Sprite[] pants;

    private int currentHatIndex = 0;
    private int currentShirtIndex = 0;
    private int currentPantsIndex = 0;

    private bool hatUnlocked = false;
    private bool shirtUnlocked = false;
    private bool pantsUnlocked = false;

    void Start()
    {
        LoadOutfit();
        UpdateOutfit();
    }

    public void UnlockHat()
    {
        hatUnlocked = true;
        SaveOutfit();
    }

    public void UnlockShirt()
    {
        shirtUnlocked = true;
        SaveOutfit();
    }

    public void UnlockPants()
    {
        pantsUnlocked = true;
        SaveOutfit();
    }

    void UpdateOutfit()
    {
        hatLayer.sprite = hatUnlocked ? hats[currentHatIndex] : null;
        shirtLayer.sprite = shirtUnlocked ? shirts[currentShirtIndex] : null;
        pantsLayer.sprite = pantsUnlocked ? pants[currentPantsIndex] : null;
    }

    void SaveOutfit()
    {
        PlayerPrefs.SetInt("HatUnlocked", hatUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("ShirtUnlocked", shirtUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("PantsUnlocked", pantsUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadOutfit()
    {
        hatUnlocked = PlayerPrefs.GetInt("HatUnlocked", 0) == 1;
        shirtUnlocked = PlayerPrefs.GetInt("ShirtUnlocked", 0) == 1;
        pantsUnlocked = PlayerPrefs.GetInt("PantsUnlocked", 0) == 1;
    }
}
