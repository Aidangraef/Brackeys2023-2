using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRoll : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    Vector2 startingPosition;

    [SerializeField]
    Vector2 endingPosition;

    [SerializeField]
    float duration = 20f;
    float speed;

    private void Start() {
        rectTransform.anchoredPosition = startingPosition;

        speed = (endingPosition - startingPosition).magnitude / duration;
    }

    void Update()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, endingPosition, Time.deltaTime * speed);
        if (rectTransform.anchoredPosition == endingPosition) {
            // Once it's over call start menu
            StartCoroutine(WaitThenGoToMenu());

            // Start fade effect
            FindObjectOfType<ImageFadeEffect>().TargetAlpha = 1f;
        }
    }

    IEnumerator WaitThenGoToMenu() {
        yield return new WaitForSeconds(1);
        GameController.controller.GoToMainMenu();
    }
}
