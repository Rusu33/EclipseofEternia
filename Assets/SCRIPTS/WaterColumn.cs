using UnityEngine;

/// <summary>
/// Script pentru coloana de apă:
///  - Se autodistruge după un anumit timp
///  - Poate aplica efecte la impact (opțional)
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class WaterColumn : MonoBehaviour
{
    public float lifetime = 3f;
    public float upwardForce = 10f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Start()
    {
        // Aplicăm impuls inițial în sus
        rb.velocity = Vector3.up * upwardForce;
        // Programăm autodistrugere
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Poți adăuga aici efecte de stropire sau damage
        // De exemplu:
        // Instantiate(splashEffect, collision.contacts[0].point, Quaternion.identity);
    }
}
