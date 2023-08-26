using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveTransition : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;
    float currentPace = 0f;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    AnimationCurve curve;

    Vector3 baseScale;

    private void Start() {
        baseScale = transform.localScale;
    }

    private void Update() {
        currentPace = Mathf.MoveTowards(currentPace, 1f, Time.deltaTime * speed);
        float curValue = curve.Evaluate(currentPace)*2;
        if (curValue <= 1f) {
            spriteRenderer.color = Color.Lerp(Color.clear, Color.white, curValue);
        } else {
            spriteRenderer.color = Color.Lerp(Color.white, Color.black, curValue - 1f);
            transform.localScale = baseScale * curValue;
        }
    }
}
