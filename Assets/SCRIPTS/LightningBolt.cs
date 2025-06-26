using UnityEngine;
using System.Collections;

/// <summary>
/// Script pentru efect de fulger folosind LineRenderer.
/// Atașează acest script prefab-ului lightningPrefab și adaugă un LineRenderer cu numărul de segmente potrivit.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class LightningBolt : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPoint;
    [HideInInspector] public Vector3 EndPoint;
    public int segments = 12;
    public float jitterAmount = 0.3f;
    public float duration = 0.2f;

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segments + 1;
        StartCoroutine(ShowBolt());
    }

    IEnumerator ShowBolt()
    {
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            Vector3 pos = Vector3.Lerp(StartPoint, EndPoint, t);
            pos += Random.insideUnitSphere * jitterAmount;
            lr.SetPosition(i, pos);
        }
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
