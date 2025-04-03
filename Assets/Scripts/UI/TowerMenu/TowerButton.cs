using UnityEngine;

public class TowerButton : MonoBehaviour
{
    // Defines button func (1,2,3,4,5)
    public int whichButtonIsIt;

    // Name and game object of the tile that summon UI
    public string tileName;
    public GameObject tileObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ButtonCheck()
    {
        if (whichButtonIsIt == 0) 
        {
            tileObject.GetComponent<Tile>().BuildCommonTower();
        }
        if (whichButtonIsIt == 1)
        {
            tileObject.GetComponent<Tile>().BuildSplashTower();
        }
        if (whichButtonIsIt == 2)
        {
            tileObject.GetComponent<Tile>().BuildAntiAirTower();
        }
        if (whichButtonIsIt == 3)
        {
            tileObject.GetComponent<Tile>().BuildFreezingTower();
        }
        if (whichButtonIsIt == 4)
        {
            tileObject.GetComponent<Tile>().CloseTile();
        }
        if (whichButtonIsIt == 5)
        {
            tileObject.GetComponent<Tile>().RemoveAllTowers();
        }
        if (whichButtonIsIt == 6)
        {
            tileObject.GetComponent<Tile>().SellTower();
        }
        if (whichButtonIsIt == 7)
        {
            tileObject.GetComponent<Tile>().UpgradeTower();
        }
    }
    public void ReciveTileName(string name)
    {
        tileName = name;
        GameObject targetObject = GameObject.Find(tileName);
        if (targetObject != null)
        {
            tileObject = targetObject;
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