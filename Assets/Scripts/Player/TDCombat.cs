using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TDCombat : MonoBehaviour
{
    // Melee attack
    public GameObject meleeDemonAttack;
    public GameObject meleeHumanAttack;
    private GameObject meleeAttack;
    public float attackSpeed;
    private float attackTimer;
    public float meleeDamage;
    public float meleeFreezePower;

    private bool playerDemon;
    private Animator playerAnimator;
    public GameObject playerModel;
    public GameObject playerModel_Demon;

    // Magic controll
    public GameObject weapon;
    public string magicWord;
    public bool magicInCooldown; // State of possibility to use magic
    public float coolDownTimer;
    public float coolDownTimerDuaration; // Duaration for cooldown of a magic
    // Magic abilities
    public GameObject[] magicObject;
    // UI
    public GameObject uiMagicBar;
    public GameObject[] magicSlotUI;
    public Text uiMagicInfo;
    // Learned skills
    public GameObject[] magicSlot = new GameObject[6];
    public bool[] magicSlotActive = new bool[6];
    public bool playerSpentAllSlots;

    // Mouse State
    private bool rightMouseActive;
    private bool leftMouseActive;

    void Start()
    {
        magicInCooldown = false;
        rightMouseActive = false;
        leftMouseActive = false;
        coolDownTimer = coolDownTimerDuaration;
        if (coolDownTimerDuaration < 0) coolDownTimerDuaration = 0;
    }
    public void PutNewMagicIntoSlot(GameObject magic)
    {
        // Try to insert the magic into the first available slot
        for (int i = 0; i < magicSlot.Length; i++)
        {
            if (!magicSlotActive[i])
            {
                magicSlot[i] = magic;
                magicSlotActive[i] = true;
                //Debug.Log("Magic Slot: " + magicSlot[i]);
                magic.GetComponent<MagicDescription>().MagicSlotNumber(i);
                magic.GetComponent<MagicBase>().IncreaseMagicLevel();
                break;
            }
        }
        // Check if all slots are used
        playerSpentAllSlots = magicSlotActive.All(active => active);
    }
    private bool IsMagicInAnySlot(GameObject magic)
    {
        foreach (var slot in magicSlot)
        {
            if (slot == magic)
                return true;
        }
        return false;
    }
    public void magicInput()
    {
        if (Input.GetMouseButtonDown(1) && magicInCooldown == false)
        {
            magicWord = "";
        }
        if (rightMouseActive && Input.GetKeyDown(KeyCode.Q) && magicWord.Length < 3) magicWord = magicWord + "q";
        if (rightMouseActive && Input.GetKeyDown(KeyCode.E) && magicWord.Length < 3) magicWord = magicWord + "e";
        if (rightMouseActive && Input.GetKeyDown(KeyCode.F) && magicWord.Length < 3) magicWord = magicWord + "f";
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
            //uiMagicBar.GetComponent<UIBarLogic>().DeactivateBar();
            magicInCooldown = false;
        }
    }
    private void MouseButtonState()
    {
        if (Input.GetMouseButtonDown(1)) rightMouseActive = true;
        if (Input.GetMouseButtonUp(1)) rightMouseActive = false;
        if (Input.GetMouseButtonDown(0)) leftMouseActive = true;
        if (Input.GetMouseButtonUp(0)) leftMouseActive = false;
    }
    public void ImproveAttack()
    {
        meleeDamage *= 1.1f;
    }
    public void ImproveFreezePower()
    {
        meleeFreezePower *= 0.9f;
    }
    public void IncreaseAttackSpeed(float increaseAttackSpeed)
    {
        attackSpeed *= increaseAttackSpeed;
    }
    public void DecreaseAttackSpeed(float decreaseAttackSpeed)
    {
        attackSpeed *= decreaseAttackSpeed;
    }
    private void MeleeAttack()
    {
        if (Input.GetMouseButtonDown(0) && rightMouseActive && attackTimer > attackSpeed)
        {
            attackTimer = 0;
            meleeAttack.GetComponent<MeleeAttackZone>().getDamageInfo(meleeDamage);
            meleeAttack.GetComponent<MeleeAttackZone>().getFreezeInfo(meleeFreezePower);
            meleeAttack.GetComponent<MeleeAttackZone>().CanAttack();
            meleeAttack.SetActive(true);
            this.gameObject.GetComponent<TDPlayerMovement>().AttackTrigger();
        }
    }
    public void MagicUse()
    {
        if (!magicInCooldown && !rightMouseActive)
        {
            GameObject magic = null;
            string magicName = "";
            weapon = meleeAttack;
            switch (magicWord)
            {
                case "fff":
                    if (magicObject[1] != null && IsMagicInAnySlot(magicObject[1]))
                    {
                        magic = Instantiate(magicObject[1], weapon.transform.position, transform.rotation);
                        magicName = "sun strike";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "ffe":
                    if (magicObject[2] != null && IsMagicInAnySlot(magicObject[2]))
                    {
                        magic = Instantiate(magicObject[2], weapon.transform.position, weapon.transform.rotation);
                        magicName = "flame wall";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "ffq":
                    if (magicObject[3] != null && IsMagicInAnySlot(magicObject[3]))
                    {
                        magic = Instantiate(magicObject[3], weapon.transform.position, weapon.transform.rotation);
                        magicName = "fire bomb";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "fef":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[4], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "fqf":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[5], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "eff":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[6], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qff":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[7], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qq":
                    if (magicObject[8] != null && IsMagicInAnySlot(magicObject[8]))
                    {
                        magic = Instantiate(magicObject[8], weapon.transform.position, weapon.transform.rotation);
                        magicName = "ice thrower";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qqe":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[9], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qqf":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[10], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qeq":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[11], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qfq":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[12], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "eqq":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[13], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "fqq":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[14], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "eee":
                    if (magicObject[15] != null && IsMagicInAnySlot(magicObject[15]))
                    {
                        magic = Instantiate(magicObject[15], weapon.transform.position, weapon.transform.rotation);
                        magicName = "lightning strike";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "eef":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[16], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "eeq":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[17], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "efe":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[18], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "eqe":
                    if (magicObject[19] != null && IsMagicInAnySlot(magicObject[19]))
                    {
                        magic = Instantiate(magicObject[19], weapon.transform.position, this.transform.rotation);
                        magicName = "lightning chain";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "fee":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[20], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qee":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[21], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "fqe":
                    if (magicObject[22] != null && IsMagicInAnySlot(magicObject[22]))
                    {
                        magic = Instantiate(magicObject[22], weapon.transform.position, weapon.transform.rotation);
                        magicName = "electric shield";
                        uiMagicInfo.text = magicName;
                        this.gameObject.GetComponent<PlayerStats>().godmode = true;
                    }
                    break;
                case "feq":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[23], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qfe":
                    if (magicObject[24] != null && IsMagicInAnySlot(magicObject[24]))
                    {
                        magic = Instantiate(magicObject[24], weapon.transform.position, this.transform.rotation);
                        magicName = "poison shpere";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "eqf":
                    if (magicObject[25] != null && IsMagicInAnySlot(magicObject[25]))
                    {
                        magic = Instantiate(magicObject[25], weapon.transform.position, this.transform.rotation);
                        magicName = "drag the enemy";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "qef":
                    if (magicObject[0] != null && IsMagicInAnySlot(magicObject[0]))
                    {
                        magic = Instantiate(magicObject[26], weapon.transform.position, weapon.transform.rotation);
                        magicName = "nothing";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                case "efq":
                    if (magicObject[27] != null && IsMagicInAnySlot(magicObject[27]))
                    {
                        magic = Instantiate(magicObject[27], weapon.transform.position, this.transform.rotation);
                        magicName = "lift The enemy";
                        uiMagicInfo.text = magicName;
                    }
                    break;
                default:
                    return;
            }
            //if (magic == null) uiMagicInfo.text = "No valid magic combination.";
            if (magic != null)
            {
                magic.name = magicName;
                Debug.Log(magic.name);
                magicInCooldown = true;
                coolDownTimer = 0;
                playerAnimator.SetTrigger("cast");
                magicWord = "";
                magic.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        playerDemon = this.gameObject.GetComponent<PlayerStats>().playerDemon;
        if (playerDemon == true)
        {
            meleeAttack = meleeDemonAttack;
            playerAnimator = playerModel_Demon.GetComponent<Animator>();
        }
        else
        {
            meleeAttack = meleeHumanAttack;
            playerAnimator = playerModel.GetComponent<Animator>();
        }
        MouseButtonState();
        magicInput();
        MagicUse();
        MeleeAttack();
        if (magicInCooldown == true) MagicCooldown();
    }

}