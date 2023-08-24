using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for keeping all game info
public class GameController : MonoBehaviour
{
    // This has to be ordered as the CharacterEnum
    [SerializeField]
    List<CharacterThoughtsScriptableObject> characterThoughts;

    [SerializeField]
    List<MemoryEnum> memoriesSeen = new List<MemoryEnum>();

    [SerializeField]
    CharacterEnum currentCharacterDive;

    public static GameController controller;

    public CharacterEnum CurrentCharacterDive { get => currentCharacterDive; set => currentCharacterDive = value; }
    public List<MemoryEnum> MemoriesSeen { get => memoriesSeen; set => memoriesSeen = value; }
    public List<CharacterThoughtsScriptableObject> CharacterThoughts { get => characterThoughts; set => characterThoughts = value; }

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

#if UNITY_EDITOR
    private void OnValidate() {
        foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum))) {
            if (characterThoughts[(int)character].character != character) {
                Debug.LogError("Character thoughts in Game Controller aren't sorted in the same order as CharacterEnum!");
            }
        }
    }
#endif
}
