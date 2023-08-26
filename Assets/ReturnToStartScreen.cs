using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToStartScreen : MonoBehaviour
{
    public void GoBackToStartScreen()
    {
        StartCoroutine(WaitThenGoToMenu());

        // Start fade effect
        FindObjectOfType<ImageFadeEffect>().TargetAlpha = 1f;
    }

    IEnumerator WaitThenGoToMenu() {
        yield return new WaitForSeconds(1);
        GameController.controller.GoToMainMenu();
    }
}
