using UnityEngine;

public class GlowOnHover : MonoBehaviour
{
    public Material glowMaterial; // Assign the "GlowMaterial" here

    private Material defaultMaterial;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    private void OnMouseEnter()
    {
        rend.material = glowMaterial;
    }

    private void OnMouseExit()
    {
        rend.material = defaultMaterial;
    }
}
