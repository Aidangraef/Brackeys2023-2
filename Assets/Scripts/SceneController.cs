using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    float barDelay = 2f;

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
        yield return new WaitForSeconds(barDelay);
        // Start fade effect
        // TODO Make sure there's one Fade Effect in a Canvas, like in Scene BenDive1
        FindObjectOfType<ImageFadeEffect>().TargetAlpha = 1f;
        yield return new WaitForSeconds(1);


        AkSoundEngine.PostEvent("exitMemory", this.gameObject);
        SceneManager.LoadScene(1);
    }

    public void SwitchToBigReveal()
    {
        Debug.Log("Got here");
        // Prepare fade out effect
        AkSoundEngine.PostEvent("endingStart", this.gameObject);
        FindObjectOfType<ImageFadeEffect>().TargetAlpha = 1f;
        StartCoroutine(WaitThenLoadScene(15));
    }

    public void SwitchToBadEnding()
    {

        AkSoundEngine.PostEvent("endingStart", this.gameObject);

        // Prepare fade out effect
        FindObjectOfType<ImageFadeEffect>().TargetAlpha = 1f;

        // Check if no memories were seen
        if (GameController.controller.MemoriesSeen.Count == 0)
        {
            // Load good ending but it will be empty
            StartCoroutine(WaitThenLoadScene(15));
        }
        else
        {
            StartCoroutine(WaitThenLoadScene(17));
        }
    }

    IEnumerator WaitThenLoadScene(int scene)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }
}
