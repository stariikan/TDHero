using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_stats : MonoBehaviour
{
    public float e_Speed;               // Enemy movement speed
    public float e_maxHP;               // Maximum health
    public float e_currentHP;          // Current health
    public float coinReward;            // Reward for death
    private string enemyStarterTag; //Enemy Tag that was on the start
    private CapsuleCollider bodyCollider; // Capsule collider for the enemy

    private bool isStun; //Enemy stuned? 
    private bool isFreezing; //Enemy freeze?
    private bool isPoisoned; //Enemy poisened?
    private bool isTagChanged; //Enemys tag changed?

    private int e_poison_lvl;  // Current poison lvl
    private float e_poison_damage; // damage per tick

    private float e_timerStunRecovery;        // Timer for Stun effect
    private float e_timerFreezing;      // Timer for freezing effect
    private float e_timerPoisonRecovery; // Timer for Poison Recovery
    private float e_timereTagRecovery; // Timer for Tag changing effect

    public float stunRecoveryTime;        // Timer for Stun effect
    public float freezingRecoveryTime;      // Timer for freezing effect
    public float poisonRecoveryTime; // Timer for Poison Recovery
    public float tagRecoveryTime; // Timer for Tag changing effect

    private float timerPerSec = 0; //

    public GameObject finishLine;        // Reference to the finish line object
    public GameObject player;            // Reference to the player object
    public GameObject uiHealthBar;      // Reference to Health Bar object
    public GameObject freezeIcon;       // Reference to FreezeIcon
    public GameObject stunIcon;         // Reference for Stun Icon
    public GameObject poisonIcon;       // Reference for Poison Icon
    public GameObject takenDamageUi;    // Reference for DamageNumbers Icon

    public GameObject MainCamera; // MainCamera object for access to Monster_Generate script.

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        e_currentHP = e_maxHP;
        e_poison_lvl = 0;
        bodyCollider = GetComponent<CapsuleCollider>();
        isStun = false;
        isFreezing = false;
        isPoisoned = false;
        isTagChanged = false;
        enemyStarterTag = tag;

        if (finishLine == null)
        {
            Debug.LogWarning("Finish line is not assigned!");
        }

        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        e_Speed = navMeshAgent.speed;
    }

    void Update()
    {
        RecoveryAfterEffects();
    }
    public void RecoveryAfterEffects()
    {
        if (isFreezing == true)
        {
            e_timerFreezing += Time.deltaTime;
            if (e_timerFreezing > freezingRecoveryTime && isStun == false)
            {
                freezeIcon.GetComponent<UIBarLogic>().DeactivateBar();
                navMeshAgent.speed = e_Speed;
                isFreezing = false;
            }
        }
        if (isStun == true)
        {
            e_timerStunRecovery += Time.deltaTime;
            if (e_timerStunRecovery > stunRecoveryTime)
            {
                isStun = false;
                stunIcon.GetComponent<UIBarLogic>().DeactivateBar();
            }
        }
        if (isTagChanged == true)
        {
            e_timereTagRecovery += Time.deltaTime;
            if (e_timereTagRecovery > tagRecoveryTime)
            {
                tag = enemyStarterTag;
                isTagChanged = false;
            }
        }
        if (isPoisoned == true)
        {
            e_timerPoisonRecovery += Time.deltaTime;
            timerPerSec += Time.deltaTime;
            if (timerPerSec > 1 && e_timerPoisonRecovery < poisonRecoveryTime)
            {
                GetDamage(e_poison_damage * e_poison_lvl);
                timerPerSec = 0;
            }
            if (e_timerPoisonRecovery > poisonRecoveryTime)
            {
                e_poison_lvl = 0;
                poisonIcon.GetComponent<UIBarLogic>().DeactivateBar();
                isPoisoned = false;
            }
        }
        if (navMeshAgent.speed < e_Speed * 0.4f && isStun == false) navMeshAgent.speed = e_Speed * 0.4f;
    }
    public void Stun()
    {
        isStun = true;
        e_timerStunRecovery = 0;
        navMeshAgent.speed = 0;
        stunIcon.GetComponent<UIBarLogic>().ActivateBar();
    }
    public void ChangeEnemyTag(float recoveryTime)
    {
        isTagChanged = true;
        if (CompareTag("Enemy")) tag = "Flying_Enemy";
        else if (CompareTag("Flying_Enemy")) tag = "Enemy";
        e_timereTagRecovery = 0;
        tagRecoveryTime = recoveryTime;
        Debug.Log(tag);
    }
    public void ReduceSpeed(float freezeDmg)
    {
        isFreezing = true;
        navMeshAgent.speed *= freezeDmg;
        freezeIcon.GetComponent<UIBarLogic>().ActivateBar();
        e_timerFreezing = 0;
    }
    public void EnemyPoison(float damage)
    {
        e_poison_damage = damage;
        e_poison_lvl += 1;
        isPoisoned = true;
        poisonIcon.GetComponent<UIBarLogic>().ActivateBar();
        e_timerPoisonRecovery = 0;

    }
    public void GetHP(float heal)
    {
        e_currentHP += heal;
        if (e_currentHP > e_maxHP) e_currentHP = e_maxHP;
    }
    private void OnReachFinishLine()
    {
        // Example: Reduce player health or trigger a game over
        Debug.Log("Enemy has reached the finish line. Triggering event.");
        player.GetComponent<PlayerStats>().VillageDamaged(1);
        MainCamera.GetComponent<Monster_Generate>().KillMonster();
        e_currentHP = 0;
        DeadAndDestroy();
    }
    public void HealthBarUI()
    {
        float ratio = e_currentHP / e_maxHP;
        uiHealthBar.GetComponent<UIBarLogic>().BarUpdate(ratio);
    }
    public void GetDamage(float damage)
    {
        if (damage >= 0.1)
        {
            e_currentHP -= damage;
            HealthBarUI();
            // Create a copy of this GameObject
            GameObject damageUI = Instantiate(takenDamageUi, transform.position, Quaternion.identity);
            damageUI.transform.SetParent(takenDamageUi.transform.parent, false);

            // Optionally, set the parent of the copied object to match the original's parent
            damageUI.GetComponent<TextMeshProUGUI>().text = $"{damage}";
            damageUI.SetActive(true);

            if (e_currentHP <= 0)
            {
                e_currentHP = 0;
                player.GetComponent<PlayerStats>().CoinPlus(coinReward);
                MainCamera.GetComponent<Monster_Generate>().KillMonster();
                DeadAndDestroy();
            }
        }
    }
    public void DeadAndDestroy()
    {
        Destroy(gameObject); // Destroy the enemy game object
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == finishLine)
        {
            Debug.Log("Enemy reached the finish line!");
            OnReachFinishLine();
        }
    }
}
