using UnityEngine;
using UnityEngine.UI;

public class UICounter : MonoBehaviour
{
    public Text uiCounterText;
    private float currentText;
    void Start()
    {
        uiCounterText = this.gameObject.GetComponent<Text>();
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
        uiCounterText.text = "" + (currentText + counter);
    }
}
