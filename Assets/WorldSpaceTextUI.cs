using UnityEngine;

public class WorldSpaceTextUI : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2.0f, 0);

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // 🔽 Adăugat: ajustăm automat offset-ul dacă e item
        if (target != null && target.CompareTag("Item"))
        {
            offset = new Vector3(0, 1.0f, 0); // text mai jos pentru iteme
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
            return;
        }

        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        bool inView = screenPos.z > 0 &&
                      screenPos.x >= 0 && screenPos.x <= Screen.width &&
                      screenPos.y >= 0 && screenPos.y <= Screen.height;

        if (inView)
        {
            transform.position = screenPos;

            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }
        else
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}
