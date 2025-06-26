using UnityEngine;

public class FlameTriggerTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("🔥 OnTriggerEnter: " + other.name + " / Tag: " + other.tag);
    }
}
