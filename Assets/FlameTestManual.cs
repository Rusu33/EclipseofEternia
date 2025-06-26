using UnityEngine;

public class FlameTestManual : MonoBehaviour
{
    public GameObject flameObject; // GameObject care conține Particle System

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("✅ T apăsat");

            if (flameObject != null)
            {
                Debug.Log("🎯 Avem flameObject: " + flameObject.name);
                flameObject.SetActive(true); // dacă era dezactivat

                ParticleSystem ps = flameObject.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    Debug.Log("🔥 Particle System găsit — se dă Play()");
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // reset
                    ps.Play(); // pornește
                }
                else
                {
                    Debug.LogWarning("⚠️ flameObject NU are ParticleSystem!");
                }
            }
            else
            {
                Debug.LogWarning("❌ flameObject este null!");
            }
        }
    }
}
    