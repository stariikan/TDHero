using UnityEngine;
using UnityEngine.UI;
public class InputDisplay : MonoBehaviour
{
    public Text inputText;
    public bool mouseState;
    public GameObject[] magicIcon;
    public GameObject playerObject;
    public bool magicInCooldown;
    // Start is called before the first frame update
    void Start()
    {
        inputText = GetComponent<Text>();
        mouseState = false;
    }
    public void MagicIcons()
    {
        if (inputText.text == "FFF" || inputText.text == "FF" || inputText.text == "F")
        {
            if (magicIcon[0] != null) magicIcon[0].SetActive(true);
        }
        else
        {
            if (magicIcon[0] != null) magicIcon[0].SetActive(false);
        }

        if (inputText.text == "QQQ" || inputText.text == "QQ" || inputText.text == "Q")
        {
            if (magicIcon[1] != null) magicIcon[1].SetActive(true);
        }
        else
        {
            if (magicIcon[1] != null) magicIcon[1].SetActive(false);
        }

        if (inputText.text == "FEF" || inputText.text == "FE" || inputText.text == "F")
        {
            if (magicIcon[2] != null) magicIcon[2].SetActive(true);
        }
        else
        {
            if (magicIcon[2] != null) magicIcon[2].SetActive(false);
        }

        if (inputText.text == "FFQ" || inputText.text == "FF" || inputText.text == "F")
        {
            if (magicIcon[3] != null) magicIcon[3].SetActive(true);
        }
        else
        {
            if (magicIcon[3] != null) magicIcon[3].SetActive(false);
        }

        if (inputText.text == "EEE" || inputText.text == "EE" || inputText.text == "E")
        {
            if (magicIcon[4] != null) magicIcon[4].SetActive(true);
        }
        else
        {
            if (magicIcon[4] != null) magicIcon[4].SetActive(false);
        }

        if (inputText.text == "FFE" || inputText.text == "FF" || inputText.text == "F")
        {
            if (magicIcon[6] != null) magicIcon[6].SetActive(true);
        }
        else
        {
            if (magicIcon[6] != null) magicIcon[6].SetActive(false);
        }

        if (inputText.text == "FQF" || inputText.text == "FQ" || inputText.text == "F")
        {
            if (magicIcon[13] != null) magicIcon[13].SetActive(true);
        }
        else
        {
            if (magicIcon[13] != null) magicIcon[13].SetActive(false);
        }

        if (inputText.text == "FQQ" || inputText.text == "FQ" || inputText.text == "F")
        {
            if (magicIcon[26] != null) magicIcon[26].SetActive(true);
        }
        else
        {
            if (magicIcon[26] != null) magicIcon[26].SetActive(false);
        }
        
        if (inputText.text == "FQE" || inputText.text == "FQ" || inputText.text == "F")
        {
            if (magicIcon[5] != null) magicIcon[5].SetActive(true);
        }
        else
        {
            if (magicIcon[5] != null) magicIcon[5].SetActive(false);
        }
               
        if (inputText.text == "FEQ" || inputText.text == "FE" || inputText.text == "F")
        {
            if (magicIcon[7] != null) magicIcon[7].SetActive(true);
        }
        else
        {
            if (magicIcon[7] != null) magicIcon[7].SetActive(false);
        }
            
        if (inputText.text == "FEE" || inputText.text == "FE" || inputText.text == "F")
        {
            if (magicIcon[8] != null) magicIcon[8].SetActive(true);
        }
        else
        {
            if (magicIcon[8] != null) magicIcon[8].SetActive(false);
        }

        if (inputText.text == "QFF" || inputText.text == "QF" || inputText.text == "Q")
        {
            if (magicIcon[9] != null) magicIcon[9].SetActive(true);
        }
        else
        {
            if (magicIcon[9] != null) magicIcon[9].SetActive(false);
        }

        if (inputText.text == "QFQ" || inputText.text == "QF" || inputText.text == "Q")
        {
            if (magicIcon[10] != null) magicIcon[10].SetActive(true);
        }
        else
        {
            if (magicIcon[10] != null) magicIcon[10].SetActive(false);
        }

        if (inputText.text == "QFE" || inputText.text == "QF" || inputText.text == "Q")
        {
            if (magicIcon[11] != null) magicIcon[11].SetActive(true);
        }
        else
        {
            if (magicIcon[11] != null) magicIcon[11].SetActive(false);
        }

        if (inputText.text == "QQF" || inputText.text == "QQ" || inputText.text == "Q")
        {
            if (magicIcon[12] != null) magicIcon[12].SetActive(true);
        }
        else
        {
            if (magicIcon[12] != null) magicIcon[12].SetActive(false);
        }

        if (inputText.text == "QQE" || inputText.text == "QQ" || inputText.text == "Q")
        {
            if (magicIcon[14] != null) magicIcon[14].SetActive(true);
        }
        else
        {
            if (magicIcon[14] != null) magicIcon[14].SetActive(false);
        }

        if (inputText.text == "QEF" || inputText.text == "QE" || inputText.text == "Q")
        {
            if (magicIcon[15] != null) magicIcon[15].SetActive(true);
        }
        else
        {
            if (magicIcon[15] != null) magicIcon[15].SetActive(false);
        }
        if (inputText.text == "QEQ" || inputText.text == "QE" || inputText.text == "Q")
        {
            if (magicIcon[16] != null) magicIcon[16].SetActive(true);
        }
        else
        {
            if (magicIcon[16] != null) magicIcon[16].SetActive(false);
        }

        if (inputText.text == "QEE" || inputText.text == "QE" || inputText.text == "Q")
        {
            if (magicIcon[17] != null) magicIcon[17].SetActive(true);
        }
        else
        {
            if (magicIcon[17] != null) magicIcon[17].SetActive(false);
        }

        if (inputText.text == "EFF" || inputText.text == "EF" || inputText.text == "E")
        {
            if (magicIcon[18] != null) magicIcon[18].SetActive(true);
        }
        else
        {
            if (magicIcon[18] != null) magicIcon[18].SetActive(false);
        }

        if (inputText.text == "EFQ" || inputText.text == "EF" || inputText.text == "E")
        {
            if (magicIcon[19] != null) magicIcon[19].SetActive(true);
        }
        else
        {
            if (magicIcon[19] != null) magicIcon[19].SetActive(false);
        }

        if (inputText.text == "EFE" || inputText.text == "EF" || inputText.text == "E")
        {
            if (magicIcon[20] != null) magicIcon[20].SetActive(true);
        }
        else
        {
            if (magicIcon[20] != null) magicIcon[20].SetActive(false);
        }

        if (inputText.text == "EQF" || inputText.text == "EQ" || inputText.text == "E")
        {
            if (magicIcon[21] != null) magicIcon[21].SetActive(true);
        }
        else
        {
            if (magicIcon[21] != null) magicIcon[21].SetActive(false);
        }

        if (inputText.text == "EQQ" || inputText.text == "EQ" || inputText.text == "E")
        {
            if (magicIcon[22] != null) magicIcon[22].SetActive(true);
        }
        else
        {
            if (magicIcon[22] != null) magicIcon[22].SetActive(false);
        }

        if (inputText.text == "EQE" || inputText.text == "EQ" || inputText.text == "E")
        {
            if (magicIcon[23] != null) magicIcon[23].SetActive(true);
        }
        else
        {
            if (magicIcon[23] != null) magicIcon[23].SetActive(false);
        }

        if (inputText.text == "EEF" || inputText.text == "EE" || inputText.text == "E")
        {
            if (magicIcon[24] != null) magicIcon[24].SetActive(true);
        }
        else
        {
            if (magicIcon[24] != null) magicIcon[24].SetActive(false);
        }

        if (inputText.text == "EEQ" || inputText.text == "EE" || inputText.text == "E")
        {
            if (magicIcon[25] != null) magicIcon[25].SetActive(true);
        }
        else
        {
            if (magicIcon[25] != null) magicIcon[25].SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        magicInCooldown = playerObject.GetComponent<TDCombat>().magicInCooldown;
        MagicIcons();
        if (Input.GetMouseButtonDown(1) && magicInCooldown == false)
        {
            mouseState = true;

        }
        if (Input.GetMouseButtonUp(1) && magicInCooldown == false)
        {
            mouseState = false;
            inputText.text = "";
        }
        if (mouseState && Input.GetKeyDown(KeyCode.Q)) inputText.text = inputText.text + "Q";
        if (mouseState && Input.GetKeyDown(KeyCode.E)) inputText.text = inputText.text + "E";
        if (mouseState && Input.GetKeyDown(KeyCode.F)) inputText.text = inputText.text + "F";
    }
}