using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp; //player HP
    public float maxhp;
    public float villageHp; //HP of the village
    public float coins; //money for upgrade of abilities and towers

    public bool godmode;

    public float takkenDamageFromEnemy;
    public bool gameover;

    //UI game objects
    public GameObject coinsCounter;
    public GameObject villageHpCounter;
    public GameObject hpCounter;
    public GameObject mainCamera;
    public static PlayerStats Instance { get; set; } // To collect and send data from this script

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameover = false;
        godmode = false;
        maxhp = hp;
        coinsCounter.GetComponent<UICounter>().TakeCounterData(coins);
        villageHpCounter.GetComponent<UICounter>().TakeCounterData(villageHp);
    }

    public void PlayerDamaged(float damageDeal)
    {
        if (godmode == false) hp -= damageDeal;
        float ratio = hp / maxhp;
        hpCounter.GetComponent<UIBarLogic>().BarUpdate(ratio);
    }
    public void PlayerHealed(float healDeal)
    {
        hp += healDeal;
        float ratio = hp / maxhp;
        hpCounter.GetComponent<UIBarLogic>().BarUpdate(ratio);
    }
    public void VillageDamaged(float damageDeal)
    {
        villageHp -= damageDeal;
        villageHpCounter.GetComponent<UICounter>().TakeCounterData(villageHp);
    }
    public void VillageRepaired(float repairAmount)
    {
        villageHp += repairAmount;
        villageHpCounter.GetComponent<UICounter>().TakeCounterData(villageHp);
    }
    public void CoinPlus(float amount)
    {
        coins += amount;
        coinsCounter.GetComponent<UICounter>().TakeCounterData(coins);
    }
    public void CoinMinus(float amount)
    {
        coins -= amount;
        coinsCounter.GetComponent<UICounter>().TakeCounterData(coins);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Flying_Enemy"))
        {
            PlayerDamaged(takkenDamageFromEnemy);
        }
        else return;
    }
    void Update()
    {
        if (villageHp <= 0 && gameover == false)
        {
            mainCamera.GetComponent<Pause>().GameOver();
        }
        if (hp <= 0 && gameover == false)
        {
            mainCamera.GetComponent<Pause>().GameOver();
        }
    }
}
