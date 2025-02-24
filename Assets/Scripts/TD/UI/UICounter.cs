<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICounter : MonoBehaviour
{
    public Text uiCounterText;
    private string currentText;
    void Start()
    {
        uiCounterText = GetComponent<Text>();
        currentText = uiCounterText.text;
    }
    public void ActivateUI()
    {
        this.gameObject.SetActive(true);
    }
    public void DeactivateUI()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    public void TakeCounterData(float counter)
    {
        uiCounterText.text = currentText + counter;
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICounter : MonoBehaviour
{
    public Text uiCounterText;
    private string currentText;
    void Start()
    {
        uiCounterText = GetComponent<Text>();
        currentText = uiCounterText.text;
    }
    public void ActivateUI()
    {
        this.gameObject.SetActive(true);
    }
    public void DeactivateUI()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    public void TakeCounterData(float counter)
    {
        uiCounterText.text = currentText + counter;
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
