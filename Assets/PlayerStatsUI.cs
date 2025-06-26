using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [Header("Text UI")]
    public Text goldText;
    public Text moralitateText;

    [Header("Slider de moralitate")]
    public Slider moralitateSlider; 
    public Color goodColor = Color.green;
    public Color neutralColor = Color.yellow;
    public Color evilColor = Color.red;

    [Header("Skybox-uri")]
    public Material goodSkybox;
    public Material neutralSkybox;
    public Material evilSkybox;

    public int gold = 0;
    public int moralitate = 0;

    void Start()
    {
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    public void AddMoralitate(int amount)
    {
        moralitate += amount;
        if (moralitate > 100) moralitate = 100;
        UpdateUI();
    }

    public void SubtractMoralitate(int amount)
    {
        moralitate -= amount;
        if (moralitate < 0) moralitate = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateGoldUI();
        UpdateMoralitateUI();
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
            goldText.text = "Gold: " + gold;
    }

    private void UpdateMoralitateUI()
    {
        if (moralitateText != null)
            moralitateText.text = "Moralitate: " + moralitate;

        if (moralitate >= 70 && goodSkybox != null)
            RenderSettings.skybox = goodSkybox;
        else if (moralitate >= 30 && neutralSkybox != null)
            RenderSettings.skybox = neutralSkybox;
        else if (evilSkybox != null)
            RenderSettings.skybox = evilSkybox;

        DynamicGI.UpdateEnvironment();

        if (moralitateSlider != null)
        {
            moralitateSlider.value = moralitate;
            if (moralitateSlider.fillRect != null)
            {
                Image fillImage = moralitateSlider.fillRect.GetComponent<Image>();
                if (fillImage != null)
                {
                    if (moralitate >= 70)
                        fillImage.color = goodColor;
                    else if (moralitate >= 30)
                        fillImage.color = neutralColor;
                    else
                        fillImage.color = evilColor;
                }
            }
        }
        
    }



    public void SubtractGold(int amount)
    {
        gold -= amount;
        if (gold < 0) gold = 0;
        UpdateUI();
    }


    public int GetMoralitate()
    {
        return moralitate;
    }


    public bool TryPayGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI(); 
            return true;
        }
        else
        {
            return false;
        }
    }

}
