using System.Collections.Generic;
using UnityEngine;

public class TowerState : MonoBehaviour
{
    // Tower stats
    public float damage;
    public float attackDelayTime;
    private float attackTimer;
    public float attackRange;

    public float freezingPower;
    public float bombRadius;
    public float bombDamage;
    public float towerPrice;

    // Projectile prefab
    public GameObject projectile;
    public Vector3 offset = Vector3.zero;

    // Enemy detection
    public string enemyTag;
    public string enemyTag2;
    private HashSet<Collider> enemiesInRange = new HashSet<Collider>();

    private void Start()
    {
        attackTimer = 0f;
        gameObject.GetComponent<SphereCollider>().radius = attackRange;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelayTime && enemiesInRange.Count > 0)
        {
            Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2))
        {
            enemiesInRange.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemiesInRange.Contains(other))
        {
            enemiesInRange.Remove(other);
        }
    }

    private void Attack()
    {
        if (enemiesInRange.Count == 0) return;

        // Select first enemy in HashSet
        Collider targetEnemy = null;
        foreach (var enemy in enemiesInRange)
        {
            if (enemy != null && enemy.gameObject.activeInHierarchy)
            {
                targetEnemy = enemy;
                break;
            }
        }

        if (targetEnemy == null) return;

        // Instantiate projectile
        GameObject magic = Instantiate(projectile, transform.position + offset, transform.rotation);
        magic.name = "Projectile";

        CommonProjectile projectileComponent = magic.GetComponent<CommonProjectile>();
        if (projectileComponent != null)
        {
            projectileComponent.SetTarget(targetEnemy.name);
            projectileComponent.SetDamage(damage);
            projectileComponent.SetBombDamage(bombDamage);
            projectileComponent.SetBombRadius(bombRadius);
            projectileComponent.SetFreezingPower(freezingPower);
        }

        magic.SetActive(true);

        // Reset attack timer
        attackTimer = 0f;
    }

    public void RemoveTower()
    {
        Destroy(gameObject);
    }

    public void IncreaseDamage(float plus) => damage += plus;
    public void IncreaseAttackSpeed(float plus) => attackDelayTime = Mathf.Max(0.1f, attackDelayTime - plus);
    public void IncreaseAttackRange(float plus)
    {
        attackRange += plus;
        GetComponent<SphereCollider>().radius = attackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
