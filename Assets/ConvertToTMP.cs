using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertToTMP : MonoBehaviour
{
    public Text text;

    int i;
    bool talking;

    // Start is called before the first frame update
    void Awake()
    {
        i = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(text.text != this.GetComponent<TMPro.TextMeshProUGUI>().text)
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = text.text;
            talking = true;
        }
        else
        {
            talking = false;
        }
    }

    private void FixedUpdate()
    {
        if(i >= 3 && talking)
        {
            AkSoundEngine.PostEvent("npcTalk", this.gameObject);
            i = 0;
        }
        else
        {
            i += 1;
        }
    }
}
