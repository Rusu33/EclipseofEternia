using UnityEngine;
using Invector;

public class FlameDamageTrigger : MonoBehaviour
{
    public int damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<vHealthController>();
            if (health != null)
            {
                Debug.Log("🔥 Jucătorul a fost atins de flacără!");
                health.TakeDamage(new vDamage(damage));
            }
        }
    }
}
