using UnityEngine;
public class TowerState : MonoBehaviour
{
    // Tower stats
    public float damage;                // Damage of the tower
    public float attackDelayTime;      // Attack speed
    public float attackTimer;         // Timer for attack speed
    public float attackRange;        // Attack range of the Tower

    public float freezingPower;    

    public float bombRadius;
    public float bombDamage;

    public float towerPrice;        // Building price or Upgrade price

    // Game object of projectile (shell)
    public GameObject projectile;

    public Vector3 offset = new Vector3(0, 0, 0);

    // Layer mask for detecting enemies
    public LayerMask enemyLayer;

    void Start()
    {
        attackTimer = 0;
    }

    public void RemoveTower()
    {
        Destroy(this.gameObject);
    }

    public void IncreaseDamage(float plus)
    {
        damage += plus;
    }

    public void IncreaseAttackSpeed(float plus)
    {
        attackDelayTime -= plus;
    }

    public void IncreaseAttackRange(float plus)
    {
        attackRange += plus; // Increase range dynamically
    }

    private void DetectEnemiesAndAttack()
    {
        // Detect all enemies within the attack range
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        // If there are enemies, attack the first one
        if (enemiesInRange.Length > 0 && attackTimer >= attackDelayTime)
        {
            Collider targetEnemy = enemiesInRange[0]; // Pick the first detected enemy
            GameObject magic = Instantiate(projectile, transform.position + offset, transform.rotation);
            magic.name = "projectile";
            magic.GetComponent<CommonProjectile>().SetTarget(targetEnemy.name);
            magic.GetComponent<CommonProjectile>().SetDamage(damage);
            magic.GetComponent<CommonProjectile>().SetBombDamage(bombDamage);
            magic.GetComponent<CommonProjectile>().SetBombRadius(bombRadius);
            magic.GetComponent<CommonProjectile>().SetFreezingPower(freezingPower);
            magic.SetActive(true);
            attackTimer = 0;
        }
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        // Check for enemies and attack
        DetectEnemiesAndAttack();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
