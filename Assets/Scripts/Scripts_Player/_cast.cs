using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class _cast : MonoBehaviour
{
   public float speed = 200f;
   public float damage = 20f;
   Rigidbody rb;
   void OnEnable()
   {
     rb = GetComponent<Rigidbody>();
     rb.AddForce(transform.forward * speed, ForceMode.Impulse);
   }
   void OnCollisionEnter(Collision col)
   {
     foreach(ContactPoint contact in col.contacts)
     {
      if(contact.otherCollider.gameObject.GetComponent<_enemyController>() != null)
      {
        contact.otherCollider.gameObject.GetComponent<_enemyController>().TakeDamage(damage);
        Destroy(gameObject);
      }
     }
   }
    void Update()
    {
      Destroy(gameObject, 2f);
    }
}
