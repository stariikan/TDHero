using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceThrowerProjectile : CommonProjectile
{
    public float tickRate = 1f; // how often damage is dealt
    public float effectDuration = 5f; // how long it lives
    private float lifeTimer = 0f;
    private float tickTimer = 0f;
    public Vector3 offset;
    private List<Enemy_stats> enemiesInArea = new List<Enemy_stats>();

    void Start()
    {
        // Prevent CommonProjectile from doing its default OnTriggerEnter logic
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        void Update()
        {
            base.Update(); // handles lifespan, etc.

            tickTimer += Time.deltaTime;
            lifeTimer += Time.deltaTime;

            if (tickTimer >= tickRate)
            {
                tickTimer = 0f;
                ApplyEffects();
            }

            if (lifeTimer >= effectDuration)
            {
                Destroy(gameObject);
            }

            // Position follows enemy with offset
            if (towerTransform != null && target != null)
            {
                transform.position = target.transform.position + target.transform.TransformDirection(offset);

                // Face away from the tower (outward direction)
                Vector3 outwardDirection = (transform.position - towerTransform.position).normalized;
                if (outwardDirection.sqrMagnitude > 0.001f)
                    transform.rotation = Quaternion.LookRotation(outwardDirection);
            }
        }

    }

    private void ApplyEffects()
    {
        for (int i = enemiesInArea.Count - 1; i >= 0; i--)
        {
            if (enemiesInArea[i] != null)
            {
                enemiesInArea[i].GetDamage(projectileDamage);
                enemiesInArea[i].ReduceSpeed(freezingPower);
            }
            else
            {
                enemiesInArea.RemoveAt(i);
            }
        }
    }
    protected override void RotateTowardsTarget()
    {
        if (towerTransform != null && target != null)
        {
            // Make it face away from tower (if that's what you want)
            Vector3 direction = (transform.position - towerTransform.position).normalized;
            if (direction.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag_2))
        {
            if (other.TryGetComponent(out Enemy_stats enemy))
            {
                if (!enemiesInArea.Contains(enemy))
                    enemiesInArea.Add(enemy);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            if (enemiesInArea.Contains(enemy))
                enemiesInArea.Remove(enemy);
        }
    }
}
