using UnityEngine;
using DialogueEditor;
using Invector.vItemManager;
using System.Linq;

public class FinalJudgeDialogue : MonoBehaviour
{
    public NPCConversation convEvil;
    public NPCConversation convMedium;
    public NPCConversation convPure;
    public PlayerAbilityController abilityScript;
    public GameObject bossHard;
    public GameObject bossMedium;
    public GameObject angelNPC;

    public Material mediumEndingSkybox;

    public Transform arenaSpawnPoint;
    public Transform player;

    public int requiredGold = 500;
    private PlayerStatsUI stats;

    private bool playerInRange = false;
    public FadeManager fadeManager; 

    private void Start()
    {
        stats = FindObjectOfType<PlayerStatsUI>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.O))
        {
            int moralitate = stats.GetMoralitate();
            if (moralitate <= 29)
                ConversationManager.Instance.StartConversation(convEvil);
            else if (moralitate <= 69)
                ConversationManager.Instance.StartConversation(convMedium);
            else
                ConversationManager.Instance.StartConversation(convPure);
        }
    }

    public void PayRedemption()
    {
        if (!stats.TryPayGold(requiredGold)) return; 

        stats.AddMoralitate(20);
        fadeManager.FadeOut(() =>
        {
            TeleportToArena();
            bossMedium.SetActive(true);
            fadeManager.FadeIn();
        });
    }


    public void Punishment()
    {
        fadeManager.FadeOut(() =>
        {
            TeleportToArena();
            bossHard.SetActive(true);
            fadeManager.FadeIn();
        });
    }

    public void MediumFight()
    {
        fadeManager.FadeOut(() =>
        {
            TeleportToArena();
            bossMedium.SetActive(true);
            fadeManager.FadeIn();
            RenderSettings.skybox = mediumEndingSkybox;
            DynamicGI.UpdateEnvironment();
        });
    }

    public void AngelEnding()
    {
        fadeManager.FadeOut(() =>
        {
            TeleportToArena();
            angelNPC.SetActive(true);
            fadeManager.FadeIn();
        });
    }

    private void TeleportToArena()
    {
        player.position = arenaSpawnPoint.position;
        player.rotation = arenaSpawnPoint.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }


    public void ActivateAbilitiesIfMediumMoral()
    {
        int moralitate = stats.GetMoralitate();

        if (moralitate >=30 && moralitate <= 79)
        {
            abilityScript.enabled = true;
            Debug.Log("✅ Abilitățile au fost activate pentru moralitate medie.");
        }
        else
        {
            Debug.Log("❌ Moralitate nepotrivită pentru abilități.");
        }
    }
}
