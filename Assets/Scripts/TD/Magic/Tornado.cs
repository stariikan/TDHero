<<<<<<< HEAD
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float lifeTime = 1.5f; // Time before the projectile is destroyed
    public float damage;         // Damage dealt by the projectile
    public float explosionRadius = 5f; // Radius of the AOE explosion
    public string enemyTag;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime

    private SphereCollider collider; // Capsule collider for the enemy

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        timer = 0f;
        collider.radius = explosionRadius;
        ExplosionMousePosition();
    }
    public void ExplosionMousePosition() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPosition = hit.point;
            newPosition.y += 5;  // Adjust Y position
            this.gameObject.transform.position = newPosition;  // Assign back to transform
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public void SetRadius(float radius)
    {
        explosionRadius = radius;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                enemyStats.ChangeEnemyTag();
            }
        }
        else return;
    }
    void Update()
    {
        timer += Time.deltaTime;
        // Trigger the explosion when the bomb's lifetime expires
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
=======
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float lifeTime = 1.5f; // Time before the projectile is destroyed
    public float damage;         // Damage dealt by the projectile
    public float explosionRadius = 5f; // Radius of the AOE explosion
    public string enemyTag;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime

    private SphereCollider collider; // Capsule collider for the enemy

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        timer = 0f;
        collider.radius = explosionRadius;
        ExplosionMousePosition();
    }
    public void ExplosionMousePosition() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPosition = hit.point;
            newPosition.y += 5;  // Adjust Y position
            this.gameObject.transform.position = newPosition;  // Assign back to transform
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public void SetRadius(float radius)
    {
        explosionRadius = radius;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                enemyStats.ChangeEnemyTag();
            }
        }
        else return;
    }
    void Update()
    {
        timer += Time.deltaTime;
        // Trigger the explosion when the bomb's lifetime expires
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
