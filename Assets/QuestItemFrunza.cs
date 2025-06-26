using UnityEngine;
using Invector.vItemManager;

public class QuestItemFrunza : MonoBehaviour
{
    public string questObjectiveName = "FrunzaVantului";
    public int amount = 1;
    public int itemID = 6;

    private bool isPlayerNearby = false;
    private GameObject player;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // 1. Progres quest
            QuestManager.questManager.AddQuestItem(questObjectiveName, amount);

            // 2. Adaugã în inventar
            var itemManager = player.GetComponent<vItemManager>();
            if (itemManager != null && itemManager.itemListData != null)
            {
                ItemReference itemRef = new ItemReference(itemID);
                itemRef.amount = amount;

                itemManager.AddItem(itemRef);
                Debug.Log("Frunza a fost adãugatã în inventar!");
            }

            // 3. Distruge obiectul
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
