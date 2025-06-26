using UnityEngine;

public class LightingController : MonoBehaviour
{
    public Light directionalLight;
    public PlayerStatsUI playerStats;
    public Light moonLightSpot;
    public GameObject moonVisual;

    [Header("Culori lumină moralitate")]
    public Color goodColor = new Color(1f, 0.95f, 0.8f);       // cald, soare
    public Color neutralColor = Color.white;                   // echilibrat
    public Color evilColor = new Color(0.8f, 0.85f, 1f);       // rece, dar luminos

    void Update()
    {
        int moralitate = playerStats.GetMoralitate();

        if (moralitate >= 70)
        {
            directionalLight.color = goodColor;
            directionalLight.intensity = 1f;
            directionalLight.transform.rotation = Quaternion.Euler(50, -30, 0);
        }
        else if (moralitate >= 30)
        {
            directionalLight.color = neutralColor;
            directionalLight.intensity = 0.7f;
            directionalLight.transform.rotation = Quaternion.Euler(30, -30, 0);
        }
        else
        {
            directionalLight.color = evilColor;
            directionalLight.intensity = 0.5f;
            directionalLight.transform.rotation = Quaternion.Euler(15, -30, 0);

            if (moonLightSpot != null)
            {
                moonLightSpot.gameObject.SetActive(true);
                moonLightSpot.enabled = true;
                if (moonVisual != null)
                    moonVisual.SetActive(true);
            }
            else if (moonVisual != null)
            {
                moonVisual.SetActive(false);
            }
        }

    }
}
