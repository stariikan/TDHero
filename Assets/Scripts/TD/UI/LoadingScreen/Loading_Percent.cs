<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
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
=======
using System.Collections;
using System.Collections.Generic;
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
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
