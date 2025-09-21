using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackZone : MonoBehaviour
{
    private float damage;
    private float freezePower;
    private float timer;
    private float attackRadius = 3f;
    private Collider meleeCollider;
    public GameObject effect;
    public string enemyTag;    // Choose enemy tag
    public string enemyTag2;    // Choose second enemy tag
    public string enemyTag3;    // Choose third enemy tag
    private HashSet<Collider> enemiesInRange = new HashSet<Collider>();
    private bool canDamage; // bool for making one time damage

    private void Start()
    {
        meleeCollider = this.gameObject.GetComponent<SphereCollider>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.1 && timer < 0.2)
        {
            meleeCollider.enabled = true;
            effect.SetActive(true);
        }
        if (timer > 0.9)
        {
            meleeCollider.enabled = false;
            this.gameObject.SetActive(false);
            effect.SetActive(false);
            enemiesInRange.Clear();
            canDamage = false;
        }
        if (canDamage) 
        { 
            MeleeAttack(); 
        }
    }
    public void getDamageInfo(float dmg)
    {
        damage = dmg;
        timer = 0;
    }
    public void getFreezeInfo(float freeze)
    {
        freezePower = freeze;
    }
    public void SetAtatckRadius(float radius)
    {
        attackRadius = radius;
    }
    public void CanAttack()
    {
        canDamage = true;
    }
    private void MeleeAttack()
    {
        if (enemiesInRange.Count == 0) return;

        // Select first enemy in HashSet
        Collider targetEnemy = null;
        foreach (var enemy in enemiesInRange)
        {
            if (enemy != null && enemy.gameObject.activeInHierarchy)
            {
                targetEnemy = enemy;
                if (targetEnemy.CompareTag("Enemy") || targetEnemy.CompareTag("Flying_Enemy")) 
                {
                    targetEnemy.GetComponent<Enemy_stats>().GetDamage(damage);
                    if (freezePower > 0) targetEnemy.GetComponent<Enemy_stats>().ReduceSpeed(freezePower);
                } 
                if (targetEnemy.CompareTag("Tower"))
                {
                    targetEnemy.GetComponent<TowerState>().GetDamage(damage);
                }
                if (targetEnemy.CompareTag("Player"))
                {
                    targetEnemy.GetComponent<PlayerStats>().PlayerDamaged(damage);
                }
                if (targetEnemy.CompareTag("Npc"))
                {
                    //targetEnemy.GetComponent<PlayerStats>().PlayerDamaged(damage);
                }
                break;
            }
        }
        canDamage = false;
        if (targetEnemy == null) return;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2) || other.CompareTag(enemyTag3))
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
}
