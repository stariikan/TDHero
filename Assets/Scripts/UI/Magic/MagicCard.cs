
using UnityEngine;
using UnityEngine.UI;

public class MagicCard : MonoBehaviour
{
    public GameObject magicObject;
    public GameObject player;
    public GameObject mainCamera;
    public Text magicDescription;
    public string upgradeText;
    public int magicLevel;
    public void OpenMagicCard()
    {
        magicLevel = magicObject.GetComponent<MagicBase>().level;
        if (magicLevel >= 1)
        {
            magicDescription.text = "Level: " + magicLevel + " Upgrade: " + upgradeText;
        }
    }
    public void ClaimMagic()
    {
        if (magicLevel > 0)
        {
            mainCamera.GetComponent<Pause>().LevelUpResume();
            magicObject.GetComponent<MagicBase>().IncreaseMagicLevel();
            player.GetComponent<PlayerStats>().SpentPoint();
        }
        if (magicLevel < 1) 
        {
            player.GetComponent<TDCombat>().PutNewMagicIntoSlot(magicObject);
            mainCamera.GetComponent<Pause>().LevelUpResume();
            magicObject.GetComponent<MagicBase>().IncreaseMagicLevel();
            player.GetComponent<PlayerStats>().SpentPoint();
        }

    }
}
