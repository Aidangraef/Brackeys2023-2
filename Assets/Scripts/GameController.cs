using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTypes;

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

    // Keeps track of the player position/direction when returning to the bar scene
    Vector3 playerPosition;
    Vector3 playerScale;
    bool playerFacingRight;
    bool playerSaved = false;

    public static GameController controller;

    public CharacterEnum CurrentCharacterDive { get => currentCharacterDive; set => currentCharacterDive = value; }
    public List<MemoryEnum> MemoriesSeen { get => memoriesSeen; set => memoriesSeen = value; }
    public List<CharacterThoughtsScriptableObject> CharacterThoughts { get => characterThoughts; set => characterThoughts = value; }

    void Awake() {
        if (controller == null) {
            controller = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        } else {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // TODO Change to use UnityScenes
        switch ((UnityScenes)scene.buildIndex) {
            case UnityScenes.Bar:
                if (playerSaved) {
                    LoadPlayerTransform();
                }
                break;

            case UnityScenes.Minigame:
                // TODO Get index from DISO inside MinigameController
                currentCharacterDive = ConvertSceneIndexToCharacter(3);

                // Load character
                MinigameController.controller.SetCharacter(currentCharacterDive);
                break;
        }
    }

    CharacterEnum ConvertSceneIndexToCharacter(int index) {
        switch ((MemoryEnum)index) {    
            case MemoryEnum.BEN_PAST:
            case MemoryEnum.BEN_WALLY_GET_TIPSY:
            case MemoryEnum.BEN_GUN_GOES_MISSING:
                return CharacterEnum.BEN;

            case MemoryEnum.TINA_SINGS_LA_CANTATA:
            case MemoryEnum.TINA_KEVIN_TOGETHER:
            case MemoryEnum.TINA_BUSINESS_FAIL:
                return CharacterEnum.TINA;

            case MemoryEnum.KEVIN_BEATS_VINNIE_POOL:
            case MemoryEnum.KEVIN_HIDES_NERDY_SIDE:
            case MemoryEnum.KEVIN_KNOWS_DETECTIVE:
                return CharacterEnum.KEVIN;

            case MemoryEnum.WALLY_LOSES_VINNIE_POKER:
            case MemoryEnum.WALLY_SUSPICIOUS_PHONE_CALL:
            case MemoryEnum.WALLY_YOU_SHOT_VINNIE:
                return CharacterEnum.WALLY;

            default:
                Debug.LogError("Character not found!");
                return 0;
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

    public void SavePlayerTransform() {
        CharacterController characterController = FindObjectOfType<CharacterController>();
        playerPosition = characterController.transform.position;
        playerScale = characterController.transform.localScale;
        playerFacingRight = characterController.FacingRight;
        playerSaved = true;
    }

    public void LoadPlayerTransform() {
        // Find character controller
        CharacterController characterController = FindObjectOfType<CharacterController>();
        characterController.transform.position = playerPosition;
        characterController.transform.localScale = playerScale;
        characterController.FacingRight = playerFacingRight;
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
