using UnityEngine;
using DialogueEditor;

public class AngelCharacter : MonoBehaviour
{
    public NPCConversation angelConversation;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.O) && !ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(angelConversation);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
