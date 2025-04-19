using UnityEngine;
public class CommonProjectile : MonoBehaviour
{
    // Projectile settings
    public float projectileSpeed = 10f;        // Speed of the projectile
    public float projectileLifeTime = 5f;     // Time before the projectile is destroyed
    public float projectileDamage;           // Damage dealt by the projectile
    public float freezingPower;             // Power of decreasing enemies speed
    private string targetName;             // Name of the target
    public GameObject target;             // Target object
    public Transform towerTransform;

    public int typeOfProjectile; // 0 - common damage, 1 - freeze damage, 3 - poison damage, 4 - fire damage, 5 - AOE damage

    public string enemyTag;    // Choose enemy tag
    public string enemyTag_2;    // Choose second enemy tag

    private float timer;     // Timer to track lifetime

    public GameObject aoeProjectile;     // Game object AOE projectile
    private float bombDamage;
    private float bombRadius;
    private Transform projectileTransform;
    private SphereCollider projectileCollider;

    void Start()
    {
        timer = 0f;
        projectileTransform = transform;
        projectileCollider = GetComponent<SphereCollider>();
    }
    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }
    public void SetBombDamage(float bDamage)
    {
        bombDamage = bDamage;
    }
    public void SetBombRadius(float bRadius)
    {
        bombRadius = bRadius;
    }
    public void SetFreezingPower(float fPower)
    {
        freezingPower = fPower;
    }
    public void SetTarget(string name)
    {
        targetName = name;
        FindEnemy();
    }
    public void SetTowerTransform(Transform tower)
    {
        towerTransform = tower;
    }
    private void FindEnemy()
    {
        GameObject targetObject = GameObject.Find(targetName);
        if (targetObject != null)
        {
            target = targetObject;
        }
        else
        {
            Debug.LogWarning($"Target with name {targetName} not found!");
            Destroy(gameObject); // Destroy the projectile if no target is found
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag_2))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                enemyStats.GetDamage(projectileDamage);
                if (typeOfProjectile == 1) enemyStats.ReduceSpeed(freezingPower);
                if (typeOfProjectile == 5)
                {
                    GameObject bomb = Instantiate(aoeProjectile, transform.position, transform.rotation);
                    bomb.name = "bomb" + this.gameObject.name;
                    bomb.GetComponent<Explosion>().SetDamage(bombDamage);
                    bomb.GetComponent<Explosion>().SetRadius(bombRadius);
                    bomb.SetActive(true);
                }
            }
            Destroy(gameObject); // Destroy the projectile after hitting the enemy
        }
        else return;
    }
    protected virtual void RotateTowardsTarget()
    {
        if (target != null)
            projectileTransform.LookAt(target.transform);
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        // Destroy the projectile if it exceeds its lifetime
        if (timer >= projectileLifeTime)
        {
            Destroy(gameObject);
            return;
        }

        if (target != null)
        {
            Vector3 direction = (target.transform.position - projectileTransform.position).normalized;
            projectileTransform.position += direction * projectileSpeed * Time.deltaTime;

            // Now call overridable method
            RotateTowardsTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}