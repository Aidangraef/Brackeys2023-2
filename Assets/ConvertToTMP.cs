using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertToTMP : MonoBehaviour
{
    public Text text;

    [SerializeField]
    bool shouldSendTextToVoiceManager = false;

    // Update is called once per frame
    void Update()
    {
        if(text.text != this.GetComponent<TMPro.TextMeshProUGUI>().text)
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = text.text;
            if (shouldSendTextToVoiceManager) {
                VoiceManager.manager.Speak(text.text);
            }
        }
    }
}
