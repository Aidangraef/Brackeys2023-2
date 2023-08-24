using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameFadeEffect : MonoBehaviour
{
    [SerializeField]
    Image darkScreen;

    float targetAlpha = 0f;
    [SerializeField]
    float fadeSpeed = 2f;

    public float TargetAlpha { get => targetAlpha; set { enabled = true; targetAlpha = value; } }
    public float FadeSpeed { get => fadeSpeed; set => fadeSpeed = value; }

    void Update()
    {
        Color color = darkScreen.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        darkScreen.color = color;

        if (color.a == targetAlpha) {
            MinigameController.controller.FadeEffectOver();
            enabled = false;
        }
    }
}
