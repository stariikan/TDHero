using System.Collections.Generic;
using UnityEngine;

public class TDPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;         // Movement speed
    public Rigidbody rb;                // Reference to the Rigidbody component
    public Camera mainCamera;           // Reference to the main camera
    private bool rushToTarget;
    private float dashTimer;
    public bool dashing;

    [Header("Detection")]
    public LayerMask enemyLayer;
    public LayerMask enemyLayer_2;

    [Header("Enemy Detection")]
    public float interestZone = 15f;
    public string enemyTag;
    public string enemyTag2;
    public GameObject currentTarget;
    private HashSet<Collider> enemiesInZone = new HashSet<Collider>();

    private float forwardInput;
    private float strafeInput;
    private bool isGrounded;
    private bool isAlive;
    private bool isAttacking;
    private Vector3 velocity;
    private Animator playerAnimator;
    private Animator playerAnimator_Demon;

    [Header("Player Mode")]
    private bool playerDemon;
    public GameObject playerModel;
    public GameObject playerModel_Demon;
    public GameObject dash_trace;
    private float timer;

    [Header("Mouse State")]
    private bool rightMouseActive;
    private bool leftMouseActive;

    private void Start()
    {
        rightMouseActive = false;
        leftMouseActive = false;
        isAttacking = false;
        isAlive = true;
        rushToTarget = false;

        if (playerDemon == true)
        {
            playerModel.SetActive(false);
            playerModel_Demon.SetActive(true);
            playerAnimator = playerModel_Demon.GetComponent<Animator>();
        }
        else
        {
            playerModel.SetActive(true);
            playerModel_Demon.SetActive(false);
            playerAnimator = playerModel.GetComponent<Animator>();
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        dashTimer += Time.deltaTime;
        playerDemon = this.gameObject.GetComponent<PlayerStats>().playerDemon;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashing == false)
        {
            Dash();
        }
        if (dashTimer > 0.5f && dashing == true)
        {
            dashing = false;
            moveSpeed = 10f;
            this.gameObject.GetComponent<PlayerStats>().GodModeOff();
        }
        if (timer > 1.02f) StopAttack();
        MouseButtonState();
        AnimationState();

        if (!rightMouseActive || currentTarget == null) UpdateEnemiesInZone();
        if (rightMouseActive) FindTarget();

        if (rightMouseActive && isAttacking && currentTarget != null) RotateToTarget();
        if (playerDemon == true)
        {
            playerModel.SetActive(false);
            playerModel_Demon.SetActive(true);
            playerAnimator = playerModel_Demon.GetComponent<Animator>();
        }
        else
        {
            playerModel.SetActive(true);
            playerModel_Demon.SetActive(false);
            playerAnimator = playerModel.GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive && !isAttacking)
        {
            // Get input for forward/backward movement and strafing
            forwardInput = Input.GetAxis("Vertical");  // W/S or Up/Down arrows
            strafeInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        }
        if (isAlive && isAttacking && currentTarget != null)
        {
            Vector3 moveToTarget = currentTarget.transform.position - transform.position;
            moveToTarget.y = 0f; // ignore vertical

            if (moveToTarget.magnitude > 2.5f && rushToTarget && currentTarget != null) // small buffer to stop right at the target
            {
                Vector3 direction = moveToTarget.normalized * moveSpeed * 2;
                rb.velocity = new Vector3(direction.x, velocity.y, direction.z);
            }
            else
            {
                rushToTarget = false;
                rb.velocity = new Vector3(0, velocity.y, 0); // stop moving when close
            }

            return; // Skip player input-based movement
        }
        // Use the camera's forward and right vectors for movement direction
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Flatten the camera's forward and right vectors to ignore vertical rotation
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate target movement
        Vector3 targetDirection = cameraForward * forwardInput + cameraRight * strafeInput;
        targetDirection.Normalize();

        // Smooth movement
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = targetDirection * moveSpeed;
        Vector3 smoothVelocity = Vector3.Lerp(currentVelocity, targetVelocity, 10f * Time.fixedDeltaTime);

        // Apply horizontal movement (preserve vertical velocity for jumping)
        rb.velocity = new Vector3(smoothVelocity.x, velocity.y, smoothVelocity.z);

        // Rotate toward moving direction smoothly
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }
    private void UpdateEnemiesInZone()
    {
        // Get all enemies currently in the interest zone
        Collider[] detected = Physics.OverlapSphere(transform.position, interestZone, enemyLayer | enemyLayer_2);

        // Convert to set for easier comparison
        HashSet<Collider> detectedSet = new HashSet<Collider>(detected);

        // Remove enemies that have left
        enemiesInZone.RemoveWhere(collider => !detectedSet.Contains(collider));

        // Add new enemies
        foreach (var col in detectedSet)
        {
            if (!enemiesInZone.Contains(col))
            {
                enemiesInZone.Add(col);
            }
        }
    }
    public void FindTarget()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (var enemy in enemiesInZone)
        {
            if (enemy != null && enemy.gameObject.activeInHierarchy)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.gameObject;
                }
            }
        }
        currentTarget = closestEnemy;
    }
    public void isDead()
    {
        isAlive = false;
    }
    public void AttackTrigger()
    {
        int randomAttack = Random.Range(0, 3);
        if (randomAttack == 0) playerAnimator.SetTrigger("attack");
        if (randomAttack == 1) playerAnimator.SetTrigger("attack_2");
        if (randomAttack == 2) playerAnimator.SetTrigger("attack_3");
        if (randomAttack == 3) playerAnimator.SetTrigger("attack_4");
        rushToTarget = true;
        isAttacking = true;
        timer = 0;
    }
    public void StopAttack()
    {
        isAttacking = false;
    }
    private void Dash()
    {
        dashing = true;
        dashTimer = 0;
        moveSpeed = 30f;
        this.gameObject.GetComponent<PlayerStats>().GodModeOn();
        //dash_trace.SetActive(true);
    }
    private void AnimationState()
    {
        if (isAlive)
        {
            if (forwardInput == 0 && strafeInput == 0)
            {
                playerAnimator.SetInteger("state", 0);
            }
            if (forwardInput > 0 || strafeInput > 0)
            {
                playerAnimator.SetInteger("state", 1);
            }
            if (forwardInput < 0 || strafeInput > 0)
            {
                playerAnimator.SetInteger("state", 1);
            }
            if (forwardInput > 0 || strafeInput < 0)
            {
                playerAnimator.SetInteger("state", 1);
            }
            if (forwardInput < 0 || strafeInput < 0)
            {
                playerAnimator.SetInteger("state", 1);
            }
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
    private void RotateToTarget()
    {

        Vector3 targetPosition = currentTarget.transform.position;

        // Flatten the direction on the horizontal (XZ) plane
        Vector3 direction = new Vector3(
            targetPosition.x - transform.position.x,
            0f,
            targetPosition.z - transform.position.z
        ).normalized;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(targetRotation);
        }
    }
    private void MouseButtonState()
    {
        if (Input.GetMouseButtonDown(1)) rightMouseActive = true;
        if (Input.GetMouseButtonUp(1)) rightMouseActive = false;
        if (Input.GetMouseButtonDown(0)) leftMouseActive = true;
        if (Input.GetMouseButtonUp(0)) leftMouseActive = false;
    }
}