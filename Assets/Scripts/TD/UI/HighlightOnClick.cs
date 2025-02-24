using UnityEngine;
public class HighlightOnClick : MonoBehaviour
{
    public Color highlightColor = Color.red; // The highlight color
    private Renderer modelRenderer;            // Reference to the model's renderer
    private Color originalColor;               // To store the original color
    private bool isHighlighted = false;        // To track highlight state

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

    void OnMouseOver()
    {
        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1))
        {
            ToggleHighlight();
        }
    }

    private void ToggleHighlight()
    {
        if (modelRenderer != null)
        {
            if (isHighlighted)
            {
                // Revert to the original color
                modelRenderer.material.color = originalColor;
            }
            else
            {
                // Change to the highlight color
                modelRenderer.material.color = highlightColor;
            }

            // Toggle the highlight state
            isHighlighted = !isHighlighted;
        }
    }
}