using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Optional: Add variables for hit effects
    public Material hitMaterial;
    private Material originalMaterial;
    private Renderer wallRenderer;

    void Start()
    {
        // Get the original material of the wall (assuming it has a Renderer component)
        wallRenderer = GetComponent<Renderer>();
        if (wallRenderer != null)
        {
            originalMaterial = wallRenderer.material;
        }
    }

    // This method could be called when the wall is hit
    public void OnHit()
    {
        if (wallRenderer != null && hitMaterial != null)
        {
            // Change the wall's material to a hit material
            wallRenderer.material = hitMaterial;

            // Optionally, revert back to the original material after a short time
            Invoke("ResetMaterial", 0.5f);
        }
    }

    // Reset the wall's material back to the original
    private void ResetMaterial()
    {
        if (wallRenderer != null && originalMaterial != null)
        {
            wallRenderer.material = originalMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
