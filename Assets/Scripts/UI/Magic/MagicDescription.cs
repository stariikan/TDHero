using UnityEngine;
using UnityEngine.UI;

public class MagicDescription : MonoBehaviour
{
    public string howToUse;
    public string magicName;
    public bool magicIsActive;
    public GameObject icon;
    public GameObject[] descriptionSlot;
    public GameObject[] iconSlot;
    
    public void MagicSlotNumber(int number)
    {
        int slotNumber = number;
        icon.SetActive(true);
        magicIsActive = true;
        icon.transform.position = iconSlot[number].transform.position;
        descriptionSlot[number].GetComponent<Text>().text = "LMB + " + howToUse;
    }
}
