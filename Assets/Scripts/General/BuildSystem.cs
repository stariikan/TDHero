using UnityEditor.Experimental.GraphView;
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
    public GameObject antiFlyTower;
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
        if (towerPlaced == true && buildMode == true) TowerFollowTheCursor();
        if (towerPlaced == true && buildMode == true && Input.GetMouseButtonDown(0)) PlaceNewTower();
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
        if (availableCoins > needCoins)
        {
            GameObject tower = Instantiate(commonTower, Input.mousePosition, commonTower.transform.rotation);
            towerCounter += 1;
            tower.name = "Common Tower " + towerCounter;
            tower.SetActive(true);
            newTower = tower;
            newTower.GetComponent<TowerState>().BuildTower();
            towerPlaced = true;
        }
        else if (infoText != null) infoText.GetComponent<Text>().text = "Need more souls: " + (needCoins - availableCoins);
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
            newTower.GetComponent<TowerState>().PlaceTower();
        }
    }
}
