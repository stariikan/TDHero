using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    public void OpenWindow()
    {
        this.gameObject.SetActive(true);
        camera.GetComponent<TileSelector>().WindowOpened();
    }
    public void CloseWindow()
    {
        this .gameObject.SetActive(false);
        camera.GetComponent<TileSelector>().WindowClosed();
    }
}
