using UnityEngine;
using Invector.vItemManager;

public class QuestItem : MonoBehaviour
{
    public string questObjectiveName = "Amuleta Eldgar";
    public int amount = 1;
    public int itemID = 1;
    //public GameObject pickupEffect;

    private bool isPlayerNearby = false;
    private GameObject player;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // 1. Progres quest
             QuestManager.questManager.AddQuestItem(questObjectiveName, amount);

            // 2. Adaugă în inventar
            var itemManager = player.GetComponent<vItemManager>();
            if (itemManager != null && itemManager.itemListData != null)
            {
                ItemReference itemRef = new ItemReference(itemID);
                itemRef.amount = amount;
                itemManager.AddItem(itemRef);
                Debug.Log("Ai ridicat amuleta: " + questObjectiveName);
            }

           /* // 3. Efect vizual
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
           */
            // 4. Distruge obiectul
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
        }
    }
}
