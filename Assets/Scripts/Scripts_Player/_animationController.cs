using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _animationController : MonoBehaviour
{
    [SerializeReference]_attackController attackAnimation;
    [SerializeReference]_playerController playerAnimation;

    [SerializeReference]Animator _animator;

    void Update()
    {
        _animator.SetBool("Running", playerAnimation._running);
       if(Input.GetButtonDown("Dash") && playerAnimation._dashing) _animator.SetTrigger("Dash");
        // _animator.SetInteger("ComboStep", attackAnimation._currComboSteps);
    }

    
}
