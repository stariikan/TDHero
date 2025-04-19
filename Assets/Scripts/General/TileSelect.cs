using UnityEngine;
using UnityEngine.UI;

public class TileSelect : MonoBehaviour
{
    // Main camera obj
    public GameObject mainCamera;
    private GameObject selectedTower;
    public GameObject towerMenu;
    private void Start()
    {
        selectedTower = null;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) Close();
        if (Input.GetMouseButtonDown(0)) Select();
    }
    private void Close()
    {
        if (selectedTower != null) selectedTower.GetComponent<TowerState>().UnselectTower();
        selectedTower = null;
    }
    private void Select()
    {
        bool towerMenuUi = towerMenu.GetComponent<TowerMenu>().windowIsActive;
        bool isPausedUi = mainCamera.GetComponent<Pause>().isPaused;
        bool buildSystemIsActive = mainCamera.GetComponent<BuildSystem>().buildMode;
        if (towerMenuUi == false && isPausedUi == false && buildSystemIsActive == false) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

            foreach (RaycastHit hit in hits)
            {
                Collider col = hit.collider;

                // Check if the hit object is a tower AND it has a BoxCollider (not a trigger)
                if (col.gameObject.CompareTag("Tower") && col is BoxCollider && !col.isTrigger)
                {
                    selectedTower = col.gameObject;
                    Debug.Log("Selected Tower: " + selectedTower.name);
                    selectedTower.GetComponent<TowerState>().SelectTower();
                    break;
                }
            }
        }
    }
}
