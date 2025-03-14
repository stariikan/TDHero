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
                    if (magicObject[1] != null) magic = Instantiate(magicObject[1], weapon.transform.position, this.transform.rotation);
                    magicName = "Sun Strike";
                    uiMagicInfo.text = magicName;
                    break;
                case "FFE":
                    if (magicObject[2] != null) magic = Instantiate(magicObject[2], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Flame Thrower";
                    uiMagicInfo.text = magicName;
                    break;
                case "FFQ":
                    if (magicObject[3] != null) magic = Instantiate(magicObject[3], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Fire Bomb";
                    uiMagicInfo.text = magicName;
                    break;
                case "FEF":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[4], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "FQF":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[5], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "EFF":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[6], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "QFF":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[7], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "QQQ":
                    if (magicObject[8] != null) magic = Instantiate(magicObject[8], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Ice Thrower";
                    uiMagicInfo.text = magicName;
                    break;
                case "QQE":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[9], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "QQF":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[10], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "QEQ":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[11], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "QFQ":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[12], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "EQQ":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[13], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "FQQ":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[14], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "EEE":
                    if (magicObject[15] != null) magic = Instantiate(magicObject[15], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Lightning Strike";
                    uiMagicInfo.text = magicName;
                    break;
                case "EEF":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[16], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "EEQ":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[17], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "EFE":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[18], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "EQE":
                    if (magicObject[19] != null) magic = Instantiate(magicObject[19], weapon.transform.position, this.transform.rotation);
                    magicName = "Lightning Chain";
                    uiMagicInfo.text = magicName;
                    break;
                case "FEE":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[20], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "QEE":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[21], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "FQE":
                    if (magicObject[22] != null) magic = Instantiate(magicObject[22], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Electric Shield";
                    uiMagicInfo.text = magicName;
                    this.gameObject.GetComponent<PlayerStats>().godmode = true;
                    break;
                case "FEQ":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[23], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "QFE":
                    if (magicObject[24] != null) magic = Instantiate(magicObject[24], weapon.transform.position, this.transform.rotation);
                    magicName = "Poison Shpere";
                    uiMagicInfo.text = magicName;
                    break;
                case "EQF":
                    if (magicObject[25] != null) magic = Instantiate(magicObject[25], weapon.transform.position, this.transform.rotation);
                    magicName = "Drag The Enemy";
                    uiMagicInfo.text = magicName;
                    break;
                case "QEF":
                    if (magicObject[0] != null) magic = Instantiate(magicObject[26], weapon.transform.position, weapon.transform.rotation);
                    magicName = "Nothing";
                    uiMagicInfo.text = magicName;
                    break;
                case "EFQ":
                    if (magicObject[27] != null) magic = Instantiate(magicObject[27], weapon.transform.position, this.transform.rotation);
                    magicName = "Lift The Enemy";
                    uiMagicInfo.text = magicName;
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