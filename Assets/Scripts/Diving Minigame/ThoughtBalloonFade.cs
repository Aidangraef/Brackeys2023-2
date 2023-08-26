using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThoughtBalloonFade : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    float fadeSpeed = 2f;

    private void Update() {
        // Fade sprite and text
        Color color = spriteRenderer.color;
        color.a -= fadeSpeed * Time.deltaTime;
        spriteRenderer.color = color;

        color = textComponent.color;
        color.a -= fadeSpeed * 2 * Time.deltaTime;
        textComponent.color = color;

        if (spriteRenderer.color.a <= 0f) {
            // Destroy balloon
            Destroy(gameObject);
        }
    }
}
