using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertToTMP : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text != this.GetComponent<TMPro.TextMeshProUGUI>().text)
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = text.text;
        }
    }
}
