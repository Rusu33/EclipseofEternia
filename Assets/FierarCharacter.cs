using UnityEngine;
using DialogueEditor;
using Invector.vItemManager;

public class FierarCharacter : MonoBehaviour
{
    public NPCConversation questGood;  
    public NPCConversation questBad;   

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
        if (playerInRange && Input.GetKeyDown(KeyCode.O))
        {
            var ui = FindObjectOfType<PlayerStatsUI>();
            int moralitate = ui != null ? ui.GetMoralitate() : 0;

            if (moralitate <= 50)
            {
                ConversationManager.Instance.StartConversation(questBad);
            }
            else
            {
                ConversationManager.Instance.StartConversation(questGood);
            }
        }
    }

    
    public void DaPotiuneLuiPlayer()
    {
        var itemManager = FindObjectOfType<vItemManager>();
        if (itemManager != null)
        {
            var potiune = new ItemReference(3); 
            potiune.amount = 1; 

            itemManager.CollectItem(potiune);
        }
    }

  
    public void AcceptQuest4()
    {
        Quest quest = QuestManager.questManager.GetQuestByID(4);
        if (quest != null && quest.progress == Quest.Progress.NOT_AVAILABLE)
        {
            quest.progress = Quest.Progress.AVAILABLE;
            QuestManager.questManager.AcceptQuest(4);
        }
    }

    public void SabiePlayer()
    {
        var statsUI = FindObjectOfType<PlayerStatsUI>();
        if (statsUI != null)
        {
            statsUI.AddMoralitate(20);
            statsUI.AddGold(200);
        }
    }




}
