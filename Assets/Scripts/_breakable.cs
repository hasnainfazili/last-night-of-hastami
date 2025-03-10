using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _breakable : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Weapon")) Destroy(gameObject);
    }
}
