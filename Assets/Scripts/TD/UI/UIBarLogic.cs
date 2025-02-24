<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarLogic : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera here
    public Image imageBar;
    public Transform target; // Assign the enemy's transform
    public Vector3 offset = new Vector3(0, 0, 0); // Adjust this to place the health bar above the enemy

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the health bar's position to follow the enemy
            transform.position = target.position + offset;

            // Optional: Make the health bar face the camera
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0); // Flip to face the camera correctly
        }
        if (mainCamera != null)
        {
            // Make the health bar face the camera
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
    }
    public void ActivateBar()
    {
        this.gameObject.SetActive(true);
    }
    public void DeactivateBar() 
    {
        gameObject.SetActive(false);
    }
    public void BarUpdate(float fraction)
    {
        imageBar.fillAmount = fraction;
    }
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarLogic : MonoBehaviour
{
    public Camera mainCamera; // Assign the main camera here
    public Image imageBar;
    public Transform target; // Assign the enemy's transform
    public Vector3 offset = new Vector3(0, 0, 0); // Adjust this to place the health bar above the enemy

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the health bar's position to follow the enemy
            transform.position = target.position + offset;

            // Optional: Make the health bar face the camera
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0); // Flip to face the camera correctly
        }
        if (mainCamera != null)
        {
            // Make the health bar face the camera
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
    }
    public void ActivateBar()
    {
        this.gameObject.SetActive(true);
    }
    public void DeactivateBar() 
    {
        gameObject.SetActive(false);
    }
    public void BarUpdate(float fraction)
    {
        imageBar.fillAmount = fraction;
    }
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera
        }
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
