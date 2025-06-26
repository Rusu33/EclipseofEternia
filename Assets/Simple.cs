using UnityEngine;
using Invector.vCharacterController;


public class Simple : MonoBehaviour
{
    private vThirdPersonInput[] inputs;
    private vThirdPersonController[] controllers;
    private MonoBehaviour[] combatScripts;

    void Start()
    {
        inputs = FindObjectsOfType<vThirdPersonInput>();
        controllers = FindObjectsOfType<vThirdPersonController>();
        combatScripts = FindScriptsByKeywords(new string[] { "attack", "shooter", "melee", "combat", "input" });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            LockCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            UnlockCharacter();
        }
    }

    void LockCharacter()
    {
        foreach (var input in inputs) input.enabled = false;
        foreach (var ctrl in controllers) ctrl.enabled = false;
        ToggleScripts(combatScripts, false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UnlockCharacter()
    {
        foreach (var input in inputs) input.enabled = true;
        foreach (var ctrl in controllers) ctrl.enabled = true;
        ToggleScripts(combatScripts, true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    MonoBehaviour[] FindScriptsByKeywords(string[] keywords)
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

    void ToggleScripts(MonoBehaviour[] scripts, bool state)
    {
        foreach (var script in scripts)
        {
            if (script != this) script.enabled = state;
        }
    }
}
