using UnityEngine;
using Invector;
using Invector.vCharacterController;

public class HitboxDebug : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("🎯 HIT TRIGGER: " + other.name);

        if (other.CompareTag("Player"))
        {
            var health = other.GetComponentInChildren<vIHealthController>();
            if (health != null)
            {
                Debug.Log("✅ Player hit — applying damage!");
                vDamage dmg = new vDamage { damageValue = 10 };
                health.TakeDamage(dmg);
            }
            else
            {
                Debug.LogWarning("⚠️ Player has NO vIHealthController!");
            }
        }
    }
}
