using UnityEngine;

public class TestActiv : MonoBehaviour
{
    public GameObject objectToActivate;

    public void ActivateTestObject()
    {
        if (objectToActivate != null)
            objectToActivate.SetActive(true);
    }
}