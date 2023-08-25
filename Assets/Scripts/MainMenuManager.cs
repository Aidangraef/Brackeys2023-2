using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    float playDelay = 2.5f;

    public GameObject menu;
    public GameObject credits;
    public GameObject settings;

    [SerializeField]
    ImageFadeEffect fadeEffect;

    // Start is called before the first frame update
    void Start() {
        menu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
    }

    int i = 0;
    private void FixedUpdate() {
        i++;
        if (i == 50) {
            i = 0;
            AkSoundEngine.PostEvent("npcTalk", this.gameObject);
        }
    }

    public void Play()
    {
        FindObjectOfType<LogoFlicker>().StartSequence();

        // Disappear buttons
        menu.SetActive(false);

        // Start fade effect
        fadeEffect.FadeSpeed = 1f / playDelay;
        fadeEffect.TargetAlpha = 1f;

        StartCoroutine(WaitAndStartGame());
    }

    IEnumerator WaitAndStartGame() {
        yield return new WaitForSeconds(playDelay);
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Settings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void Back()
    {
        Debug.Log("GOING BACK");
        menu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
