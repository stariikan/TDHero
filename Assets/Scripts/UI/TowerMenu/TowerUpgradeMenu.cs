using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeMenu : MonoBehaviour
{
    public string tileName;
    public GameObject tileObject;
    public GameObject towerObject;
    public bool windowIsActive; // Check that window already open or not

    // Buttons game objects
    public GameObject upgradeButton;
    public GameObject sellTower;
    public GameObject closeButton;

    // Text game object
    public Text towerName;
    public string towerNameText;

    public Text towerDamage;
    public float towerDamageStat;

    public Text towerRange;
    public float towerRangeStat;

    public Text towerAttackSpeed;
    public float towerAttackSpeedStat;

    public Text towerLvl;
    public int towerLvlStat;

    // Prices on the buttons
    public Text towerUpgradePriceText;
    public Text towerSellPriceText;
    private float towerUpgradePrice;

    //Tower game objects
    public GameObject[] commonTower;
    public GameObject[] splashTower;
    public GameObject[] antiAirTower;
    public GameObject[] freezingTower;
    // Start is called before the first frame update
    void Start()
    {
        windowIsActive = true;
    }
    public void OpenWindow(string name)
    {
        if (!windowIsActive)
        {
            windowIsActive = true;
            tileName = name;
            this.gameObject.SetActive(true);
            FindTile();
        }
    }
    public void ReciveTowerName(string name)
    {
        towerNameText = name;
        FindTower();
    }
    public void CloseWindow()
    {
        if (windowIsActive)
        {
            windowIsActive = false;
            towerName.text = "";
            this.gameObject.SetActive(false);
        }
    }
    public void ShowTowerStats()
    {
        bool commonTowerState = tileObject.GetComponent<Tile>().commonTowerState;
        bool splashTowerState = tileObject.GetComponent<Tile>().splashTowerState;
        bool antiAirTowerState = tileObject.GetComponent<Tile>().antiAirTowerState;
        bool freezingTowerState = tileObject.GetComponent<Tile>().freezingTowerState;

        towerName.text = towerNameText;

        if (splashTowerState == false) 
        {
            towerDamageStat = towerObject.GetComponent<TowerState>().damage;
            towerDamage.text = "Damage: " + towerDamageStat;
        }
        else
        {
            towerDamageStat = towerObject.GetComponent<TowerState>().bombDamage;
            towerDamage.text = "Damage: " + towerDamageStat;
        }

        towerRangeStat = towerObject.GetComponent<TowerState>().attackRange;
        towerRange.text = "Attack Range: " + towerRangeStat;

        towerAttackSpeedStat = towerObject.GetComponent<TowerState>().attackDelayTime;
        towerAttackSpeedStat = 1 / towerAttackSpeedStat;
        towerAttackSpeed.text = "Attack/sec: " + towerAttackSpeedStat;

        towerLvlStat = tileObject.GetComponent<Tile>().towerLvl + 1;
        towerLvl.text = "Tower LVL: " + towerLvlStat;
        
        if (commonTowerState == true) 
        {
            if (towerLvlStat == 1) towerUpgradePrice = commonTower[1].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 2) towerUpgradePrice = commonTower[2].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 3) towerUpgradePrice = commonTower[3].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 4) towerUpgradePrice = commonTower[4].GetComponent<TowerState>().towerPrice;
        }
        if (splashTowerState == true)
        {
            if (towerLvlStat == 1) towerUpgradePrice = splashTower[1].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 2) towerUpgradePrice = splashTower[2].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 3) towerUpgradePrice = splashTower[3].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 4) towerUpgradePrice = splashTower[4].GetComponent<TowerState>().towerPrice;
        }
        if (antiAirTowerState == true)
        {
            if (towerLvlStat == 1) towerUpgradePrice = antiAirTower[1].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 2) towerUpgradePrice = antiAirTower[2].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 3) towerUpgradePrice = antiAirTower[3].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 4) towerUpgradePrice = antiAirTower[4].GetComponent<TowerState>().towerPrice;
        }
        if (freezingTowerState == true)
        {
            if (towerLvlStat == 1) towerUpgradePrice = freezingTower[1].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 2) towerUpgradePrice = freezingTower[2].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 3) towerUpgradePrice = freezingTower[3].GetComponent<TowerState>().towerPrice;
            if (towerLvlStat == 4) towerUpgradePrice = freezingTower[4].GetComponent<TowerState>().towerPrice;
        }
        towerUpgradePriceText.text = "" + towerUpgradePrice;
        towerSellPriceText.text = "" + towerUpgradePrice / 2;

        if (towerLvlStat <= 4) upgradeButton.SetActive(true);
        if (towerLvlStat >= 5) upgradeButton.SetActive(false);
    }
    public void FindTile()
    {
        GameObject targetObject = GameObject.Find(tileName);
        if (targetObject != null)
        {
            tileObject = targetObject;
            closeButton.GetComponent<TowerButton>().ReciveTileName(tileName);
            sellTower.GetComponent<TowerButton>().ReciveTileName(tileName);
            upgradeButton.GetComponent<TowerButton>().ReciveTileName(tileName);
        }
        else
        {
            Debug.LogWarning("Object not found!");
        }
    }
    public void FindTower()
    {
        GameObject targetObject = GameObject.Find(towerNameText);
        if (targetObject != null)
        {
            towerObject = targetObject;  
        }
        else
        {
            Debug.LogWarning("Object not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowTowerStats();
    }
}