using UnityEngine;
using DialogueEditor;

public class EasterEggNPC : MonoBehaviour
{
    public NPCConversation easterEggConversation;
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
            ConversationManager.Instance.StartConversation(easterEggConversation);
        }
    }
}
