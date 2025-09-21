using UnityEngine;
using UnityEngine.UI;

public class BuildSystem : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject playerObj;

    public bool buildMode;
    public bool towerPlaced;

    public int towerCounter;
    // Towers
    public GameObject commonTower;
    public GameObject iceTower;
    public GameObject airDefenceTower;
    public GameObject aoeTower;

    private GameObject newTower;

    public GameObject infoText;

    public Vector3 offset;


    private void Start()
    {
        buildMode = false;
        towerCounter = 0;
    }
    void Update()
    {
        if (buildMode == true && towerPlaced == true && Input.GetMouseButtonDown(1)) Close();
        if (towerPlaced == true && buildMode == true && newTower != null) TowerFollowTheCursor();
        if (towerPlaced == true && buildMode == true && newTower != null &&  Input.GetMouseButtonDown(0)) PlaceNewTower();
    }
    private void Close()
    {
        buildMode = false;
        Destroy(newTower);
        newTower = null;
    }
    public void CreateCommonTower()
    {
        buildMode = true;
        float needCoins = commonTower.GetComponent<TowerState>().towerPrice;
        float availableCoins = playerObj.GetComponent<PlayerStats>().coins;
        if (availableCoins >= needCoins)
        {
            GameObject tower = Instantiate(commonTower, Input.mousePosition, commonTower.transform.rotation);
            towerCounter += 1;
            tower.name = "fire tower #" + towerCounter;
            tower.SetActive(true);
            newTower = tower;
            newTower.GetComponent<TowerState>().BuildTower();
            towerPlaced = true;
            offset = new Vector3(0, 0, 0);
        }
        else if (infoText != null) infoText.GetComponent<Text>().text = "Need more souls: " + (needCoins - availableCoins);
    }
    public void CreateIceTower()
    {
        buildMode = true;
        float needCoins = iceTower.GetComponent<TowerState>().towerPrice;
        float availableCoins = playerObj.GetComponent<PlayerStats>().coins;
        if (availableCoins >= needCoins)
        {
            GameObject tower = Instantiate(iceTower, Input.mousePosition, iceTower.transform.rotation);
            tower.name = "ice tower #" + towerCounter;
            tower.SetActive(true);
            newTower = tower;
            newTower.GetComponent<TowerState>().BuildTower();
            towerPlaced = true;
            offset = new Vector3(0, 4, 0);
        }
        else if (infoText != null) infoText.GetComponent<Text>().text = "need more souls: " + (needCoins - availableCoins);
    }
    public void CreateAirDefenceTower()
    {
        buildMode = true;
        float needCoins = airDefenceTower.GetComponent<TowerState>().towerPrice;
        float availableCoins = playerObj.GetComponent<PlayerStats>().coins;
        if (availableCoins >= needCoins)
        {
            GameObject tower = Instantiate(airDefenceTower, Input.mousePosition, airDefenceTower.transform.rotation);
            tower.name = "air defence #" + towerCounter;
            tower.SetActive(true);
            newTower = tower;
            newTower.GetComponent<TowerState>().BuildTower();
            towerPlaced = true;
            offset = new Vector3(0, 5, 0);
        }
        else if (infoText != null) infoText.GetComponent<Text>().text = "need more souls: " + (needCoins - availableCoins);
    }
    public void CreateAoeTower()
    {
        buildMode = true;
        float needCoins = aoeTower.GetComponent<TowerState>().towerPrice;
        float availableCoins = playerObj.GetComponent<PlayerStats>().coins;
        if (availableCoins >= needCoins)
        {
            GameObject tower = Instantiate(aoeTower, Input.mousePosition, airDefenceTower.transform.rotation);
            tower.name = "aoe tower #" + towerCounter;
            tower.SetActive(true);
            newTower = tower;
            newTower.GetComponent<TowerState>().BuildTower();
            towerPlaced = true;
            offset = new Vector3(0, 5, 0);
        }
        else if (infoText != null) infoText.GetComponent<Text>().text = "need more souls: " + (needCoins - availableCoins);
    }
    private void TowerFollowTheCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
        foreach (RaycastHit hit in hits)
        {
            //Debug.Log("Hit: " + hit.collider.gameObject.name + " on layer " + LayerMask.LayerToName(hit.collider.gameObject.layer));

            // Check if the hit object is a tile
            if (hit.collider.gameObject.CompareTag("Tile"))
            {
                newTower.transform.position = hit.collider.gameObject.transform.position + offset;
                break; // Stop looking once we find a tile
            }
            
        }
    }
    public void PlaceNewTower()
    {
        bool canPlace = newTower.GetComponent<TowerState>().canPlaceTower;
        if (canPlace)
        {
            buildMode = false;
            towerPlaced = false;
            towerCounter += 1;
            newTower.GetComponent<TowerState>().PlaceTower();
            offset = new Vector3(0, 0, 0);
        }
    }
}
