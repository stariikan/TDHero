using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftEnemies : MonoBehaviour
{
    public float lifeTime; // Time before the projectile is destroyed
    public float damage;         // Damage dealt by the projectile
    public float explosionRadius; // Radius of the AOE explosion
    public float effectTime; 
    public string enemyTag;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime

    private SphereCollider collider; // Capsule collider for the enemy
    private ParticleSystem particle;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        particle = GetComponent<ParticleSystem>();
        timer = 0f;
        collider.radius = explosionRadius;
        var shape = particle.shape;  // Modify ShapeModule properties
        shape.radius = explosionRadius;  // Modify the radius
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
    public void SetLifetime(float time)
    {
        lifeTime = time;
    }
    public void SetEffecttime(float time)
    {
        effectTime = time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                enemyStats.ChangeEnemyTag(effectTime);
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
