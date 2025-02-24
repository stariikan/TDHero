using UnityEngine;
public class Camera_movement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Camera movement speed
    public float sensitivity = 2.0f; // Mouse sensitivity
    public bool lockCursor = false; // Lock mouse cursor

    public Transform target;  // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // How smoothly the camera follows the player
    public Vector3 offset = new Vector3(0f, 10f, 0f); // Offset from the player's position (adjust height for top-down view)

    public float minY;  // Minimum Y position
    public float maxY; // Maximum Y position

    public float cameraAngle = 90f; // Camera angle to control the tilt (default: 90 for top-down)
    private Vector3 currentVelocity; // Used for smoothing

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

      void LateUpdate() // Use LateUpdate for camera follow to ensure all player movement is complete
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned to the camera!");
            return;
        }

        // Adjust camera Y position with mouse scroll (zooming in and out)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            offset.y -= scroll * sensitivity;
            offset.y = Mathf.Clamp(offset.y, minY, maxY);
        }

        // Maintain top-down view by adjusting the camera's Y position and looking at the player
        Vector3 desiredPosition = target.position + new Vector3(offset.x, offset.y, offset.z);

        // Smoothly interpolate to the desired position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        // Set the camera's rotation to look at the player with the specified camera angle
        transform.LookAt(target.position);

        // Adjust the camera's tilt using the cameraAngle parameter
        transform.rotation = Quaternion.Euler(cameraAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // Set camera to desired angle

        if (offset.y < 10) offset.z = offset.y;
        if (offset.y > 30)
        {
            float offsetAdjaster = offset.y - 30; 
            offset.z = 10 + offsetAdjaster;
        }

    }
}