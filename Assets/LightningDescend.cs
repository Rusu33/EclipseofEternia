using UnityEngine;

/// <summary>
/// Coboară un efect de fulger din cer spre țintă.
/// Se autodistruge la impact și poate genera un efect vizual la contact.
/// </summary>
public class LightningDescend : MonoBehaviour
{
    public float speed = 30f;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public GameObject impactEffect;

    private bool hasArrived = false;

    void Update()
    {
        if (hasArrived) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            hasArrived = true;

            if (impactEffect != null)
                Instantiate(impactEffect, target, Quaternion.identity);

            Destroy(gameObject, 0.5f); // lasă puțin timp pentru a se termina efectul
        }
    }
}