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

    void Start()
    {
        StartCoroutine(StartMinigame());
    }

    IEnumerator StartMinigame() {
        yield return new WaitForSeconds(startDelay);

        StartCoroutine(EndMinigame());

    }


    IEnumerator EndMinigame() {
        yield return new WaitForSeconds(duration);

        inputController.enabled = false;

        // TODO Disappear balloons
    }

    public void ShowThoughtText(string thought) {
        textElement.text = thought;
    }
}
