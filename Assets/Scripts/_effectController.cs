using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _effectController : MonoBehaviour
{
    public Camera main;
    Transform originalPosition;
    public GameObject SlashSfx;

    void Awake()
    {
        main = Camera.main;
        originalPosition = main.transform;
    }

    public void ShakeCameraZ()
    {
        LeanTween.rotateZ(main.gameObject, .6f, .01f).setOnComplete(ShakeCameraZVE);
        // main.transform.rotation = originalPosition.rotation;
    }
    void ShakeCameraZVE()
    {
        LeanTween.rotateZ(main.gameObject, -.6f, .01f).setOnComplete(Reset);
        // main.transform.rotation = originalPosition.rotation;
    }
    public void SpecialShake()
    {
        LeanTween.rotateZ(main.gameObject, 1f, .1f).setOnComplete(SpecialShake2);
    }
    public void SpecialShake2()
    {
        LeanTween.rotateZ(main.gameObject, -1f, .1f).setOnComplete(Reset);
    }

    public void PortalShake()
    {
        LeanTween.rotateZ(main.gameObject, 1f, .1f).setOnComplete(PortalShake2);

    }
     public void PortalShake2()
    {
        LeanTween.rotateZ(main.gameObject, -1f, .1f).setOnComplete(Reset);

    }
    void Reset()
    {
        main.transform.rotation = originalPosition.rotation;
    }
}
