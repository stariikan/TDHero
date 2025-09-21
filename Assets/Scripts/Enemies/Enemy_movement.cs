using UnityEngine;
using UnityEngine.AI;

public class Enemy_movement : MonoBehaviour
{
    public float rotationSpeed;
    public float moveSpeed;

    public GameObject target;
    private Transform objTransform;
    private NavMeshAgent navMeshAgent;
    private Animator m_animator;

    private float startY;
    public bool useNavMesh;
    private bool isTagChanged;
    public string gameObjTag;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        navMeshAgent.speed = moveSpeed;
        objTransform = transform;
        startY = objTransform.position.y;
        isTagChanged = false;
        useNavMesh = true;
        gameObjTag = tag;
        navMeshAgent.enabled = true;

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent not found!");
            return;
        }
    }

    void Update()
    {
        if (useNavMesh)
        {
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(target.transform.position);
            if (m_animator != null) m_animator.SetInteger("run", 1);
            objTransform.position = Vector3.Lerp(objTransform.position, new Vector3(objTransform.position.x, startY, objTransform.position.z), 1f * Time.deltaTime);
        }
        CheckDistanceWithTarget();
    }
    private void CheckDistanceWithTarget()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            //Debug.Log(distanceToTarget);
            if (distanceToTarget < 5f)
            {
                navMeshAgent.enabled = false;
                this.gameObject.GetComponent<Enemy_AI>().EnemyAttack();
            }
            if (distanceToTarget >= 5f)
            {
                navMeshAgent.enabled = true;
            }
        }
    }

    public void ChangeTag() => isTagChanged = true;
    public void ReturnTag() => isTagChanged = false;

    public void GoingUp()
    {
        float targetY = startY + 2f;
        objTransform.position = Vector3.Lerp(objTransform.position, new Vector3(objTransform.position.x, targetY, objTransform.position.z), 1f * Time.deltaTime);
    }

    public void GoingDown()
    {
        float targetY = startY;
        objTransform.position = Vector3.Lerp(objTransform.position, new Vector3(objTransform.position.x, targetY, objTransform.position.z), 1f * Time.deltaTime);
    }

    public void SetDestination(Vector3 destination)
    {
        if (navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(destination);
        }
    }
    public void ChooseTarget(GameObject targetObject)
    {
        target = targetObject;
    }
}
