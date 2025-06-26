using UnityEngine;
using DialogueEditor;
using Invector.vItemManager;

public class LiquaCharacter : MonoBehaviour
{
    public NPCConversation startConversation;    
    public NPCConversation finalConversation;    
    public int questID = 6;
       


    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.O))
        {
            if (QuestManager.questManager.RequestCompleteQuest(questID))
            {
                ConversationManager.Instance.StartConversation(finalConversation);
                GiveReward();
                QuestManager.questManager.CompleteQuest(questID); 
            }
            else if (QuestManager.questManager.RequestAvailableQuest(questID))
            {
                ConversationManager.Instance.StartConversation(startConversation);
            }
            else
            {
                Debug.Log("Questul nu este disponibil încă.");
            }
        }
    }

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




    public void GiveReward()
    {
        Debug.Log("GiveReward() a fost apelată!");

        // 1. Găsește vItemManager în scenă
        var itemManager = FindObjectOfType<vItemManager>();
        if (itemManager == null)
        {
            Debug.LogError("vItemManager nu a fost găsit în scenă!");
            return;
        }

        // 2. Șterge frunza (ID = 6)
        int frunzaID = 6;
        var frunzaItem = itemManager.GetItem(frunzaID);
        if (frunzaItem != null)
        {
            itemManager.DestroyItem(frunzaItem, 1);
            Debug.Log("Frunza a fost ștearsă din inventar.");
        }
        else
        {
            Debug.LogWarning("Frunza NU a fost găsită în inventar.");
        }

        // 3. Adaugă 5 poțiuni HP (ID = 5)
        int potionID = 5;
        int potionAmount = 5;
        ItemReference potionRef = new ItemReference(potionID);
        potionRef.amount = potionAmount;
        itemManager.AddItem(potionRef);

        Debug.Log($"Ai primit {potionAmount} poţiuni HP (ID {potionID})!");
    }



}



