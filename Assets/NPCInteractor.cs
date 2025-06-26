using UnityEngine;
using DialogueEditor;

public class NPCInteractor : MonoBehaviour
{
    public MonoBehaviour playerMovement;      // Ex: vThirdPersonController
    public MonoBehaviour cameraController;    // Ex: camera input script (rotire cameră)
    public GameObject dialogueUI;             // (opțional) UI-ul dacă e separat de DialogueEditor

    private void OnEnable()
    {
        ConversationManager.OnConversationStarted += StartInteraction;
        ConversationManager.OnConversationEnded += EndInteraction;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationStarted -= StartInteraction;
        ConversationManager.OnConversationEnded -= EndInteraction;
    }

    private void StartInteraction()
    {
        if (playerMovement != null) playerMovement.enabled = false;
        if (cameraController != null) cameraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (dialogueUI != null)
            dialogueUI.SetActive(true);
    }

    private void EndInteraction()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        if (cameraController != null) cameraController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (dialogueUI != null)
            dialogueUI.SetActive(false);
    }
}
