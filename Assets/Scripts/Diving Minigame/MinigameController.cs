using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    [SerializeField]
    float duration;
    [SerializeField]
    float startDelay = 0.7f;

    [SerializeField]
    FadingTMP textElement;

    [SerializeField]
    InputController inputController;

    Coroutine endMinigameCoroutine;

    [SerializeField]
    MinigameFadeEffect fadeEffect;

    bool minigameOver = false;
    bool foundSpecialThought = false;

    MemoryEnum currentMemory;

    [Header("Character attributes")]
    [SerializeField]
    List<Sprite> characterSprites;
    [SerializeField]
    SpriteRenderer characterSpriteRenderer;
    [SerializeField]
    CharacterEnum currentCharacter;

    [SerializeField]
    DiveInfoScriptableObject diso;

    [Header("Tutorial")]
    [SerializeField]
    GameObject tutorial;
    [SerializeField]
    float tutorialDuration = 2f;

    public static MinigameController controller;

    public CharacterEnum CurrentCharacter { get => currentCharacter; set => currentCharacter = value; }
    public MemoryEnum CurrentMemory { get => currentMemory; set => currentMemory = value; }

    private void Awake() {
        if (controller == null) {
            controller = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (tutorial.activeSelf) {
            StartCoroutine(FadeTutorial());
        }
        else {
            fadeEffect.TargetAlpha = 0f;
        }

        // Load current memory dive index
        currentMemory = (MemoryEnum)diso.DivingScene;

        // Load character
        currentCharacter = ConvertSceneIndexToCharacter((int)currentMemory);
        SetCharacter(currentCharacter);
    }

    public void SetCharacter(CharacterEnum character) {
        characterSpriteRenderer.sprite = characterSprites[(int)character];
    }

    IEnumerator FadeTutorial() {
        yield return new WaitForSeconds(tutorialDuration);

        FadingTMP fadingTutorialScript = tutorial.GetComponent<FadingTMP>();
        fadingTutorialScript.enabled = true;
        fadingTutorialScript.IsFading = true;

        // Set screen fading to start minigame
        fadeEffect.TargetAlpha = 0f;
    }

    IEnumerator StartMinigame() {
        yield return new WaitForSeconds(startDelay);

        AkSoundEngine.PostEvent("diveStart", this.gameObject);

        endMinigameCoroutine = StartCoroutine(WaitToEndMinigame());
    }


    IEnumerator WaitToEndMinigame() {
        yield return new WaitForSeconds(duration);

        EndMinigame();
    }

    public void EndMinigame(bool foundSpecialThought = false) {
        this.foundSpecialThought = foundSpecialThought;
        minigameOver = true;
        inputController.enabled = false;

        if (foundSpecialThought) {
            // Didn't end by timeout, so stop coroutine
            if (endMinigameCoroutine != null) {
                StopCoroutine(endMinigameCoroutine);
            }

            // Inform game controller thought was found
            GameController.controller.NewMemorySeen(currentMemory);

            // Play victory sound
            AkSoundEngine.PostEvent("diveSuccess", this.gameObject);
        } else {
            // Play failure sound
            //AkSoundEngine.PostEvent("diveFail", this.gameObject);
        }

        // Fade off
        fadeEffect.TargetAlpha = 1f;
    }

    public void FadeEffectOver() {
        if (minigameOver) {
            // Check which scene to load
            if (foundSpecialThought) {
                // Load memory
                SceneManager.LoadScene(((int)diso.DivingScene));
                AkSoundEngine.PostEvent("memoryStart", this.gameObject);

            } else {
                // Load bar scene
                SceneManager.LoadScene(1);
                AkSoundEngine.PostEvent("exitMemory", this.gameObject);

            }
        } else {
            // Start minigame
            StartCoroutine(StartMinigame());
        }
    }

    public void ShowThoughtText(string thought) {
        textElement.LoadNewText(thought);
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
                return CharacterEnum.WALLY;

            case MemoryEnum.VINNIE_YOU_SHOT_VINNIE:
                return CharacterEnum.VINNIE;

            default:
                Debug.LogError("Character not found!");
                return 0;
        }
    }

    public void ShowTutorial() {
        tutorial.SetActive(true);
    }

    public void PlaySound(string eventSound) {
        AkSoundEngine.PostEvent(eventSound, this.gameObject);
        Debug.Log("Tried to Play:" + eventSound);
    }
}
