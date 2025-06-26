using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
     public static QuestManager questManager;
     public List<Quest> questList=new List<Quest>();  
     public List<Quest> currentQuestList=new List<Quest>(); 

    


    private void Awake()
    {
        if (questManager == null)
        {
            questManager = this;
        }
        else if (questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void QuestRequest(QuestObject NPCQuestObject)
    {
        
        if (NPCQuestObject.availableQuestIDs.Count > 0)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                for (int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
                {
                    if (questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.Progress.AVAILABLE)
                    {
                        AcceptQuest(NPCQuestObject.availableQuestIDs[j]);
                    }
                }
            }
        }


        
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (currentQuestList[i].id == NPCQuestObject.receivableQuestIDs[j] && currentQuestList[i].progress == Quest.Progress.ACCEPT || currentQuestList[i].progress == Quest.Progress.COMPLETE)
                {
               
                    CompleteQuest(NPCQuestObject.receivableQuestIDs[j]);

                }
            }
        }
    }


    //ACCEPT QUEST

    public void AcceptQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress==Quest.Progress.AVAILABLE)
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.Progress.ACCEPT;
            }
        }
    }



    //REFUSE QUEST

    public void RefuseQuest(int questID)
    {
        Quest quest = questList.Find(q => q.id == questID);
        if (quest != null && quest.progress == Quest.Progress.AVAILABLE)
        {
            quest.progress = Quest.Progress.REFUSED;
            Debug.Log("Quest ID " + questID + " a fost refuzat.");
        }
    }

    //COMPLETE QUEST

    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.Progress.COMPLETE)
            {
                Quest completedQuest = currentQuestList[i]; 
                completedQuest.progress = Quest.Progress.DONE;

                var ui = FindObjectOfType<PlayerStatsUI>();
                if (ui != null)
                {
                    ui.AddGold(completedQuest.goldReward);
                    ui.AddMoralitate(completedQuest.moralitate);
                }

                currentQuestList.RemoveAt(i); 
                break; 
            }
        }

        CheckChainQuest(questID);
    }



    //check chain quest

    void CheckChainQuest(int questID)
    {
        int tempID = 0;
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id==questID && questList[i].nextQuest > 0) 
            {
                tempID = questList[i].nextQuest;
            }
        }
        if(tempID > 0)
        {
            for(int i = 0; i<questList.Count ; i++)
            {
                if (questList[i].id==tempID && questList[i].progress == Quest.Progress.NOT_AVAILABLE)
                {
                    questList[i].progress= Quest.Progress.AVAILABLE;
                }
            }
        }
    }


    //ADD ITEM

    public void AddQuestItem(string questObjective, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            Quest quest = currentQuestList[i];

            if (quest.questObjective == questObjective && quest.progress == Quest.Progress.ACCEPT)
            {
                quest.questObjectiveCount += itemAmount;

                if (quest.questObjectiveCount >= quest.questObjectiveRequirement)
                {
                    quest.progress = Quest.Progress.COMPLETE;
                }
            }
        }
    }


    //BOOLS

    public bool RequestAvailableQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress==Quest.Progress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestAcceptQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.Progress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.Progress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }

    public bool  CheckAvailableQuests(QuestObject NPCQuestObject)
    {
        for(int i= 0; i<questList.Count;i++)
        {
            for(int j=0; j < NPCQuestObject.availableQuestIDs.Count; j++) {
                if (questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress== Quest.Progress.AVAILABLE) 
                {
                    return true;
                }
            }
        }
        return false;
    }


    public bool CheckAcceptedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.Progress.ACCEPT)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompletedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.Progress.COMPLETE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool HasItemForQuest(string questObjective)
    {
        foreach (Quest quest in currentQuestList)
        {
            if (quest.questObjective == questObjective &&
                quest.questObjectiveCount >= quest.questObjectiveRequirement)
            {
                return true;
            }
        }
        return false;
    }

    public Quest GetQuestByID(int id)
    {
        foreach (Quest quest in currentQuestList)
        {
            if (quest.id == id)
                return quest;
        }

        foreach (Quest quest in questList)
        {
            if (quest.id == id)
                return quest;
        }

        return null;
    }



    
    public void StartQuestByID(int questID)
    {
        Quest questToStart = questList.Find(q => q.id == questID);
        if (questToStart != null && questToStart.progress == Quest.Progress.AVAILABLE)
        {
            AcceptQuest(questID); 
            
        }
        else
        {
            Debug.LogWarning("Quest ID " + questID + " is not available.");
        }
    }
}
