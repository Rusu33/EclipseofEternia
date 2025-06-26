using UnityEngine;
using UnityEngine.UI;
using Invector.vCharacterController;
using Invector;

public class HealthBar : MonoBehaviour
{
    public Image healthFill;
    private vHealthController health;
    public Transform lookAtTarget; // camera

    void Start()
    {
        health = GetComponentInParent<vHealthController>();
    }

    void Update()
    {
        if (health && healthFill)
        {
            float percent = health.currentHealth / health.maxHealth;
            healthFill.fillAmount = percent;

            if (lookAtTarget)
                transform.LookAt(transform.position + (transform.position - lookAtTarget.position)); // face camera
        }
    }
}
