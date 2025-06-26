using UnityEngine;
using Invector;

[RequireComponent(typeof(vHealthController))]
public class BossHitBridge : MonoBehaviour
{
    private BossController boss;

    void Start()
    {
        boss = GetComponent<BossController>();
    }

    // Aceasta e metoda corectă și recunoscută de UnityEvent
    public void OnReceiveDamage(Invector.vDamage damage)
    {
        if (boss != null)
        {
            boss.TakeDamage((int)damage.damageValue);
        }
    }
}
