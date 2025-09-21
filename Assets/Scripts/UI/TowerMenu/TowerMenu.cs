using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public bool windowIsActive; // Check that window already open or not

    //Tower game objects ant text object for price
    public GameObject towerObj;

    public float maxHP;
    public float e_currentHP;
    public float damage;
    public float attackDelayTime;
    public float freezingPower;
    public float bombRadius;
    public float bombDamage;
    public float upgradeTowerPrice;

    public Text towerName;
    public Text maxHPText;
    public Text e_currentHPText;
    public Text damageText;
    public Text attackDelayTimeText;
    public Text freezingPowerText;
    public Text bombRadiusText;
    public Text bombDamageText;
    public Text upgradeTowerPriceText;

    public void OpenWindow(string name)
    {
        if (!windowIsActive)
        {
            windowIsActive = true;
            towerName.text = name;
            FindTower(name);
            GatherTowerStats();
            UseTowerStats();
            this.gameObject.SetActive(true);
        }
    }
    public void UpgradeTower()
    {
        towerObj.GetComponent<TowerState>().UpgradeTower();
        GatherTowerStats();
        UseTowerStats();
    }
    public void SellTower()
    {
        towerObj.GetComponent<TowerState>().SellTower();
        CloseWindow();
    }
    public void CloseWindow()
    {
        if (windowIsActive)
        {
            windowIsActive = false;
            towerObj = null;
            this.gameObject.SetActive(false);
        }
    }
    public void FindTower(string name)
    {
        GameObject targetObject = GameObject.Find(name);
        if (targetObject != null)
        {
            towerObj = targetObject;
        }
        else
        {
            Debug.LogWarning("Object not found!");
        }
    }
    public void GatherTowerStats()
    {
        upgradeTowerPrice = towerObj.GetComponent<TowerState>().upgradeTowerPrice;
        maxHP = towerObj.GetComponent<TowerState>().e_maxHP;
        e_currentHP = towerObj.GetComponent<TowerState>().e_currentHP;
        damage = towerObj.GetComponent<TowerState>().damage;
        attackDelayTime = towerObj.GetComponent<TowerState>().attackDelayTime;
        freezingPower = towerObj.GetComponent<TowerState>().freezingPower;
        bombRadius = towerObj.GetComponent<TowerState>().bombRadius;
        bombDamage = towerObj.GetComponent<TowerState>().bombDamage;
    }
    public void UseTowerStats()
    {
        upgradeTowerPriceText.text = "" + upgradeTowerPrice;
        maxHPText.text = "" + maxHP;
        e_currentHPText.text = "" + e_currentHP;
        damageText.text = "" + damage;
        attackDelayTimeText.text = "" + attackDelayTime;
        freezingPowerText.text = "" + freezingPower;
        bombRadiusText.text = "" + bombRadius;
        bombDamageText.text = "" + bombDamage;
    }
}