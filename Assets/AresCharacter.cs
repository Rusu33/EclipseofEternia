using UnityEngine;
using DialogueEditor;
using Invector.vItemManager;

public class AresCharacter : MonoBehaviour
{
    public NPCConversation aresconvfinal; // Conversație de mulțumire (opțional)
    public int questID = 4; // Questul cu poțiunea
    public int itemID = 3;  // ID-ul poțiunii

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
            var itemManager = FindObjectOfType<vItemManager>();
            var quest = QuestManager.questManager.GetQuestByID(questID);

            if (itemManager != null && quest != null && quest.progress == Quest.Progress.ACCEPT)
            {
                if (itemManager.ContainItem(itemID))
                {
                    // Consumă poțiunea
                    var item = itemManager.GetItem(itemID);
                    if (item != null)
                        itemManager.DestroyItem(item, 1);

                    // Marchează questul ca complete
                    quest.progress = Quest.Progress.COMPLETE;
                    Debug.Log("Questul cu poțiunea a fost completat.");

                    if (aresconvfinal != null)
                        ConversationManager.Instance.StartConversation(aresconvfinal);
                }
                else
                {
                    Debug.Log("Nu ai poțiunea în inventar.");
                }
            }
        }
    }
}
