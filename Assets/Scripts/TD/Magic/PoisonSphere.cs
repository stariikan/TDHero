<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PoisonSphere : MonoBehaviour
{
    public float lifeTime = 6f; // Time before the projectile is destroyed
    public float poisonDamagePerSec;         // Damage dealt by the projectile
    public string enemyTag;          // Tag to identify enemies
    public string enemyTag2;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime

    public float projectileSpeed = 10f;        // Speed of the projectile
    private Transform projectileTransform;
    private SphereCollider projectileCollider;
    public Vector3 offset;
    void Start()
    {
        projectileTransform = transform;
        projectileCollider = GetComponent<SphereCollider>();
        timer = 0f;
        projectileTransform.position += offset;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                enemyStats.EnemyPoison(poisonDamagePerSec);
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
        // Move the projectile towards
        projectileTransform.position += projectileTransform.forward * projectileSpeed * Time.deltaTime;
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PoisonSphere : MonoBehaviour
{
    public float lifeTime = 6f; // Time before the projectile is destroyed
    public float poisonDamagePerSec;         // Damage dealt by the projectile
    public string enemyTag;          // Tag to identify enemies
    public string enemyTag2;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime

    public float projectileSpeed = 10f;        // Speed of the projectile
    private Transform projectileTransform;
    private SphereCollider projectileCollider;
    public Vector3 offset;
    void Start()
    {
        projectileTransform = transform;
        projectileCollider = GetComponent<SphereCollider>();
        timer = 0f;
        projectileTransform.position += offset;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                enemyStats.EnemyPoison(poisonDamagePerSec);
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
        // Move the projectile towards
        projectileTransform.position += projectileTransform.forward * projectileSpeed * Time.deltaTime;
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
