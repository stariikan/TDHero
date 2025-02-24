using UnityEngine;

public class TurnOfUiObjecth : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 7)
        {
            timer = 0;
            this.gameObject.SetActive(false);
        }
    }
}
