using UnityEngine;
using UnityEngine.UI;

public class TileSelect : MonoBehaviour
{
    // Main camera obj
    public GameObject mainCamera;
    private GameObject selectedTower;
    public GameObject towerMenu;
    private bool rightMouseActive;
    private bool leftMouseActive;
    private void Start()
    {
        selectedTower = null;
        rightMouseActive = false;
        leftMouseActive = false;
    }
    void Update()
    {
        MouseButtonState();
        if (Input.GetMouseButtonDown(1)) Close();
        if (Input.GetMouseButtonDown(0) && !rightMouseActive) Select();
    }
    private void MouseButtonState() 
    {
        if (Input.GetMouseButtonDown(1)) rightMouseActive = true;
        if (Input.GetMouseButtonUp(1)) rightMouseActive = false;
        if (Input.GetMouseButtonDown(0)) leftMouseActive = true;
        if (Input.GetMouseButtonUp(0)) leftMouseActive = false;
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
