using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    public Text interactionText; // Tragi în inspector textul „Apasă O…”
    public string mesaj = "Apasă O pentru a interacționa";

    private bool isPlayerNear = false;

    private void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (interactionText != null)
            {
                interactionText.text = mesaj;
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactionText != null)
                interactionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Ai interacționat cu NPC-ul!");
            // aici poți declanșa dialogul
        }
    }
}
