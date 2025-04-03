using UnityEngine;
using UnityEngine.UI;

public class TileSelect : MonoBehaviour
{
    // Main camera obj
    public GameObject mainCamera;
    private GameObject selectedTile;
    private void Start()
    {
        selectedTile = null;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) Close();
        if (Input.GetMouseButtonDown(0)) Select();
    }
    private void Close()
    {
        selectedTile.GetComponent<Tile>().CloseTile();
    }
    private void Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
        foreach (RaycastHit hit in hits)
        {
            //Debug.Log("Hit: " + hit.collider.gameObject.name + " on layer " + LayerMask.LayerToName(hit.collider.gameObject.layer));

            // Check if the hit object is a tile
            if (hit.collider.gameObject.CompareTag("Tile"))
            {
                selectedTile = hit.collider.gameObject;
                break; // Stop looking once we find a tile
            }
        }
        selectedTile.GetComponent<Tile>().SelectTile();
    }
}
