using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroystuff : MonoBehaviour
{

    // Update is called once per frame
    void OnEnable()
    {
        Destroy(gameObject, .4f);
    }
}
