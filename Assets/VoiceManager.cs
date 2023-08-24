using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    string currentDialogueText;

    // How many words to play another sound
    int wordSpeed = 1;

    int wordsSpoken = 0;

    public static VoiceManager manager;

    public string CurrentDialogueText { get => currentDialogueText; set { currentDialogueText = value; wordsSpoken = 0; AkSoundEngine.PostEvent("npcTalk", this.gameObject); } }
    public int WordSpeed { get => wordSpeed; set => wordSpeed = value; }

    private void Awake() {
        manager = this;
    }

    public void Speak(string currentSpeechPosition) {
        if (currentSpeechPosition.Length == 0) {
            return;
        }

        // Compares current speech to current dialogue to check for words
        int similarUpTo = 0;
        for (int i = 0; i < currentDialogueText.Length; i++) {
            if (currentDialogueText[i] == currentSpeechPosition[i]) {
                similarUpTo = i;
            } else {
                break;
            }
        }

        Debug.Log(currentDialogueText.Substring(0, similarUpTo + 1).Split(" ").Length + "..." + currentDialogueText.Substring(0, similarUpTo + 1));

        // Get words in what  was already spoken
        if (currentDialogueText.Substring(0, similarUpTo + 1).Split(" ").Length - wordsSpoken >= wordSpeed) {
            wordsSpoken += wordSpeed;
            AkSoundEngine.PostEvent("npcTalk", this.gameObject);
        }

    }

    
}
