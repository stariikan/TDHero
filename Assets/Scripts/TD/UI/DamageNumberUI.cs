<<<<<<< HEAD
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberUI : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera here
    public Image imageBar;
    public Transform target; // Assign the enemy's transform
    public Vector3 offset = new Vector3(0, 0, 0); // Adjust this to place the health bar above the enemy

    private bool isMovingUp = false; // Flag to prevent multiple coroutine calls

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the health bar's position to follow the enemy
            transform.position = target.position + offset;
        }

        if (mainCamera != null)
        {
            // Make the health bar face the camera
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
        if (!isMovingUp) // Prevent multiple coroutine calls
        {
            StartCoroutine(MoveUpAndDestroy());
        }
    }

    public void ActivateBar()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateBar()
    {
        gameObject.SetActive(false);
    }

    public void BarUpdate(float fraction)
    {
        imageBar.fillAmount = fraction;
    }

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera
        }
    }

    IEnumerator MoveUpAndDestroy()
    {
        isMovingUp = true; // Prevent multiple coroutine calls
        float duration = 1f; // Move up in 1 second
        float time = 0;

        float startY = offset.y; // Store original Y offset
        float targetY = startY + 4; // Move up by 5 units

        float randomX = Random.Range(0f, 4f); // Random X amplitude for each coroutine instance
        float startX = offset.x; // Store original X position
        float targetX = startX + randomX; // Move X randomly

        // Move up with horizontal sway
        while (time < duration)
        {
            time += Time.deltaTime;
            offset.y = Mathf.Lerp(startY, targetY, time / duration);
            offset.x = Mathf.Lerp(startX, targetX, Mathf.Sin(time / duration * Mathf.PI)); // Smooth oscillation
            yield return null;
        }

        // Ensure final position is exactly at the target
        offset.y = targetY;
        offset.x = targetX;

        // Wait for X seconds before moving down
        yield return new WaitForSeconds(0.5f);

        // Reset time for downward motion
        time = 0;
        float downY = targetY - 2; // Move back down

        // Move down smoothly
        while (time < duration)
        {
            time += Time.deltaTime;
            offset.y = Mathf.Lerp(targetY, downY, time / duration);
            yield return null;
        }

        // Ensure it ends at the correct position
        offset.y = downY;

        // Wait for X second before destroying
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject); // Destroy after all movements are completed
    }
}
=======
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberUI : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera here
    public Image imageBar;
    public Transform target; // Assign the enemy's transform
    public Vector3 offset = new Vector3(0, 0, 0); // Adjust this to place the health bar above the enemy

    private bool isMovingUp = false; // Flag to prevent multiple coroutine calls

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the health bar's position to follow the enemy
            transform.position = target.position + offset;
        }

        if (mainCamera != null)
        {
            // Make the health bar face the camera
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
        if (!isMovingUp) // Prevent multiple coroutine calls
        {
            StartCoroutine(MoveUpAndDestroy());
        }
    }

    public void ActivateBar()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateBar()
    {
        gameObject.SetActive(false);
    }

    public void BarUpdate(float fraction)
    {
        imageBar.fillAmount = fraction;
    }

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera
        }
    }

    IEnumerator MoveUpAndDestroy()
    {
        isMovingUp = true; // Prevent multiple coroutine calls
        float duration = 1f; // Move up in 1 second
        float time = 0;

        float startY = offset.y; // Store original Y offset
        float targetY = startY + 4; // Move up by 5 units

        float randomX = Random.Range(0f, 4f); // Random X amplitude for each coroutine instance
        float startX = offset.x; // Store original X position
        float targetX = startX + randomX; // Move X randomly

        // Move up with horizontal sway
        while (time < duration)
        {
            time += Time.deltaTime;
            offset.y = Mathf.Lerp(startY, targetY, time / duration);
            offset.x = Mathf.Lerp(startX, targetX, Mathf.Sin(time / duration * Mathf.PI)); // Smooth oscillation
            yield return null;
        }

        // Ensure final position is exactly at the target
        offset.y = targetY;
        offset.x = targetX;

        // Wait for X seconds before moving down
        yield return new WaitForSeconds(0.5f);

        // Reset time for downward motion
        time = 0;
        float downY = targetY - 2; // Move back down

        // Move down smoothly
        while (time < duration)
        {
            time += Time.deltaTime;
            offset.y = Mathf.Lerp(targetY, downY, time / duration);
            yield return null;
        }

        // Ensure it ends at the correct position
        offset.y = downY;

        // Wait for X second before destroying
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject); // Destroy after all movements are completed
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
