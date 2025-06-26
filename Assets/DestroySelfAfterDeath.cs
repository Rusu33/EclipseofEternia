using UnityEngine;

public class DestroySelfAfterDeath : MonoBehaviour
{
    public float delay = 2f;

    public void DestroyMe()
    {
        Destroy(gameObject, delay);
    }
}
