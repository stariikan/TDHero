using UnityEngine;
using UnityEngine.UI;//�� UI

public class Loading_Percent : MonoBehaviour
{
    private int percent_ui; //��� ������� ���������� ����� ��� ����� �������� �������� ���������� hp �� ������� Hero
    void Update() //���������� ������� ���������� ��� ���������� ������� �����
    {
        PercentUiUpdate();
    }
    private void PercentUiUpdate()
    {
        if (percent_ui < 100)
        {
            percent_ui = Map_Generate.Instance.generate_percent_done;
            GetComponent<Text>().text = $"{percent_ui}%";
        }
        if (percent_ui == 100)
        {
            this.gameObject.SetActive(false);
        }
    }
}
