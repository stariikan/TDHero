<<<<<<< HEAD
using UnityEngine;
using UnityEngine.AI;

public class Enemy_movement : MonoBehaviour
{
    private float rotationSpeed = 3.0f;

    private Vector3 currentDirection;
    private Rigidbody rb;
    private Animator m_animator;
    private Transform modelTransform;
    private NavMeshAgent navMeshAgent;
    public GameObject target;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        m_animator = this.gameObject.GetComponent<Animator>();
        modelTransform = this.gameObject.GetComponent<Transform>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        tag = this.gameObject.tag;
        navMeshAgent.enabled = true;
    }
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        currentDirection = target.transform.position;
        navMeshAgent.destination = currentDirection;
        Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
        //modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        m_animator.SetInteger("run", 1);
    }
=======
using UnityEngine;
using UnityEngine.AI;

public class Enemy_movement : MonoBehaviour
{
    private float rotationSpeed = 3.0f;

    private Vector3 currentDirection;
    private Rigidbody rb;
    private Animator m_animator;
    private Transform modelTransform;
    private NavMeshAgent navMeshAgent;
    public GameObject target;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        m_animator = this.gameObject.GetComponent<Animator>();
        modelTransform = this.gameObject.GetComponent<Transform>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        tag = this.gameObject.tag;
        navMeshAgent.enabled = true;
    }
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        currentDirection = target.transform.position;
        navMeshAgent.destination = currentDirection;
        Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
        //modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        m_animator.SetInteger("run", 1);
    }
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
}