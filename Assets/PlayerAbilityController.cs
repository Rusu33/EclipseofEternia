using UnityEngine;
using Invector.vCharacterController;
using static CartoonFX.CFXR_Effect;
using System.Collections;

public class PlayerAbilityController : MonoBehaviour
{
    [Header("References")]
    public Transform dragonTransform;
    public GameObject lightningPrefab;
    public GameObject lightningStartEffect;
    public GameObject lightningHitEffect;
    public GameObject altAbilityPrefab;
    public GameObject alternativePrefab;
    public GameObject dragon;
    public CameraShake cameraShake;
    public GameObject abilitiesUIPanel;
    public TMPro.TextMeshProUGUI lightningProgressText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] lightningSounds;
    public AudioClip starImpactSound;

    [Header("Costs and Settings")]
    public float lightningCost = 20f;
    public float lightningHeight = 20f;
    public float altCost = 15f;
    public float forwardOffset = 2f;

    private vThirdPersonController tpController;
    private int lightningCasts = 0;
    private float lightningCooldown = 6f;
    private float nextLightningTime = 0f;

    void Start()
    {
        tpController = GetComponent<vThirdPersonController>();

        if (abilitiesUIPanel != null)
            abilitiesUIPanel.SetActive(true);

        if (lightningProgressText != null)
        {
            lightningProgressText.text =
                "Abilități disponibile:\nJ – Fulger divin (0/6)\nK – Stele (Ultimate după 6 folosiri fulger)";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextLightningTime)
            TryCastLightning();

        if (Input.GetKeyDown(KeyCode.K))
            TryCastAlternative();
    }

    void TryCastLightning()
    {
        if (lightningStartEffect == null || dragonTransform == null) return;
        if (tpController.currentStamina < lightningCost) return;

        tpController.ReduceStamina(lightningCost, false);
        lightningCasts++;
        nextLightningTime = Time.time + lightningCooldown;

        Vector3 start = dragonTransform.position + Vector3.up * lightningHeight;
        Vector3 end = dragonTransform.position + Vector3.up * 1.5f;

        GameObject fx = Instantiate(lightningStartEffect, start, Quaternion.identity);
        fx.transform.localScale *= 10f;

        var descend = fx.AddComponent<LightningDescend>();
        descend.target = end;
        descend.impactEffect = lightningHitEffect;

        if (alternativePrefab != null)
        {
            GameObject auraFx = Instantiate(alternativePrefab, transform.position, Quaternion.identity);
            Destroy(auraFx, 5f);
        }

        if (lightningSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, lightningSounds.Length);
            audioSource.PlayOneShot(lightningSounds[randomIndex]);
        }

        if (lightningProgressText != null)
        {
            lightningProgressText.text =
                $"Abilități disponibile:\nJ – Fulger divin ({lightningCasts}/6)\nK – Stele (Ultimate după 6 folosiri fulger)";
        }
    }

    void TryCastAlternative()
    {
        if (lightningCasts < 6) return;
        if (altAbilityPrefab == null || dragonTransform == null) return;
        if (tpController.currentStamina < altCost) return;

        tpController.ReduceStamina(altCost, false);
        StartCoroutine(TriggerStarRainFinale());
    }

    IEnumerator TriggerStarRainFinale()
    {
        Vector3 centerPos = dragonTransform.position + Vector3.up * 1.5f;
        int starCount = 10;
        float radius = 4f;
        float height = 60f;
        float scale = 3f;

        
        if (starImpactSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(starImpactSound);
        }

        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(6f, 0.2f));

        for (int i = 0; i < starCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * radius;
            Vector3 targetPos = centerPos + new Vector3(offset.x, 0, offset.y);
            Vector3 spawnPos = targetPos + Vector3.up * height;

            GameObject fx = Instantiate(altAbilityPrefab, spawnPos, Quaternion.identity);
            fx.transform.localScale *= scale;

            var descend = fx.AddComponent<LightningDescend>();
            descend.target = targetPos;
            descend.speed = 10f;
            descend.impactEffect = null;
        }

        yield return new WaitForSeconds(5.5f);

        if (dragon != null)
            Destroy(dragon);

        yield return new WaitForSeconds(2f);

        FinalManager finalManager = FindObjectOfType<FinalManager>();
        if (finalManager != null)
            finalManager.TriggerEnding("medium");
    }

}
