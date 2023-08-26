using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ReturnToBar()
    {
        StartCoroutine(WaitThenGoToBar());
    }

    public void ReturnToMenu()
    {
        StartCoroutine(WaitThenGoToMenu());

        // Start fade effect
        FindObjectOfType<ImageFadeEffect>().TargetAlpha = 1f;
    }

    IEnumerator WaitThenGoToMenu()
    {
        yield return new WaitForSeconds(1);
        GameController.controller.GoToMainMenu();
    }

    IEnumerator WaitThenGoToBar()
    {
        yield return new WaitForSeconds(3);
        AkSoundEngine.PostEvent("exitMemory", this.gameObject);
        SceneManager.LoadScene(1);
    }
}
