using UnityEngine;

public class TileState : MonoBehaviour
{
    public GameObject oppositeTile;
    public void ChangeTile()
    {
        if (oppositeTile != null)
        {
            oppositeTile.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
