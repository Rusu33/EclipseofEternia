using UnityEngine;
using DialogueEditor;

public class EldgarCharacter : MonoBehaviour
{
    public NPCConversation testconversation;    
    public NPCConversation finalconversation;   
    public int questID = 1;                        
    public string requiredObjectiveName = "Amuleta Eldgar";

    private bool playerInRange = false;

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

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.O))
        {
            Quest quest = QuestManager.questManager.GetQuestByID(questID);

            if (quest == null)
            {
                Debug.LogWarning("Quest not found in QuestManager!");
                return;
            }

            switch (quest.progress)
            {
                case Quest.Progress.AVAILABLE:
                    ConversationManager.Instance.StartConversation(testconversation);
                    break;

                case Quest.Progress.ACCEPT:
                    
                    if (QuestManager.questManager.HasItemForQuest(requiredObjectiveName))
                    {
                        QuestManager.questManager.CompleteQuest(questID);
                        Debug.Log("✅ Ai returnat . Quest completat!");

                        if (finalconversation != null)
                        {
                            ConversationManager.Instance.StartConversation(finalconversation);
                        }
                    }
                    else
                    {
                        Debug.Log("🔸 Încă nu ai completat.");
                    }
                    break;

                case Quest.Progress.COMPLETE:
                case Quest.Progress.DONE:
                    Debug.Log("✔️ Questul a fost deja finalizat.");
                    if (finalconversation != null)
                    {
                        ConversationManager.Instance.StartConversation(finalconversation);
                    }
                    break;
            }
        }
    }
}
