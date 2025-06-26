using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingSwitcher : MonoBehaviour
{
    public Volume volume;
    public VolumeProfile goodProfile;
    public VolumeProfile neutralProfile;
    public VolumeProfile evilProfile;

    public PlayerStatsUI playerStatsUI;

    void Update()
    {
        int moralitate = playerStatsUI.GetMoralitate();
        Debug.Log("Moralitate: " + moralitate);
        Debug.Log("Profil curent: " + volume.profile.name);

        if (moralitate >= 70 && volume.profile != goodProfile)
        {
            volume.profile = goodProfile;
            Debug.Log("Am setat profil GOOD");
        }
        else if (moralitate >= 30 && moralitate < 70 && volume.profile != neutralProfile)
        {
            volume.profile = neutralProfile;
            Debug.Log("Am setat profil NEUTRAL");
        }
        else if (moralitate <= 29 && volume.profile != evilProfile)
        {
            volume.profile = evilProfile;
            Debug.Log("Am setat profil EVIL");
        }
    }

}
