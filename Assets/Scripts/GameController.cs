using PixelCrushers.DialogueSystem;
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

    // Keeps track of the player position/direction when returning to the bar scene
    Vector3 playerPosition;
    Vector3 playerScale;
    bool playerFacingRight;
    bool playerSaved = false;

    // Keeps track of the tutorial for the minigame
    bool minigameFirstTimer = true;

    public static GameController controller;

    public List<MemoryEnum> MemoriesSeen { get => memoriesSeen; set => memoriesSeen = value; }
    public List<CharacterThoughtsScriptableObject> CharacterThoughts { get => characterThoughts; set => characterThoughts = value; }

    // Graph's Additions
    public bool didIJustShootSomeone; // Used to show one of the detective's inner monologues
    public GameObject didIJustShootSomeoneGameObject;

    public bool wasThatMe; // Same thing as above
    public GameObject wasThatMeGameObject;

    public bool barTenderIntro;
    public GameObject barTenderIntroGameObject;

    void Awake() {
        if (controller == null) {
            controller = this;

            DontDestroyOnLoad(gameObject);

            ResetDialogueManagerVariables();

            SceneManager.sceneLoaded += OnSceneLoaded;
        } else {
            Destroy(gameObject);
        }

        didIJustShootSomeone = false;
        wasThatMe = false;
        barTenderIntro = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // For every scene loaded, reinitialize Dialogue Manager variables
        PopulateDialogueVariables();

        switch ((UnityScenes)scene.buildIndex) {
            case UnityScenes.Bar:
                // If there is a player saved, load the transform to keep player at the same place/direction
                if (playerSaved) {
                    LoadPlayerTransform();
                }
                // check to see if we should show the dialogue
                if (didIJustShootSomeone) {
                    didIJustShootSomeoneGameObject.SetActive(true);
                    didIJustShootSomeone = false;
                }
                // same here
                if(wasThatMe) {
                    wasThatMeGameObject.SetActive(true);
                    wasThatMe = false;
                }
                if(barTenderIntro)
                {
                    barTenderIntroGameObject.SetActive(true);
                    barTenderIntro = false;
                }
                break;

            case UnityScenes.Minigame:
                // Prepare to show tutorial
                if (minigameFirstTimer) {
                    MinigameController.controller.ShowTutorial();
                    minigameFirstTimer = false;
                }
                break;

        }
    }

    void ResetDialogueManagerVariables() {
        DialogueLua.SetVariable("SawNoMemory", true);
        DialogueLua.SetVariable("SawMemory4", false);
        DialogueLua.SetVariable("SawMemory5", false);
        DialogueLua.SetVariable("SawMemory6", false);
        DialogueLua.SetVariable("SawMemory10", false);
        DialogueLua.SetVariable("SawMemory11", false);
        DialogueLua.SetVariable("SawMemory12", false);
        DialogueLua.SetVariable("SawMemory13", false);
        DialogueLua.SetVariable("SawMemory14", false);
    }

    public void PopulateDialogueVariables() {
        if (memoriesSeen.Count > 0) {
            DialogueLua.SetVariable("SawNoMemory", false);

            foreach (MemoryEnum memory in memoriesSeen) {
                switch (memory) {
                    case MemoryEnum.BEN_WALLY_GET_TIPSY:
                        DialogueLua.SetVariable("SawMemory4", true);
                        break;
                    case MemoryEnum.BEN_GUN_GOES_MISSING:
                        DialogueLua.SetVariable("SawMemory5", true);
                        break;
                    case MemoryEnum.TINA_SINGS_LA_CANTATA:
                        DialogueLua.SetVariable("SawMemory6", true);
                        break;
                    case MemoryEnum.KEVIN_HIDES_NERDY_SIDE:
                        DialogueLua.SetVariable("SawMemory10", true);
                        break;
                    case MemoryEnum.KEVIN_KNOWS_DETECTIVE:
                        DialogueLua.SetVariable("SawMemory11", true);
                        break;
                    case MemoryEnum.WALLY_LOSES_VINNIE_POKER:
                        DialogueLua.SetVariable("SawMemory12", true);
                        break;
                    case MemoryEnum.WALLY_SUSPICIOUS_PHONE_CALL:
                        DialogueLua.SetVariable("SawMemory13", true);
                        break;
                    case MemoryEnum.VINNIE_YOU_SHOT_VINNIE:
                        DialogueLua.SetVariable("SawMemory14", true);
                        break;
                }
            }
        }
    }

    public void NewMemorySeen(MemoryEnum memory) {
        if (!memoriesSeen.Contains(memory)) {
            memoriesSeen.Add(memory);
            DialogueLua.SetVariable("SawNoMemory", false);

            // Check for specific variable values
            switch (memory) {
                case MemoryEnum.BEN_WALLY_GET_TIPSY:
                    DialogueLua.SetVariable("SawMemory4", true);
                    break;
                case MemoryEnum.BEN_GUN_GOES_MISSING:
                    DialogueLua.SetVariable("SawMemory5", true);
                    break;
                case MemoryEnum.TINA_SINGS_LA_CANTATA:
                    DialogueLua.SetVariable("SawMemory6", true);
                    break;
                case MemoryEnum.KEVIN_HIDES_NERDY_SIDE:
                    DialogueLua.SetVariable("SawMemory10", true);
                    break;
                case MemoryEnum.KEVIN_KNOWS_DETECTIVE:
                    DialogueLua.SetVariable("SawMemory11", true);
                    break;
                case MemoryEnum.WALLY_LOSES_VINNIE_POKER:
                    DialogueLua.SetVariable("SawMemory12", true);
                    break;
                case MemoryEnum.WALLY_SUSPICIOUS_PHONE_CALL:
                    DialogueLua.SetVariable("SawMemory13", true);
                    break;
                case MemoryEnum.VINNIE_YOU_SHOT_VINNIE:
                    DialogueLua.SetVariable("SawMemory14", true);
                    break;
            }
        }
    }

    public bool IsMemorySeen(MemoryEnum memory) {
        return memoriesSeen.Contains(memory);
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

    // Goes back to main menu and allows script to be destroyed (and then created anew in the main menu)
    public void GoToMainMenu() {
        // Becomes destructible on load
        transform.parent = (new GameObject()).transform;

        // Resets controller variable
        controller = null;

        SceneManager.sceneLoaded -= OnSceneLoaded;

        SceneManager.LoadScene((int)UnityScenes.StartMenu);
    }

    IEnumerator WaitThenLoadScene(int scene) {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
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
