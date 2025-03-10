using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class _enemyController : MonoBehaviour
{
   Transform _player;
   NavMeshAgent _agent;
   Animator _animator;
   float _speed = 2f;
   float _atkRange = 2f;
   public float _maxHealth = 50f;
   float _currentHealth;
   public float  _damage = 10f;

   void Awake()
   {
     _animator = GetComponent<Animator>();
     _agent = GetComponent<NavMeshAgent>();
     _currentHealth = _maxHealth;
     _agent.stoppingDistance = _atkRange;
   }
   void Update()
   { 
      if(_player == null) return;
      if(_player != null)
      {
         transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
         _agent.SetDestination(_player.position);
      }
         
      if(Vector3.Distance(_player.position, transform.position)<= _atkRange)
      {
         AttackPlayer();
      }
   }
   void OnTriggerEnter(Collider _col)
   {
        if(_col.CompareTag("Player"))
        {
        _player = _col.transform;
        _animator.SetBool("Run", true);
        }
        else 
        _animator.SetBool("Run", false);
            
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

   void AttackPlayer()
   {
     if(Vector3.Distance(transform.position, _player.position) <= _atkRange)
        _animator.SetTrigger("Attack");
   }

   public void TakeDamage(float damage)
   {
     _currentHealth -= damage;
     _animator.SetTrigger("Damage");

     if(_currentHealth <= damage)
     {
        _animator.SetBool("Die", true);
        this.enabled = false;
        StartCoroutine(Death());
     }
   }

   IEnumerator Death()
   {
      yield return new WaitForSeconds(2f);
    Destroy(gameObject);
   }
}
