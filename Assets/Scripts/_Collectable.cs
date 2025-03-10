using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Collectable : MonoBehaviour
{
    public int coinValue;

    void OnEnable()
    {
        coinValue = Random.Range(1,200);
    }
}
