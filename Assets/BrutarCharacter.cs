using UnityEngine;
using DialogueEditor;

public class BrutarCharacter : MonoBehaviour
{
    public NPCConversation brutarconv;       // conversația cu pâinea
    public NPCConversation fiicaconv;        // conversația despre fiică
    public NPCConversation retfiicaconv;     // conversația finală (mulțumire)

    public GameObject painePrefab;
    public Transform spawnPoint;

    public GameObject fiicaPrefab;
    public Transform fiicaSpawnPoint;

    public GameObject fiicaDinScena;         // referință la fiica instanțiată

    private bool aSpawnaPainea = false;
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    void Update()
    {
        // Verificăm dacă fiica este aproape și questul 3 este ACCEPTAT → marcare COMPLETE
        Quest questFiica = QuestManager.questManager.GetQuestByID(3);
        if (questFiica != null && questFiica.progress == Quest.Progress.ACCEPT && fiicaDinScena != null)
        {
            float dist = Vector3.Distance(transform.position, fiicaDinScena.transform.position);
            if (dist < 3f)
            {
                questFiica.progress = Quest.Progress.COMPLETE;
                Debug.Log("Fiica e lângă brutar. Questul 3 a fost marcat ca COMPLETE.");
            }
        }

        // Apăsare O pentru a interacționa
        if (playerInRange && Input.GetKeyDown(KeyCode.O))
        {
            Quest quest2 = QuestManager.questManager.GetQuestByID(2);
            Quest quest3 = QuestManager.questManager.GetQuestByID(3);

            if (quest2 != null && quest2.progress == Quest.Progress.ACCEPT)
            {
                // Dialogul pentru questul cu pâinea
                ConversationManager.Instance.StartConversation(brutarconv);

                if (!aSpawnaPainea)
                {
                    SpawnPaine();
                    aSpawnaPainea = true;
                }
            }
            else if (quest2 != null && (quest2.progress == Quest.Progress.DONE || quest2.progress == Quest.Progress.REFUSED))
            {
                // Doar dialogul cu fiica → acceptarea și spawn-ul se face doar dacă apasă butonul Acceptă în dialog
                ConversationManager.Instance.StartConversation(fiicaconv);
            }

            // Finalizare quest 3 dacă e deja completat
            if (questFiica != null && questFiica.progress == Quest.Progress.COMPLETE && fiicaDinScena != null)
            {
                float dist = Vector3.Distance(transform.position, fiicaDinScena.transform.position);
                if (dist < 3f)
                {
                    ConversationManager.Instance.StartConversation(retfiicaconv);
                    QuestManager.questManager.CompleteQuest(3);
                    Debug.Log("Questul cu fiica a fost finalizat.");

                    Destroy(fiicaDinScena);
                    Debug.Log("Fiica a fost despawnată.");
                }
                else
                {
                    Debug.Log("Fiica nu este suficient de aproape pentru finalizare.");
                }
            }
        }
    }

    void SpawnPaine()
    {
        if (painePrefab != null && spawnPoint != null)
        {
            Instantiate(painePrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Pâinea a fost plasată.");
        }
        else
        {
            Debug.LogWarning("Prefab sau spawn point lipsă!");
        }
    }

    // ✅ Folosit DOAR de butonul "Acceptă" din dialogul cu fiica
    public void SpawnFiicaFromDialogue()
    {
        Quest quest3 = QuestManager.questManager.GetQuestByID(3);

        if (quest3 != null && quest3.progress == Quest.Progress.NOT_AVAILABLE)
            quest3.progress = Quest.Progress.AVAILABLE;

        if (quest3 != null && quest3.progress == Quest.Progress.AVAILABLE)
        {
            QuestManager.questManager.AcceptQuest(3);

            if (fiicaPrefab != null && fiicaSpawnPoint != null && fiicaDinScena == null)
            {
                fiicaDinScena = Instantiate(fiicaPrefab, fiicaSpawnPoint.position, fiicaSpawnPoint.rotation);
                Debug.Log("Fiica a fost plasată din dialog.");
            }
        }
    }
}
