using UnityEngine;
using DialogueEditor;

public class VillagerCharacter : MonoBehaviour
{
    public NPCConversation startConversation;   // dialogul inițial
    public NPCConversation finalConversation;   // dialogul de mulțumire

    public int questID = 2;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.O))
        {
            if (QuestManager.questManager.RequestCompleteQuest(questID))
            {
                QuestManager.questManager.CompleteQuest(questID);
                Debug.Log("Quest completat și recompensă primită.");
                ConversationManager.Instance.StartConversation(finalConversation);
            }
            else
            {
                // Doar începe dialogul. Acceptarea/Refuzul se face din nodurile Dialogue Editor
                ConversationManager.Instance.StartConversation(startConversation);
            }
        }
    }
}
