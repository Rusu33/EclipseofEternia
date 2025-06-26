using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class FinalManager : MonoBehaviour
{
    public Image fadePanel;
    public GameObject finalTextPanel;
    public TextMeshProUGUI finalMessage;
    public float fadeSpeed = 2f;
    public GameObject playerController;
    private bool showFinalUI = false;
    public Button returnToMenuButton;

    public void TriggerEnding(string type)
    {
        StartCoroutine(DelayedFadeAndShow(type));
    }

    IEnumerator DelayedFadeAndShow(string type)
    {
        yield return new WaitForSeconds(2f);

        while (fadePanel.color.a < 1)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a + Time.deltaTime * fadeSpeed);
            yield return null;
        }

        finalTextPanel.SetActive(true);
        if (playerController != null)
            playerController.SetActive(false);
        showFinalUI = true;

        if (type == "pure")
        {
            finalMessage.text = "Ai atins lumina cea adevărată. Eternia renaște prin curăția ta, iar lumea își regăsește în sfârșit liniștea.";
        }
        else if (type == "medium")
        {
            finalMessage.text = "Ai invins intunericul, dar urmele lui raman. Lumea merge mai departe, purtand cicatricile alegerilor tale.";
        }
        else if (type == "evil")
        {
            finalMessage.text = "Ai cazut in lupta cu intunericul. Eternia este pierduta, iar umbrele domnesc peste lume.";
        }

    }

    void Update()
    {
        if (showFinalUI)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(returnToMenuButton.gameObject);
            }
        }
    }

    public void TriggerEvilEndingFromDeath()
    {
        StartCoroutine(TriggerEvilEndingRoutine());
    }

    private IEnumerator TriggerEvilEndingRoutine()
    {
        yield return new WaitForSeconds(2f);
        TriggerEnding("evil");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

