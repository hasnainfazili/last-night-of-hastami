using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _weaponController : MonoBehaviour
{
    public enum Weapon{
        DEFAULT,
        POLE,
        SPEAR,
        SCYTHE,
        SWORD
    }
    public float _damage = 10f;
    public bool isEquipped;
    public bool isPurchased;
    public float purchaseAmount;
    public Weapon type;
    public GameObject SlashSfx , SpecialSfx;
    Transform player;
    //type
    void Awake()
    {
        if(GetComponentInParent<_attackController>() != null)
        {
            player = GetComponentInParent<_attackController>().transform;
        }
       if(type == Weapon.POLE) purchaseAmount = 10f;
       if(type == Weapon.SPEAR) purchaseAmount = 100f;
       if(type == Weapon.SCYTHE) purchaseAmount = 200f;
       if(type == Weapon.SWORD) purchaseAmount = 500f;
       isPurchased =false;
    }

   void Update()
   {
    if(SlashSfx == null) return;
    if(SpecialSfx == null) return;
    if(isEquipped == false) 
    {
        transform.Rotate(0,45*Time.deltaTime,0);
    }
   }

   public void Slash()
   {
    if(player.GetComponent<_attackController>()._attacking)
        StartCoroutine(PlaySlash());
   } 
    public void Special()
   {
    if(player.GetComponent<_attackController>()._attacking)
        StartCoroutine(PlaySpecial());
   }
   IEnumerator PlaySlash()
    {
        yield return new WaitForSeconds(.2f);
        Instantiate(SlashSfx, transform.position,transform.rotation);
    }
    IEnumerator PlaySpecial()
    {
        yield return new WaitForSeconds(.8f);
        Instantiate(SpecialSfx, new Vector3(transform.position.x, 0, transform.position.z + .6f), Quaternion.identity);
    }
}
