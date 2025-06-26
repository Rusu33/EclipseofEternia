using UnityEngine;
using Invector.vCharacterController;
using DialogueEditor;

public class DialogueInteractionHandler : MonoBehaviour
{
    [Header("Referințe principale")]
    public vThirdPersonController playerController;
    public MonoBehaviour cameraInput;
    public GameObject optionalUI;

    private MonoBehaviour[] combatScripts;

    void Start()
    {
        // Căutăm toate scripturile de tip combat automat
        combatScripts = FindScriptsByKeywords(new string[] { "combat", "melee", "attack", "shooter", "input" });
    }

    void OnEnable()
    {
        ConversationManager.OnConversationStarted += FreezePlayer;
        ConversationManager.OnConversationEnded += UnfreezePlayer;
    }

    void OnDisable()
    {
        ConversationManager.OnConversationStarted -= FreezePlayer;
        ConversationManager.OnConversationEnded -= UnfreezePlayer;
    }

    private void FreezePlayer()
    {
        if (playerController != null) playerController.enabled = false;
        if (cameraInput != null) cameraInput.enabled = false;

        ToggleScripts(combatScripts, false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (optionalUI != null)
            optionalUI.SetActive(true);
    }

    private void UnfreezePlayer()
    {
        if (playerController != null) playerController.enabled = true;
        if (cameraInput != null) cameraInput.enabled = true;

        ToggleScripts(combatScripts, true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (optionalUI != null)
            optionalUI.SetActive(false);
    }

    private MonoBehaviour[] FindScriptsByKeywords(string[] keywords)
    {
        var allScripts = FindObjectsOfType<MonoBehaviour>();
        var result = new System.Collections.Generic.List<MonoBehaviour>();

        foreach (var script in allScripts)
        {
            string name = script.GetType().Name.ToLower();
            foreach (var keyword in keywords)
            {
                if (name.Contains(keyword))
                {
                    result.Add(script);
                    break;
                }
            }
        }

        return result.ToArray();
    }

    private void ToggleScripts(MonoBehaviour[] scripts, bool state)
    {
        foreach (var script in scripts)
        {
            if (script != null && script != this)
                script.enabled = state;
        }
    }
}
