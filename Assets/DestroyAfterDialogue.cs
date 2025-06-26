using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDialogue : MonoBehaviour
{
    public void DestroyMe()
    {
        Destroy(gameObject, 2f); 
    }
}
