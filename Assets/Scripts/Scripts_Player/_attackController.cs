using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _attackController : MonoBehaviour
{
    [Header("References")]
    public Animator _animator;
    public GameObject Default;
    public GameObject Pole;
    public GameObject Spear;
    public GameObject Scythe;
    public GameObject Sword;
    public GameObject currWeapon;
    public GameObject cast;
    public _effectController effect;

    public bool _swordIdle;
    public bool _cast;

    #region Combo
    public bool _attacking;
    public int _maxComboSteps = 3;
    public int _currComboSteps = 0;
    float _lastInput = 0f;
    [SerializeField]float _comboInputWindow = .5f;
    [SerializeField]float _attackDelay = .6f;

    public float _atkDamage = 20f;
    #endregion
    #region Special
    public bool _special;
    public float _specialCooldown = 1f;
    public float _specialDamage = 40f;
    #endregion
    #region  Unity MONOBehaviours
    void Awake()
    {
        _attacking = false;
        _special = false;
        _swordIdle = false;
        _cast = false;
        currWeapon = Default;
        effect = Camera.main.GetComponent<_effectController>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1")) Attack();
        if(Input.GetButtonDown("Fire2")) Cast();
        if(Input.GetButtonDown("Special")) Special();
    }
    #endregion

    void Attack()
    {
        if(!_attacking)
        {
            _attacking = true;
            if(Time.time - _lastInput > _comboInputWindow) 
            {
                _currComboSteps = 0;
            }
            _lastInput = Time.time;
            _currComboSteps++;
            if(_currComboSteps > _maxComboSteps) _currComboSteps = 1;

            StartCoroutine(ComboAttack());
        } 
    }
    void Special()
    {
        if(!_special)
        {
            _special = true;
            StartCoroutine(SpecialAttack());
        }
    }
    void Cast()
    {
        if(!_cast)
        {
            _cast = true;
            StartCoroutine(CastAttack());
        }
        
    }
    IEnumerator ComboAttack()
    {
        _attacking = true;
        _animator.SetInteger("ComboStep", _currComboSteps);
        currWeapon.GetComponent<_weaponController>().Slash();
        StartCoroutine(Shake());
        // StartCoroutine(SwordReset());
        yield return new WaitForSeconds(_attackDelay);
        
        _animator.SetInteger("ComboStep", 0);
        _attacking= false;
    }
    IEnumerator SpecialAttack()
    {
        _attacking = true;

        _animator.SetBool("Special", _special);
        // StartCoroutine(SwordReset());
        currWeapon.GetComponent<_weaponController>().Special();
        _specialCooldown -= Time.deltaTime;

        yield return new WaitForSeconds(_specialCooldown);
        effect.SpecialShake();

        _special = false;
        _attacking= false;

        _animator.SetBool("Special", _special);
    }
    IEnumerator CastAttack()
    {
        _animator.SetBool("Cast", true);
        // StartCoroutine(SwordReset());
        yield return new WaitForSeconds(.8f);
        Instantiate(cast, currWeapon.transform.position,transform.localRotation);
        _animator.SetBool("Cast", false);

        _cast = false;
    }
    IEnumerator SwordReset()
    {
        Sword.SetActive(false);
        _animator.SetBool("Idle With Sword", true);
        yield return new WaitForSecondsRealtime(1f);
        _animator.SetBool("Idle With Sword", false);
        Sword.SetActive(true);

    }

    IEnumerator Shake()
    {
        yield return new WaitForSeconds(.3f);
        effect.ShakeCameraZ();
    }
}
