using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for keeping all game info
public class GameController : MonoBehaviour
{
    [SerializeField]
    List<MemoryEnum> memoriesSeen = new List<MemoryEnum>();

    [SerializeField]
    CharacterEnum currentCharacterDive;

    public static GameController controller;

    public CharacterEnum CurrentCharacterDive { get => currentCharacterDive; }

    void Awake() {
        if (controller == null) {
            controller = this;

            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void NewMemorySeen(MemoryEnum memory) {
        if (!memoriesSeen.Contains(memory)) {
            memoriesSeen.Add(memory);
        }
    }

    public void DiveIntoCharacterMemory(CharacterEnum character) {
        currentCharacterDive = character;

        // Call minigame scene
    }

    public bool IsMemorySeen(MemoryEnum memory) {
        return memoriesSeen.Contains(memory);
    }

    public void SubmitCulprit(CharacterEnum character) {
        // TODO Prepare code to send to ending
    }
}
