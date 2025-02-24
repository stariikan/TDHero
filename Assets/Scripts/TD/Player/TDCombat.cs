using UnityEngine;
using UnityEngine.UI;

public class TDCombat : MonoBehaviour
{
    public GameObject weapon;
    // Magic controll
    public string magicWord;
    public bool mouseState;     // Mouse state for activating magic
    public bool magicInCooldown; // State of possibility to use magic
    public float coolDownTimer;  
    public float coolDownTimerDuaration; // Duaration for cooldown of a magic
    // Magic abilities
    public GameObject[] magicObject;
    // UI
    public GameObject uiMagicBar;
    public GameObject uiMagicWindow;
    public Text uiMagicInfo;
    // Start is called before the first frame update
    void Start()
    {
        mouseState = false;
        magicInCooldown = false;
        coolDownTimer = coolDownTimerDuaration;
        if (coolDownTimerDuaration < 0) coolDownTimerDuaration = 0;
    }
    public void magicInput()
    {
        if (Input.GetMouseButtonDown(1) && magicInCooldown == false)
        {
            //uiMagicWindow.SetActive(true);
            mouseState = true;
            magicWord = "";
            
        }
        if (Input.GetMouseButtonUp(1) && magicInCooldown == false)
        {
            //uiMagicWindow.SetActive(false);
            mouseState = false;
            
        }
        if (mouseState && Input.GetKeyDown(KeyCode.Q)) magicWord = magicWord + "Q";
        if (mouseState && Input.GetKeyDown(KeyCode.E)) magicWord = magicWord + "E";
        if (mouseState && Input.GetKeyDown(KeyCode.F)) magicWord = magicWord + "F";
    }
    public void MagicCooldown()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer < coolDownTimerDuaration)
        {
            uiMagicBar.GetComponent<UIBarLogic>().ActivateBar();
            float ratio = coolDownTimer / coolDownTimerDuaration;
            uiMagicBar.GetComponent<UIBarLogic>().BarUpdate(ratio);
        }
        if (coolDownTimer >= coolDownTimerDuaration)
        {
            uiMagicBar.GetComponent<UIBarLogic>().DeactivateBar();
            magicInCooldown = false;
        }
    }
    public void MagicUse()
    {
        if (!magicInCooldown && !mouseState)
        {
            GameObject magic = null;
            string magicName = "";

            switch (magicWord)
            {
                case "FFF":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[0], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Firestorm";
                    uiMagicInfo.text = magicName;
                    break;
                case "QQQ":
                    if (magicObject[1] != null) magic = Instantiate(magicObject[1], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Storm Surge";
                    uiMagicInfo.text = magicName;
                    break;
                case "FEF":
                    if (magicObject[2] != null) magic = Instantiate(magicObject[2], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Lightning Shield";
                    uiMagicInfo.text = magicName;
                    this.gameObject.GetComponent<PlayerStats>().godmode = true;
                    break;
                case "FFQ":
                    if (magicObject[3] != null) magic = Instantiate(magicObject[3], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Flame Barrage";
                    uiMagicInfo.text = magicName;
                    break;
                case "EEE":
                    if (magicObject[4] != null) magic = Instantiate(magicObject[4], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Lightning";
                    uiMagicInfo.text = magicName;
                    break;
                case "FFE":
                    if (magicObject[5] != null) magic = Instantiate(magicObject[5], weapon.transform.position, this.transform.rotation);
                    magicName = "PoisonShpere";
                    uiMagicInfo.text = magicName;
                    break;
                case "FQF":

                    break;
                case "FQQ":

                    break;
                case "FQE":

                    break;

                case "FEQ":

                    break;
                case "FEE":

                    break;
                case "QFF":

                    break;
                case "QFQ":

                    break;
                case "QFE":

                    break;
                case "QQF":

                    break;

                case "QQE":

                    break;
                case "QEF":

                    break;
                case "QEQ":

                    break;
                case "QEE":

                    break;
                case "EFF":

                    break;
                case "EFQ":

                    break;
                case "EFE":

                    break;
                case "EQF":

                    break;
                case "EQQ":

                    break;
                case "EQE":

                    break;
                case "EEF":

                    break;
                case "EEQ":

                    break;

                default:
                    return;
            }
            //if (magic == null) uiMagicInfo.text = "No valid magic combination.";
            if (magic != null)
            {
                magic.name = magicName;
                magic.SetActive(true);
            }

            // Reset magic cooldown and input
            magicInCooldown = true;
            coolDownTimer = 0;
            magicWord = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        magicInput();
        MagicUse();
        if (magicInCooldown == true) MagicCooldown();
    }

}