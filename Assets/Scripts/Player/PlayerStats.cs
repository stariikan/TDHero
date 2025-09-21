using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp; //player HP
    public float maxhp;
    public float villageHp; //HP of the village
    public float coins; //money for upgrade of abilities and towers
    public float exp;
    public int skillPoint;
    public int playerLevel;
    public bool godmode;
    public bool isAlive;
    public bool playerDemon;
    public float takkenDamageFromEnemy;
    public bool gameover;

    //UI game objects
    public GameObject coinsCounter;
    public GameObject villageHpCounter;
    public GameObject playerLevelNumberText;
    public GameObject expNumberText;
    public GameObject hpCounter;
    public GameObject expCounter;
    public GameObject mainCamera;
    public GameObject takenDamageUi;    // Reference for DamageNumbers Icon
    public GameObject LevelUpButton;    // Reference for DamageNumbers Icon
    public static PlayerStats Instance { get; set; } // To collect and send data from this script

    // Start is called before the first frame update
    void Start()
    {
        exp = 0;
        playerLevel = 0;
        Instance = this;
        gameover = false;
        godmode = false;
        isAlive = true;
        maxhp = hp;
        coinsCounter.GetComponent<UICounter>().TakeCounterData(coins);
        villageHpCounter.GetComponent<UICounter>().TakeCounterData(villageHp);
        playerLevelNumberText.GetComponent<UICounter>().TakeCounterData(playerLevel);
        expNumberText.GetComponent<UICounter>().TakeCounterData(exp);
    }

    public void PlayerDamaged(float damageDeal)
    {
        float normalizedDamage = Mathf.Round(damageDeal * 10f) / 10f;

        if (!godmode)
            hp -= damageDeal;

        float ratio = hp / maxhp;
        hpCounter.GetComponent<UIBarLogic>().BarUpdate(ratio);

        
        GameObject damageUI = Instantiate(takenDamageUi, transform.position, Quaternion.identity);
        damageUI.transform.SetParent(takenDamageUi.transform.parent, false);

        var damageText = damageUI.GetComponent<TextMeshProUGUI>();
        damageText.text = $"   {normalizedDamage}";
        damageText.color = Color.red;

        damageUI.SetActive(true);
    }
    public void PlayerHealed(float healDeal)
    {
        float normalizedHeal = Mathf.Round(healDeal * 10f) / 10f;
        if (hp < maxhp) hp += healDeal;
        float ratio = hp / maxhp;
        hpCounter.GetComponent<UIBarLogic>().BarUpdate(ratio);
        GameObject healUI = Instantiate(takenDamageUi, transform.position, Quaternion.identity);
        healUI.transform.SetParent(takenDamageUi.transform.parent, false);
        healUI.GetComponent<TextMeshProUGUI>().text = $"       {normalizedHeal}";
        healUI.GetComponent<TextMeshProUGUI>().color = Color.green;
        healUI.SetActive(true);
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
        if (coinsCounter != null) coinsCounter.GetComponent<UICounter>().TakeCounterData(coins);
    }
    public void CoinPlusDebug()
    {
        coins += 50;
        if (coinsCounter != null) coinsCounter.GetComponent<UICounter>().TakeCounterData(coins);
    }
    public void CoinMinus(float amount)
    {
        coins -= amount;
        if (coinsCounter != null) coinsCounter.GetComponent<UICounter>().TakeCounterData(coins);
    }
    public void SpentPoint()
    {
        skillPoint -= 1;
    }
    public void GetExp(float amount)
    {
        exp += amount;
        int needExpForNextLevel = playerLevel * 1000;
        if (needExpForNextLevel < 1000) needExpForNextLevel = 1000;
        float ratio = exp / needExpForNextLevel;
        if (expCounter != null) expCounter.GetComponent<UIBarLogic>().BarUpdate(ratio);
        if (expNumberText != null) expNumberText.GetComponent<UICounter>().TakeCounterData(exp);
        if (exp >= needExpForNextLevel) 
        {
            playerLevel += 1;
            exp = 0;
            skillPoint += 1;
            needExpForNextLevel = playerLevel * 1000;
            ratio = exp / needExpForNextLevel;
            this.gameObject.GetComponent<TDCombat>().ImproveAttack();
            this.gameObject.GetComponent<TDCombat>().ImproveFreezePower();
            if (playerLevelNumberText != null) playerLevelNumberText.GetComponent<UICounter>().TakeCounterData(playerLevel);
            if (expNumberText != null) expNumberText.GetComponent<UICounter>().TakeCounterData(exp);
            if (expCounter != null) expCounter.GetComponent<UIBarLogic>().BarUpdate(ratio);
        } 
    }
    public void GetExpDebug()
    {
        exp += 100;
        int needExpForNextLevel = playerLevel * 1000;
        if (needExpForNextLevel < 1000) needExpForNextLevel = 1000;
        float ratio = exp / needExpForNextLevel;
        if (expCounter != null) expCounter.GetComponent<UIBarLogic>().BarUpdate(ratio);
        if (expNumberText != null) expNumberText.GetComponent<UICounter>().TakeCounterData(exp);
        if (exp >= needExpForNextLevel)
        {
            playerLevel += 1;
            exp = 0;
            skillPoint += 1;
            needExpForNextLevel = playerLevel * 1000;
            ratio = exp / needExpForNextLevel;
            if (playerLevelNumberText != null) playerLevelNumberText.GetComponent<UICounter>().TakeCounterData(playerLevel);
            if (expNumberText != null) expNumberText.GetComponent<UICounter>().TakeCounterData(exp);
            if (expCounter != null) expCounter.GetComponent<UIBarLogic>().BarUpdate(ratio);
        }
    }
    public void GodModeOn()
    {
        godmode = true;
    }
    public void GodModeOff()
    {
        godmode = false;
    }
    void Update()
    {
        if (villageHp <= 0 && gameover == false)
        {
            mainCamera.GetComponent<Pause>().GameOver();
        }
        if (hp <= 0 && gameover == false)
        {
            isAlive = false;
            this.gameObject.GetComponent<TDPlayerMovement>().isDead();
            mainCamera.GetComponent<Pause>().GameOver();
        }
        if (skillPoint > 0)
        {
            if (LevelUpButton != null) LevelUpButton.SetActive(true);
        }
        else if (LevelUpButton != null) LevelUpButton.SetActive(false);
    }
}
