using UnityEngine;

public class NPCInteractPrompt : MonoBehaviour
{
    private GameObject promptInstance;
    private bool isPlayerNear = false;

    // Prefab-ul textului din Resources (comun tuturor NPC-ilor)
    private static GameObject sharedPromptPrefab;

    private void LoadPromptPrefab()
    {
        if (sharedPromptPrefab == null)
        {
            sharedPromptPrefab = Resources.Load<GameObject>("InteractionWorldText");
            if (sharedPromptPrefab == null)
            {
                Debug.LogError("❌ Prefab-ul 'InteractionWorldText' nu se găsește în Resources!");
            }
        }
    }

    void Start()
    {
        LoadPromptPrefab();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (sharedPromptPrefab != null && promptInstance == null)
            {
                Canvas canvas = FindObjectOfType<Canvas>();
                if (canvas == null)
                {
                    Debug.LogError("❌ Nu există Canvas în scenă!");
                    return;
                }

                promptInstance = Instantiate(sharedPromptPrefab, canvas.transform);

                WorldSpaceTextUI follow = promptInstance.GetComponent<WorldSpaceTextUI>();
                if (follow != null)
                    follow.target = this.transform;

                promptInstance.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            if (promptInstance != null)
                Destroy(promptInstance);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log($"💬 Ai interacționat cu {gameObject.name}");
            // aici poți porni dialog, quest, inventar etc.
        }
    }
}
