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

            SceneManager.sceneLoaded += OnSceneLoaded;
        } else {
            Destroy(gameObject);
        }

        didIJustShootSomeone = false;
        wasThatMe = false;
        barTenderIntro = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
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


    public void NewMemorySeen(MemoryEnum memory) {
        if (!memoriesSeen.Contains(memory)) {
            memoriesSeen.Add(memory);
        }
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

    public void SwitchToBigReveal()
    {
        SceneManager.LoadScene(15);
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
