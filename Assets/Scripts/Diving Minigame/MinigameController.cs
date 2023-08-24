using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameController : MonoBehaviour
{
    [SerializeField]
    float duration;
    [SerializeField]
    float startDelay = 0.7f;

    [SerializeField]
    TextMeshProUGUI textElement;

    [SerializeField]
    InputController inputController;

    Coroutine endMinigameCoroutine;

    [SerializeField]
    MinigameFadeEffect fadeEffect;

    bool minigameOver = false;
    bool foundSpecialThought = false;

    public static MinigameController controller;

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

            // TODO Inform game controller thought was found

            // TODO Play victory sound
        } else {
            // TODO Inform game controller thought was NOT found

            // TODO Play failure sound
        }

        // Fade off
        fadeEffect.TargetAlpha = 1f;
    }

    public void FadeEffectOver() {
        if (minigameOver) {
            // Check which scene to load
            if (foundSpecialThought) {
                // TODO Load memory

            } else {
                // TODO Load bar scene

            }
        } else {
            // Start minigame
            StartCoroutine(StartMinigame());
        }
    }



    public void ShowThoughtText(string thought) {
        textElement.text = thought;
    }
}
