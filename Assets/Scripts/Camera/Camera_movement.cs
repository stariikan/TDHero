using UnityEngine;
public class Camera_movement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Camera movement speed
    public float sensitivity = 2.0f; // Mouse sensitivity
    public bool lockCursor = false; // Lock mouse cursor

    public Transform target;  // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // How smoothly the camera follows the player

    public float minY;  // Minimum Y position
    public float maxY; // Maximum Y position
    private Vector3 currentVelocity; // Used for smoothing

    //Camera Modes
    public int cameraMode;
    public GameObject cameraIcon;
    
    public Vector3 totalWarCamera;
    public float totalWarCameraAngle;

    public Vector3 lolCamera;
    public float lolCameraAngle;

    public Vector3 oldCamera;
    public float oldCameraAngle;

    private Vector3 offset;
    private float cameraAngle; // Camera angle to control the tilt (default: 90 for top-down)


    void Start()
    {
        cameraMode = 1;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (target == null)
        {
            Debug.LogWarning("Target not assigned to the camera!");
            return;
        }
        ChangeCamera();
    }
    public void ChangeCamera()
    {
        if (cameraMode == 1) 
        {
            offset = oldCamera;
            cameraAngle = oldCameraAngle;
        }
        if (cameraMode == 2)
        {
            offset = lolCamera;
            cameraAngle = lolCameraAngle;
        }
        if (cameraMode == 3)
        {
            offset = totalWarCamera;
            cameraAngle = totalWarCameraAngle;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (cameraMode == 1)
                cameraMode = 2;
            else if (cameraMode == 2)
                cameraMode = 3;
            else if (cameraMode == 3)
                cameraMode = 1;

            ChangeCamera();
        }
    }
    void LateUpdate() // Use LateUpdate for camera follow to ensure all player movement is complete
    {
        // Maintain top-down view by adjusting the camera's Y position and looking at the player
        Vector3 desiredPosition = target.position + new Vector3(offset.x, offset.y, offset.z);

        // Smoothly interpolate to the desired position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        // Set the camera's rotation to look at the player with the specified camera angle
        transform.LookAt(target.position);

        // Adjust the camera's tilt using the cameraAngle parameter
        transform.rotation = Quaternion.Euler(cameraAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // Set camera to desired angle
    }
}