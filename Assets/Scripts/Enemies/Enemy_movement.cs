using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy_movement : MonoBehaviour
{
    public float rotationSpeed = 3.0f;
    public GameObject target;
    private Transform objTransform;
    private NavMeshAgent navMeshAgent;
    private Animator m_animator;
    private float startY;
    private bool isTagChanged;
    private string gameObjTag;
    void Start()
    {
        if (target == null) target = GameObject.Find("Finish");
        navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        objTransform = GetComponent<Transform>();
        startY = objTransform.position.y;
        isTagChanged = false;
        gameObjTag = tag;

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent not found!");
            return;
        }

        navMeshAgent.enabled = true;
        SetDestination(target.transform.position);
    }

    void Update()
    {
         if (isTagChanged == false)
         {
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(target.transform.position);
            //m_animator.SetInteger("run", 1);
            objTransform.position = Vector3.Lerp(objTransform.position, new Vector3(objTransform.position.x, startY, objTransform.position.z), 1f * Time.deltaTime);
        }
         if (isTagChanged == true)
        {
            navMeshAgent.enabled = false;
            if (gameObjTag == "Enemy")
            {
                GoingUp();
            }
            if (gameObjTag == "Flying_Enemy")
            {
                GoingDown();
            }
        }
    }
    public void ChangeTag()
    {
        isTagChanged = true;
    }
    public void ReturnTag()
    {
        isTagChanged = false;
    }
    public void GoingUp()
    {
        float targetY = startY + 2;
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
}
