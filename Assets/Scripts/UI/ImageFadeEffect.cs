using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeEffect : MonoBehaviour {
    Image image;

    [SerializeField]
    float targetAlpha = 0f;
    [SerializeField]
    float fadeSpeed = 2f;

    public float TargetAlpha { get => targetAlpha; set { enabled = true; targetAlpha = value; } }
    public float FadeSpeed { get => fadeSpeed; set => fadeSpeed = value; }

    private void Awake() {
        image = GetComponent<Image>();
    }

    void Update() {
        Color color = image.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        image.color = color;

        if (color.a == targetAlpha) {
            enabled = false;
        }
    }
}
