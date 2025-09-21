using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_selector : MonoBehaviour
{
    public GameObject [] environments;

    public void ChangeMap()
    {
        int currentIndex = -1;

        // Find the currently active environment
        for (int i = 0; i < environments.Length; i++)
        {
            if (environments[i] != null && environments[i].activeSelf)
            {
                currentIndex = i;
                environments[i].SetActive(false);
                break;
            }
        }

        // Determine the next index (loop back to 0 if at the end)
        int nextIndex = (currentIndex + 1) % environments.Length;

        // Activate the next environment
        if (environments[nextIndex] != null)
        {
            environments[nextIndex].SetActive(true);
        }
    }
}
