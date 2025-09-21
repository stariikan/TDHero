using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [Header("Stats")]
    public float damage;
    public float freezePower;
    public float attackSpeed;
    public float attackRange;
    public float interestZone = 10f;
    public GameObject attackZone;

    [Header("Detection")]
    public LayerMask towerLayer;
    public LayerMask npcLayer;

    public GameObject player;
    public GameObject finish;

    [Header("Targeting Priorities")]
    public bool toFinish;
    public bool toTower;
    public bool toNpc;
    public bool toPlayer;

    [HideInInspector] public GameObject currentTarget;

    private float timer;
    private Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        GoToTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;
        GoToTarget();
    }

    public void GoToTarget()
    {
        // Highest priority: Tower
        if (toTower == true)
        {
            Collider[] towers = Physics.OverlapSphere(transform.position, interestZone, towerLayer);
            foreach (var tower in towers)
            {
                if (tower != null && tower.gameObject.activeInHierarchy)
                {
                    currentTarget = tower.gameObject;
                    GetComponent<Enemy_movement>().ChooseTarget(currentTarget);
                    return;
                }
            }
        }

        // Second priority: NPC
        if (toNpc == true)
        {
            Collider[] npcs = Physics.OverlapSphere(transform.position, interestZone, npcLayer);
            foreach (var npc in npcs)
            {
                if (npc != null && npc.gameObject.activeInHierarchy)
                {
                    currentTarget = npc.gameObject;
                    GetComponent<Enemy_movement>().ChooseTarget(currentTarget);
                    return;
                }
            }
        }

        // Third priority: Player
        if (toPlayer && toFinish == false && currentTarget == null)
        {
            currentTarget = player;
            GetComponent<Enemy_movement>().ChooseTarget(currentTarget);
        }

        // Last resort: Finish line
        if (toFinish && toPlayer == false && currentTarget == null)
        {
            currentTarget = finish;
            GetComponent<Enemy_movement>().ChooseTarget(currentTarget);
        }
    }

    // You can still keep these methods to set priorities externally
    public void GoToFinish(bool state) => toFinish = state;
    public void GoToTower(bool state) => toTower = state;
    public void GoToNpc(bool state) => toNpc = state;
    public void GoToPlayer(bool state) => toPlayer = state;

    public void EnemyAttack()
    {
        if (timer > attackSpeed)
        {
            timer = 0;
            if (m_animator != null) m_animator.SetTrigger("attack");
            //Debug.Log("Attack target: " + currentTarget);
            attackZone.GetComponent<MeleeAttackZone>().getDamageInfo(damage);
            attackZone.GetComponent<MeleeAttackZone>().getFreezeInfo(freezePower);
            attackZone.GetComponent<MeleeAttackZone>().SetAtatckRadius(attackRange);
            attackZone.GetComponent<MeleeAttackZone>().CanAttack();
            attackZone.SetActive(true);   
        }
        
    }
    public void GetDamageStat(float stat)
    {
        damage = stat;
    }
    public void GetFreezePowerStat(float stat)
    {
        freezePower = stat;
    }
    public void GetAttackSpeedStat(float stat)
    {
        attackSpeed = stat;
    }
    public void GetAttackRangeStat(float stat)
    {
        attackRange = stat;
    }
    public void GetInterestZoneStat(float stat)
    {
        interestZone = stat;
    }
}
