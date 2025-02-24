using UnityEngine;
using UnityEngine.UI;//дл¤ UI

public class Loading_Screen : MonoBehaviour
{
    private int percent_ui; //тут сделали переменную чтобы она потом собирала значение переменной hp из скрипта Hero
    public RawImage image;
    public Texture [] images;
    private void Start()
    {
        image = this.gameObject.GetComponent<RawImage>();
        if (image.texture != null) image.texture = images[Random.Range(0, images.Length)];
    }
    void Update() //ќбновление значени¤ происходит при обновлении каждого кадра
    {
        PercentUiUpdate();

    }
    private void PercentUiUpdate()
    {
        if (percent_ui < 100)
        {
            percent_ui = Map_Generate.Instance.generate_percent_done;
        }
        if (percent_ui == 100)
        {
            this.gameObject.SetActive(false);
        }
    }
}