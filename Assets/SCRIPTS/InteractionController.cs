using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    public static InteractionController Instance;

    public MonoBehaviour playerMovement;
    public MonoBehaviour cameraController;
    public GameObject dialogueUI;  // Panel-ul general de UI al Dialogue Editor
    public Button endButton;       // Butonul "End" din UI-ul DialogueEditor

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        endButton.onClick.AddListener(OnEndDialogue);
        endButton.gameObject.SetActive(false); // Ascuns la început
    }

    public void OnDialogueStart()
    {
        // Blocare mișcare + cameră
        if (playerMovement != null) playerMovement.enabled = false;
        if (cameraController != null) cameraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dialogueUI.SetActive(true);
        endButton.gameObject.SetActive(false); // Ascunde End până apare automat la final
    }

    public void OnEndDialogue()
    {
        // Deblocare
        if (playerMovement != null) playerMovement.enabled = true;
        if (cameraController != null) cameraController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dialogueUI.SetActive(false);
    }
}
