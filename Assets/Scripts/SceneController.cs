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
        yield return new WaitForSeconds(2);
        // Start fade effect
        // TODO Make sure there's one Fade Effect in a Canvas, like in Scene BenDive1
        FindObjectOfType<ImageFadeEffect>().TargetAlpha = 1f;
        yield return new WaitForSeconds(1);


        AkSoundEngine.PostEvent("exitMemory", this.gameObject);
        SceneManager.LoadScene(1);
    }
}
