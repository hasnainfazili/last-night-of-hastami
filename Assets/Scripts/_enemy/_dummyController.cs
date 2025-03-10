using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _dummyController : MonoBehaviour
{
   Animator _animator;
   float _maxHealth = 50f;
   float _currentHealth;
   public AudioClip damageClip;
   AudioSource _audio;
   void Awake()
   {
     _animator = GetComponent<Animator>();
     _currentHealth = _maxHealth;
     _audio = GetComponent<AudioSource>();
   }
 public void OnTriggerEnter(Collider _col)
   {
    if(_col == null) return;
    if(_col.CompareTag("Weapon"))
    { 
      if(_col.GetComponentInParent<_attackController>() == null) return;
      if(_col.GetComponentInParent<_attackController>()._attacking)
      {
        TakeDamage(_col.GetComponent<_weaponController>()._damage);    
      }
      if(_col.GetComponent<_cast>() != null) TakeDamage(_col.GetComponent<_cast>().damage);
    }
   }
  
   void TakeDamage(float damage)
   {
     _currentHealth -= damage;
     _animator.SetTrigger("Damage");     
     _audio.PlayOneShot(damageClip);
   }
}
