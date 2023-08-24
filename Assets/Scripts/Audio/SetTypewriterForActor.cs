using UnityEngine;
using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class SetTypewriterForActor : MonoBehaviour
{
    [SerializeField] private int currentTalkSpeed = 0;
    public AK.Wwise.Event KevinEvent;
    public AK.Wwise.Event BenEvent;
    public AK.Wwise.Event BarkeepEvent;
    public AK.Wwise.Event TinaEvent;
    public AK.Wwise.Event WallyEvent;
    public void OnBeginTypewriter() // Assign to typewriter's OnBegin() event.
    {
        // Look up character's switch and talking speed:
        var actorName = DialogueManager.currentConversationState.subtitle.speakerInfo.nameInDatabase;
        string switchName = DialogueManager.masterDatabase.GetActor(actorName).LookupValue("WwiseSwitch");
        Debug.Log(switchName);
        switch(switchName)
        {
            case "npcKevin":
                KevinEvent.Post(gameObject); break;
            case "npcBen":
                BenEvent.Post(gameObject); break;
            case "npcBarkeep":
                BarkeepEvent.Post(gameObject); break;
            case "npcTina":
                TinaEvent.Post(gameObject); break;
            case "npcWally":
                WallyEvent.Post(gameObject); break;
        }
        currentTalkSpeed = DialogueManager.masterDatabase.GetActor(actorName).LookupInt("speechSpeed");
        CharacterSpeech.currentSpeechSpeed = currentTalkSpeed;
        CharacterSpeech.charPassed = currentTalkSpeed;
    }
}