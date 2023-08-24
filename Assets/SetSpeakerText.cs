using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SetSpeakerText : MonoBehaviour {

    [SerializeField]
    GameObject voiceObj;

    ConvertToTMP convertToTmpScript;

    private void Start() {
        convertToTmpScript = FindObjectOfType<ConvertToTMP>();
    }

    void OnConversationLine(Subtitle subtitle) {
        Debug.Log(subtitle.speakerInfo.Name + "..." + subtitle.dialogueEntry.currentDialogueText);
        // Prepare voice manager
        VoiceManager.manager.CurrentDialogueText = subtitle.dialogueEntry.currentDialogueText;

        switch (subtitle.speakerInfo.Name) {
            case "Ben \"Big Shot\" Blamblin":
                AkSoundEngine.PostEvent("npcBen", voiceObj);
                VoiceManager.manager.WordSpeed = 2;
                break;

            case "Bartender":
                AkSoundEngine.PostEvent("npcKevin", voiceObj);
                VoiceManager.manager.WordSpeed = 1;
                break;
        }
    }
}