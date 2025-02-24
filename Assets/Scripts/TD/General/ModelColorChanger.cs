using System.Collections;
using UnityEngine;
public class ModelColorChanger : MonoBehaviour
{
    public Color newColor = Color.red; // The color to change to
    public float duration = 2f;        // How long to keep the new color

    private Renderer modelRenderer;    // Reference to the model's renderer
    private Color originalColor;       // To store the original color

    void Start()
    {
        // Get the Renderer component of the model
        modelRenderer = GetComponent<Renderer>();

        // Ensure the renderer exists
        if (modelRenderer != null)
        {
            // Store the original color of the model
            originalColor = modelRenderer.material.color;
        }
        else
        {
            Debug.LogError("Renderer not found on the object!");
        }
    }

    public void ChangeColorTemporarily()
    {
        if (modelRenderer != null)
        {
            // Start the coroutine to change the color and revert it back
            StartCoroutine(ChangeColorRoutine());
        }
    }

    private IEnumerator ChangeColorRoutine()
    {
        // Change the model's color
        modelRenderer.material.color = newColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Revert the color back to the original
        modelRenderer.material.color = originalColor;
    }
}
