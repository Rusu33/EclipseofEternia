using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{
    private bool Trigger = false;
    public List<int> availableQuestIDs = new List<int>();
    public List<int> receivableQuestIDs = new List<int>();
    public GameObject questMarker; //canvas
    //public Image theImage;

    public Sprite questavailableSprite;
    public Sprite receivableSprite;

   /* private void SetQuestMarker()
    {
        if(QuestManager.questManager.CheckCompletedQuests(this))
        {
            questMarker.SetActive(true);
           // theImage.sprite = receivableSprite;
           // theImage.color = Color.red;
        }
        else if(QuestManager.questManager.CheckAvailableQuests(this))
        {
            questMarker.SetActive(true);
            //theImage.sprite = questavailableSprite;
            //theImage.color = Color.green;
        }
        else
        {
            questMarker.SetActive(false);
        }
    }
   */
   /* private void Start()
    {
        SetQuestMarker();
    }*/
    private void Update()
    {
        if(Trigger && Input.GetKeyDown(KeyCode.Space))
        {
            //quest ui manager
            QuestManager.questManager.QuestRequest(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Trigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Trigger= false;
        }
    }

}
