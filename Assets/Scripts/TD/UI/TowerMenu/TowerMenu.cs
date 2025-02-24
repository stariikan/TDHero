using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public string tileName;
    public GameObject tileObject;
    public bool windowIsActive; // Check that window already open or not

    // Buttons game objects
    public GameObject commonTowerButton;
    public GameObject splashTowerButton;
    public GameObject antiAirTowerButton;
    public GameObject FreezingTowerButton;
    public GameObject removeButton;
    public GameObject closeButton;

    //Tower game objects ant text object for price
    public GameObject commonTower;
    public GameObject splashTower;
    public GameObject antiAirTower;
    public GameObject freezingTower;

    public Text commonTowerPriceText;
    public Text splashTowerPriceText;
    public Text antiAirTowerPriceText;
    public Text freezingTowerPriceText;

    // Text game object
    public Text tileNameText;
    public string towerMenuText;
    // Start is called before the first frame update
    void Start()
    {
        windowIsActive = true;
        commonTowerPriceText.text = "" + commonTower.GetComponent<TowerState>().towerPrice;
        splashTowerPriceText.text = "" + splashTower.GetComponent<TowerState>().towerPrice;
        antiAirTowerPriceText.text = "" + antiAirTower.GetComponent<TowerState>().towerPrice;
        freezingTowerPriceText.text = "" + freezingTower.GetComponent<TowerState>().towerPrice;
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
    public void CloseWindow()
    {
        if (windowIsActive)
        {
            windowIsActive = false;
            tileNameText.text = towerMenuText;
            this.gameObject.SetActive(false);
        }
    }
    public void ShowTileName()
    {
        tileNameText.text = tileName;
    }
    public void FindTile()
    {
        GameObject targetObject = GameObject.Find(tileName);
        if (targetObject != null)
        {
            tileObject = targetObject;
            ShowTileName();
            commonTowerButton.GetComponent<TowerButton>().ReciveTileName(tileName);
            splashTowerButton.GetComponent<TowerButton>().ReciveTileName(tileName);
            antiAirTowerButton.GetComponent<TowerButton>().ReciveTileName(tileName);
            FreezingTowerButton.GetComponent<TowerButton>().ReciveTileName(tileName);
            removeButton.GetComponent<TowerButton>().ReciveTileName(tileName);
            closeButton.GetComponent<TowerButton>().ReciveTileName(tileName);
        }
        else
        {
            Debug.LogWarning("Object not found!");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}