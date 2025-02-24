<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float bombLifeTime = 1.5f; // Time before the projectile is destroyed
    public float bombDamage;         // Damage dealt by the projectile
    public float explosionRadius = 5f; // Radius of the AOE explosion
    public string enemyTag;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime

    void Start()
    {
        timer = 0f;
    }
    public void SetDamage(float damage)
    {
        bombDamage = damage;
    }
    public void SetRadius (float radius)
    {
        explosionRadius = radius;
    }
    private void Explode()
    {
        // Find all colliders within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                Enemy_stats enemyStats = hitCollider.GetComponent<Enemy_stats>();
                if (enemyStats != null)
                {
                    enemyStats.GetDamage(bombDamage);
                }
            }
        }

        // Destroy the bomb object after the explosion
        Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Trigger the explosion when the bomb's lifetime expires
        if (timer >= bombLifeTime)
        {
            Explode();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float bombLifeTime = 1.5f; // Time before the projectile is destroyed
    public float bombDamage;         // Damage dealt by the projectile
    public float explosionRadius = 5f; // Radius of the AOE explosion
    public string enemyTag;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime

    void Start()
    {
        timer = 0f;
    }
    public void SetDamage(float damage)
    {
        bombDamage = damage;
    }
    public void SetRadius (float radius)
    {
        explosionRadius = radius;
    }
    private void Explode()
    {
        // Find all colliders within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                Enemy_stats enemyStats = hitCollider.GetComponent<Enemy_stats>();
                if (enemyStats != null)
                {
                    enemyStats.GetDamage(bombDamage);
                }
            }
        }

        // Destroy the bomb object after the explosion
        Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Trigger the explosion when the bomb's lifetime expires
        if (timer >= bombLifeTime)
        {
            Explode();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
