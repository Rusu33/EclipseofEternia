using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class WitcherCharacter : MonoBehaviour
{
    public NPCConversation myConversation; // Conversația asociată NPC-ului
    public float interactionDistance = 3f; // Distanța maximă pentru interacțiune

    private Transform player; // Referință la jucător
    private bool isPlayerInRange = false; // Verifică dacă jucătorul este în apropiere
    public static bool isDialogueActive = false; // Variabilă statică pentru blocarea altor acțiuni

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isDialogueActive) return; // Dacă dialogul este activ, ignoră restul input-urilor

        if (Vector3.Distance(player.position, transform.position) <= interactionDistance)
        {
            isPlayerInRange = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartConversation();
            }
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    private void StartConversation()
    {
        if (isPlayerInRange && myConversation != null)
        {
            isDialogueActive = true; // Blochează alte input-uri
            ConversationManager.Instance.StartConversation(myConversation);

            // Asigură-te că dialogul se oprește la final
            ConversationManager.OnConversationEnded += EndDialogue;
        }
    }

    public static void EndDialogue()
    {
        isDialogueActive = false; // Reactivează input-urile după ce dialogul s-a încheiat
        ConversationManager.OnConversationEnded -= EndDialogue; // Evită multiple apelări
    }
}
