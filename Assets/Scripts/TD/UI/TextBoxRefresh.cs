<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TextBoxRefresh : MonoBehaviour
{
    public Text textBox;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox.text == "") timer = 0;
        if (textBox.text != "")
        {
            timer += Time.deltaTime;
            if (timer > 5 && textBox.text != "Hold down the right mouse button, type \"FFF\" or \"EEE\" or \"QQQ\", then release the right mouse button to cast the spell. ") textBox.text = "";
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TextBoxRefresh : MonoBehaviour
{
    public Text textBox;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox.text == "") timer = 0;
        if (textBox.text != "")
        {
            timer += Time.deltaTime;
            if (timer > 5 && textBox.text != "Hold down the right mouse button, type \"FFF\" or \"EEE\" or \"QQQ\", then release the right mouse button to cast the spell. ") textBox.text = "";
        }
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
