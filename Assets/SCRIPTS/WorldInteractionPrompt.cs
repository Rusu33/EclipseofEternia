using UnityEngine;

public class WorldInteractionPrompt : MonoBehaviour
{
    public string mesaj = "Apasă E pentru a ridica";
    public KeyCode tastaInteractiune = KeyCode.E;

    private GameObject promptInstance;
    private bool isPlayerNear = false;

    private static GameObject sharedPromptPrefab;

    void Start()
    {
        // încarcă prefab-ul o singură dată
        if (sharedPromptPrefab == null)
        {
            sharedPromptPrefab = Resources.Load<GameObject>("InteractionWorldText");
            if (sharedPromptPrefab == null)
                Debug.LogError("❌ Nu există prefab InteractionWorldText în Resources!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (sharedPromptPrefab != null && promptInstance == null)
            {
                Canvas canvas = FindObjectOfType<Canvas>();
                promptInstance = Instantiate(sharedPromptPrefab, canvas.transform);

                var follow = promptInstance.GetComponent<WorldSpaceTextUI>();
                if (follow != null)
                    follow.target = this.transform;

                // schimbăm textul
                var text = promptInstance.GetComponent<UnityEngine.UI.Text>();
                if (text != null)
                    text.text = mesaj;

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
        if (isPlayerNear && Input.GetKeyDown(tastaInteractiune))
        {
            Debug.Log($"✅ Ai interacționat cu obiectul {gameObject.name}");
            // Aici poți adăuga cod pentru ridicarea itemului sau activarea logicii
        }
    }
}
