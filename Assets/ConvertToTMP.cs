using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvertToTMP : MonoBehaviour
{
    public Text text;
    public string currentSpeaker;
    public int speechFreq = 0;
    public int i = 0;
    public bool talking;

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
            talking = true;
        }
        else
        {
            talking = false;
        }
        int actorID = DialogueManager.currentConversationState.subtitle.speakerInfo.id;
        if (actorID == 1)
        {
            currentSpeaker = "Player";
            AkSoundEngine.PostEvent("npcPlayer", this.gameObject);
        }
        else if (actorID == 2)
        {
            currentSpeaker = "Barkeep";
            AkSoundEngine.PostEvent("npcBarkeep", this.gameObject);
        }
        else if (actorID == 3)
        {
            currentSpeaker = "Ben";
            AkSoundEngine.PostEvent("npcBen", this.gameObject);
            speechFreq = 6;
        }
        else if (actorID == 4)
        {
            currentSpeaker = "Tina";
            AkSoundEngine.PostEvent("npcTina", this.gameObject);
            speechFreq = 6;
        }
        else if (actorID == 5)
        {
            currentSpeaker = "Kevin";
            AkSoundEngine.PostEvent("npcKevin", this.gameObject);
            speechFreq = 4;
        }
        else if (actorID == 6)
        {
            currentSpeaker = "Wally";
            AkSoundEngine.PostEvent("npcWally", this.gameObject);
        }
        else
        {
            Debug.LogWarning($"Dialogue System: Actor is Blank");
        }
    }

    private void FixedUpdate()
    {
        if(i >= speechFreq && talking)
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
