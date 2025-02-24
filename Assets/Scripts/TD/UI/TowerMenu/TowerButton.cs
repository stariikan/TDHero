<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
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
            tileObject.GetComponent<TileSelect>().BuildCommonTower();
        }
        if (whichButtonIsIt == 1)
        {
            tileObject.GetComponent<TileSelect>().BuildSplashTower();
        }
        if (whichButtonIsIt == 2)
        {
            tileObject.GetComponent<TileSelect>().BuildAntiAirTower();
        }
        if (whichButtonIsIt == 3)
        {
            tileObject.GetComponent<TileSelect>().BuildFreezingTower();
        }
        if (whichButtonIsIt == 4)
        {
            tileObject.GetComponent<TileSelect>().TileUnselected();
        }
        if (whichButtonIsIt == 5)
        {
            tileObject.GetComponent<TileSelect>().RemoveAllTowers();
        }
        if (whichButtonIsIt == 6)
        {
            tileObject.GetComponent<TileSelect>().SellTower();
        }
        if (whichButtonIsIt == 7)
        {
            tileObject.GetComponent<TileSelect>().UpgradeTower();
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
=======
using System.Collections;
using System.Collections.Generic;
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
            tileObject.GetComponent<TileSelect>().BuildCommonTower();
        }
        if (whichButtonIsIt == 1)
        {
            tileObject.GetComponent<TileSelect>().BuildSplashTower();
        }
        if (whichButtonIsIt == 2)
        {
            tileObject.GetComponent<TileSelect>().BuildAntiAirTower();
        }
        if (whichButtonIsIt == 3)
        {
            tileObject.GetComponent<TileSelect>().BuildFreezingTower();
        }
        if (whichButtonIsIt == 4)
        {
            tileObject.GetComponent<TileSelect>().TileUnselected();
        }
        if (whichButtonIsIt == 5)
        {
            tileObject.GetComponent<TileSelect>().RemoveAllTowers();
        }
        if (whichButtonIsIt == 6)
        {
            tileObject.GetComponent<TileSelect>().SellTower();
        }
        if (whichButtonIsIt == 7)
        {
            tileObject.GetComponent<TileSelect>().UpgradeTower();
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
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
