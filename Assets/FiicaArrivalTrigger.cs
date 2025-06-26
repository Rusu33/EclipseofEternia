using UnityEngine;

public class FiicaArrivalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brutar"))
        {
            Quest quest = QuestManager.questManager.GetQuestByID(3);
            if (quest != null && quest.progress == Quest.Progress.ACCEPT)
            {
                quest.progress = Quest.Progress.COMPLETE;
                Debug.Log("Fiica a ajuns la brutar. Questul a fost marcat ca COMPLETE.");
            }
        }
    }
}
