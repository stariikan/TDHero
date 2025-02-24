using UnityEngine;

public class TDPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;         // Movement speed
    public Rigidbody rb;                // Reference to the Rigidbody component
    public Camera mainCamera;           // Reference to the main camera

    [Header("Jump Settings")]
    public float jumpForce = 5f;        // Force applied for jumping
    public LayerMask groundMask;        // Layer to identify ground
    public Transform groundCheck;       // Position for ground check
    public float groundDistance = 0.4f; // Radius for ground check sphere

    private float forwardInput;
    private float strafeInput;
    private bool isGrounded;
    private Vector3 velocity;
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Get input for forward/backward movement and strafing
        forwardInput = Input.GetAxis("Vertical");  // W/S or Up/Down arrows
        strafeInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        AnimationState();
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset vertical velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        // Apply gravity
        velocity.y += Physics.gravity.y * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Use the camera's forward and right vectors for movement direction
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Flatten the camera's forward and right vectors to ignore vertical rotation
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction in world space
        Vector3 moveDirection = cameraForward * forwardInput + cameraRight * strafeInput;
        moveDirection = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;

        // Move the player
        rb.MovePosition(rb.position + moveDirection);

        // Apply vertical velocity for jumping
        rb.velocity = new Vector3(rb.velocity.x, velocity.y, rb.velocity.z);

        // Rotate the player to face the mouse cursor
        RotateToMouseCursor();
    }
    private void AnimationState()
    {
        if (forwardInput == 0 && strafeInput == 0)
        {
            playerAnimator.SetInteger("state", 0);
        }
        if (forwardInput > 0 || strafeInput > 0)
        {
            playerAnimator.SetInteger("state", 1);
        }
    }
    private void RotateToMouseCursor()
    {
        // Cast a ray from the camera through the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Get the point where the ray hits and set the player's rotation to face it
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z); // Maintain player's Y position
            Vector3 direction = (targetPosition - transform.position).normalized;
            if (direction.magnitude > 0.1f) // Avoid jittering if the cursor is very close
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(targetRotation);
            }
        }
    }
}