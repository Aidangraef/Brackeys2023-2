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

    public static MinigameController controller;

    public DiveInfoScriptableObject diso;

    private void Awake() {
        if (controller == null) {
            controller = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        fadeEffect.TargetAlpha = 0f;

        // TODO Load current memory dive index
    }

    public void SetCharacter(CharacterEnum character) {
        characterSpriteRenderer.sprite = characterSprites[(int)character];
    }

    IEnumerator StartMinigame() {
        yield return new WaitForSeconds(startDelay);

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
            StopCoroutine(endMinigameCoroutine);

            // Inform game controller thought was found
            GameController.controller.NewMemorySeen(currentMemory);

            // TODO Play victory sound

            SceneManager.LoadScene(((int)diso.DivingScene));
        } else {
            // TODO Play failure sound
        }

        // Fade off
        fadeEffect.TargetAlpha = 1f;
    }

    public void FadeEffectOver() {
        if (minigameOver) {
            // Check which scene to load
            if (foundSpecialThought) {
                // Load memory
                SceneManager.LoadScene(3);

            } else {
                // Load bar scene
                SceneManager.LoadScene(1);

            }
        } else {
            // Start minigame
            StartCoroutine(StartMinigame());
        }
    }

    public void ShowThoughtText(string thought) {
        textElement.LoadNewText(thought);
    }
}
