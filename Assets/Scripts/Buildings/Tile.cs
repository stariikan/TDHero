using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    // When tile selected true
    public bool tileState;

    // Name of the builded tower 
    public string towerName;
    public string lightName;

    // When tile selected UI appears
    public GameObject tileTowerUI;
    public GameObject tileTowerUpgradeUI;
    public GameObject selectedTileLight;    // Light for tile when selected
    public bool tileUiIsActive;
    public bool towerUpgradeUiActive;

    // Tower state on tile
    public bool commonTowerState;
    public bool splashTowerState;
    public bool antiAirTowerState;
    public bool freezingTowerState;
    public int towerLvl;
    private float towerPrice; 

    //Tower game objects
    public GameObject[] commonTower;
    public GameObject[] splashTower;
    public GameObject[] antiAirTower;
    public GameObject[] freezingTower;

    //Player game object
    public GameObject playerObj;
    public float playerCoins;

    // Main camera obj
    public GameObject mainCamera;

    //Text message from tile
    public Text tileMessage;

    // Start is called before the first frame update
    void Start()
    {
        tileState = false;
        commonTowerState = false;
        splashTowerState = false;
        antiAirTowerState = false;
        freezingTowerState = false;
        towerLvl = 0;
    }
    public void BuildCommonTower()
    {
        towerPrice = commonTower[towerLvl].GetComponent<TowerState>().towerPrice;
        playerCoins = playerObj.GetComponent<PlayerStats>().coins;
        if (commonTowerState == false && splashTowerState == false && antiAirTowerState == false && freezingTowerState == false && playerCoins >= towerPrice)
        {
            commonTowerState = true;

            splashTowerState = false;
            antiAirTowerState = false;
            freezingTowerState = false;

            GameObject tower = Instantiate(commonTower[towerLvl], this.gameObject.transform.position + new Vector3(5, 0f, 5), this.gameObject.transform.rotation);
            tower.name = "Common Tower " + this.gameObject.name;
            towerName = tower.name;
            tower.SetActive(true);
            playerObj.GetComponent<PlayerStats>().CoinMinus(towerPrice);
            CloseTile();
        }
        else tileMessage.text = "Not enough money. " + "Tower Price = " + towerPrice;
    }
    public void BuildSplashTower()
    {
        towerPrice = splashTower[towerLvl].GetComponent<TowerState>().towerPrice;
        playerCoins = playerObj.GetComponent<PlayerStats>().coins;
        if (commonTowerState == false && splashTowerState == false && antiAirTowerState == false && freezingTowerState == false && playerCoins >= towerPrice)
        {
            splashTowerState = true;

            commonTowerState = false;
            antiAirTowerState = false;
            freezingTowerState = false;

            GameObject tower = Instantiate(splashTower[towerLvl], this.gameObject.transform.position + new Vector3(5, 0f, 5), this.gameObject.transform.rotation);
            tower.name = "Splash Tower " + this.gameObject.name;
            towerName = tower.name;
            tower.SetActive(true);
            playerObj.GetComponent<PlayerStats>().CoinMinus(towerPrice);
            CloseTile();
        }
        else tileMessage.text = "Tower Price: " + towerPrice + ". Not enough money.";
    }
    public void BuildAntiAirTower()
    {
        towerPrice = antiAirTower[towerLvl].GetComponent<TowerState>().towerPrice;
        playerCoins = playerObj.GetComponent<PlayerStats>().coins;
        if (commonTowerState == false && splashTowerState == false && antiAirTowerState == false && freezingTowerState == false && playerCoins >= towerPrice)
        {
            antiAirTowerState = true;

            splashTowerState = false;
            commonTowerState = false;
            freezingTowerState = false;

            GameObject tower = Instantiate(antiAirTower[towerLvl], this.gameObject.transform.position + new Vector3(5, 0f, 5), this.gameObject.transform.rotation);
            tower.name = "Anti Air Tower " + this.gameObject.name;
            towerName = tower.name;
            tower.SetActive(true);
            playerObj.GetComponent<PlayerStats>().CoinMinus(towerPrice);
            CloseTile();
        }
        else tileMessage.text = "Tower Price: " + towerPrice + ". Not enough money.";
    }
    public void BuildFreezingTower()
    {
        towerPrice = freezingTower[towerLvl].GetComponent<TowerState>().towerPrice;
        playerCoins = playerObj.GetComponent<PlayerStats>().coins;
        if (commonTowerState == false && splashTowerState == false && antiAirTowerState == false && freezingTowerState == false && playerCoins >= towerPrice)
        {
            freezingTowerState = true;

            antiAirTowerState = false;
            splashTowerState = false;
            commonTowerState = false;

            GameObject tower = Instantiate(freezingTower[towerLvl], this.gameObject.transform.position + new Vector3(5, 0f, 5), this.gameObject.transform.rotation);
            tower.name = "Freezing Tower " + this.gameObject.name;
            towerName = tower.name;
            tower.SetActive(true);
            playerObj.GetComponent<PlayerStats>().CoinMinus(towerPrice);
            CloseTile();
        }
        else tileMessage.text = "Tower Price: " + towerPrice + ". Not enough money.";
    }
    public void SellTower()
    {
        towerPrice = commonTower[towerLvl].GetComponent<TowerState>().towerPrice;
        playerObj.GetComponent<PlayerStats>().CoinPlus(towerPrice/2);
        Debug.Log("Tower sold");
        RemoveAllTowers();
    }
    public void UpgradeTower()
    {

        if (commonTowerState)
        {
            towerPrice = commonTower[towerLvl + 1].GetComponent<TowerState>().towerPrice;
            playerCoins = playerObj.GetComponent<PlayerStats>().coins;
            if (towerLvl < 6 && playerCoins >= towerPrice)
            {
                towerLvl += 1;
                GameObject targetObject = GameObject.Find(towerName);
                if (targetObject != null)
                {
                    targetObject.GetComponent<TowerState>().RemoveTower();
                }
                GameObject tower = Instantiate(commonTower[towerLvl], this.gameObject.transform.position + new Vector3(5, 0f, 5), this.gameObject.transform.rotation);
                tower.name = "Common Tower " + this.gameObject.name;
                towerName = tower.name;
                tower.SetActive(true);
                playerObj.GetComponent<PlayerStats>().CoinMinus(towerPrice);
                CloseTile();
                Debug.Log("Tower Upgraded");
            }
            else tileMessage.text = "Tower Price: " + towerPrice + ". Not enough money.";
        }
        if (splashTowerState)
        {
            towerPrice = splashTower[towerLvl + 1].GetComponent<TowerState>().towerPrice;
            playerCoins = playerObj.GetComponent<PlayerStats>().coins;
            if (towerLvl < 6 && playerCoins >= towerPrice)
            {
                towerLvl += 1;
                GameObject targetObject = GameObject.Find(towerName);
                if (targetObject != null)
                {
                    targetObject.GetComponent<TowerState>().RemoveTower();
                }
                GameObject tower = Instantiate(splashTower[towerLvl], this.gameObject.transform.position + new Vector3(5, 0f, 5), this.gameObject.transform.rotation);
                tower.name = "Splash Tower " + this.gameObject.name;
                towerName = tower.name;
                tower.SetActive(true);
                playerObj.GetComponent<PlayerStats>().CoinMinus(towerPrice);
                CloseTile();
                Debug.Log("Tower Upgraded");
            }
            else tileMessage.text = "Tower Price: " + (towerLvl + 1) + ". Not enough money.";
        }
        if (antiAirTowerState)
        {
            towerPrice = antiAirTower[towerLvl + 1].GetComponent<TowerState>().towerPrice;
            playerCoins = playerObj.GetComponent<PlayerStats>().coins;
            if (towerLvl < 6 && playerCoins >= towerPrice)
            {
                towerLvl += 1;
                GameObject targetObject = GameObject.Find(towerName);
                if (targetObject != null)
                {
                    targetObject.GetComponent<TowerState>().RemoveTower();
                }
                GameObject tower = Instantiate(antiAirTower[towerLvl], this.gameObject.transform.position + new Vector3(5, 0f, 5), this.gameObject.transform.rotation);
                tower.name = "Anti Air Tower " + this.gameObject.name;
                towerName = tower.name;
                tower.SetActive(true);
                playerObj.GetComponent<PlayerStats>().CoinMinus(towerPrice);
                CloseTile();
                Debug.Log("Tower Upgraded");
            }
            else tileMessage.text = "Tower Price: " + (towerLvl + 1) + ". Not enough money.";
        }
        if (freezingTowerState)
        {
            towerPrice = freezingTower[towerLvl + 1].GetComponent<TowerState>().towerPrice;
            playerCoins = playerObj.GetComponent<PlayerStats>().coins;
            if (towerLvl < 6 && playerCoins >= towerPrice)
            {
                towerLvl += 1;
                GameObject targetObject = GameObject.Find(towerName);
                if (targetObject != null)
                {
                    targetObject.GetComponent<TowerState>().RemoveTower();
                }
                GameObject tower = Instantiate(freezingTower[towerLvl], this.gameObject.transform.position + new Vector3(5, 0f, 5), this.gameObject.transform.rotation);
                tower.name = "Freezing Tower " + this.gameObject.name;
                towerName = tower.name;
                tower.SetActive(true);
                playerObj.GetComponent<PlayerStats>().CoinMinus(towerPrice);
                CloseTile();
                Debug.Log("Tower Upgraded");
            }
            else tileMessage.text = "Tower Price: " + towerPrice + ". Not enough money.";
        }
    }
    public void RemoveAllTowers()
    {
        commonTowerState = false;
        splashTowerState = false;
        antiAirTowerState = false;
        freezingTowerState = false;
        GameObject targetObject = GameObject.Find(towerName);
        if (targetObject != null)
        {
            targetObject.GetComponent<TowerState>().RemoveTower();
        }
        else
        {
            Debug.LogWarning("Object not found!");
        }
        towerLvl = 0;
        towerName = "";
        CloseTile();
    }
    public void SelectTile()
    {
        bool isPaused = mainCamera.GetComponent<Pause>().isPaused;
        tileUiIsActive = tileTowerUI.GetComponent<TowerMenu>().windowIsActive;
        towerUpgradeUiActive = tileTowerUpgradeUI.GetComponent<TowerUpgradeMenu>().windowIsActive;

        if (tileState || towerUpgradeUiActive || tileUiIsActive || isPaused) return; // Exit if paused or UI is active

        if (!commonTowerState && !splashTowerState && !antiAirTowerState && !freezingTowerState)
        {
            tileState = true;
            tileTowerUI.GetComponent<TowerMenu>().OpenWindow(this.gameObject.name);
        }
        if (commonTowerState || splashTowerState || antiAirTowerState || freezingTowerState)
        {
            tileState = true;
            tileTowerUpgradeUI.GetComponent<TowerUpgradeMenu>().OpenWindow(this.gameObject.name);
            tileTowerUpgradeUI.GetComponent<TowerUpgradeMenu>().ReciveTowerName(towerName);
        }
        selectedTileLight.SetActive(true);
        selectedTileLight.transform.position = this.transform.position + new Vector3(5, 5.3f, 5);
    }
    public void CloseTile()
    {
        tileState = false;
        tileTowerUI.GetComponent<TowerMenu>().CloseWindow();
        tileTowerUpgradeUI.GetComponent<TowerUpgradeMenu>().CloseWindow();
        selectedTileLight.SetActive(false);
    }
}
