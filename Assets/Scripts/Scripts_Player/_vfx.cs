using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _vfx : MonoBehaviour
{
   public GameObject vfx;
   public float _delay;

   void Update()
   {
    if(Input.GetButtonDown("Fire1"))
     StartCoroutine(PlayVFX());
   }

   IEnumerator PlayVFX()
   {
     yield return new WaitForSeconds(_delay);
     Instantiate(vfx, transform.position, Quaternion.identity);
   }
}
