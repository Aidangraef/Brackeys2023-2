using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadingTMP : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    float fadeSpeed = 2f;

    [SerializeField]
    float delay = 1f;

    bool isFading = false;

    Coroutine delayCoroutine;

    private void Update() {
        if (isFading) {
            Color color = textComponent.color;
            color.a -= fadeSpeed * Time.deltaTime;
            textComponent.color = color;
        }
    }

    public void LoadNewText(string text) {
        textComponent.text = text;

        // Stop fading
        isFading = false;

        // Make alpha 1
        Color color = textComponent.color;
        color.a = 1;
        textComponent.color = color;

        // Stop current coroutine to count delay
        if (delayCoroutine != null) {
            StopCoroutine(delayCoroutine);
        }

        delayCoroutine = StartCoroutine(WaitThenFade());
    }

    IEnumerator WaitThenFade() {
        yield return new WaitForSeconds(delay);

        isFading = true;
    }
}
