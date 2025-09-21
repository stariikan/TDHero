using UnityEngine;

public class TileSelector : MonoBehaviour
{
    // Main camera obj
    public GameObject mainCamera;
    private GameObject selectedTile;
    private bool canChangeTile;
    private bool rightMouseActive;
    private bool leftMouseActive;
    private bool anyWindowOpened;
    private void Start()
    {
        selectedTile = null;
        canChangeTile = true;
        rightMouseActive = false;
        leftMouseActive = false;
    }
    void Update()
    {
        MouseButtonState();
        if (Input.GetMouseButtonDown(0) && !rightMouseActive && !anyWindowOpened) ChangeTile();
    }
    public void WindowOpened()
    {
        if (anyWindowOpened == false) anyWindowOpened = true;
    }
    public void WindowClosed()
    {
        if (anyWindowOpened == true) anyWindowOpened = false;
    }
    private void MouseButtonState()
    {
        if (Input.GetMouseButtonDown(1)) rightMouseActive = true;
        if (Input.GetMouseButtonUp(1)) rightMouseActive = false;
        if (Input.GetMouseButtonDown(0)) leftMouseActive = true;
        if (Input.GetMouseButtonUp(0)) leftMouseActive = false;
    }
    public void AbilityToSelectTile(bool canSelect)
    {
        canChangeTile = canSelect;
    }
    private void ChangeTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (RaycastHit hit in hits)
        {
            Collider col = hit.collider;

            // Check if the hit object is a tower AND it has a BoxCollider (not a trigger)
            if ((col.gameObject.CompareTag("Tile") || col.gameObject.CompareTag("Ground")) && col is BoxCollider && !col.isTrigger)
            {
                selectedTile = col.gameObject;
                Debug.Log("Changed Tile: " + selectedTile.name);
                selectedTile.GetComponent<TileState>().ChangeTile();
                break;
            }
        }
    }
}
