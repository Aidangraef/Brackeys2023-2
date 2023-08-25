using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settings;
    public GameObject background;
    bool isPaused;
    bool isReturningMainMenu = false;

    [SerializeField]
    float fadeToMenuDuration = 2.2f;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        isPaused = false;

        DontDestroyOnLoad(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReturningMainMenu) {
            // Only runs main menu fading stuff
            Image backgroundImage = background.GetComponent<Image>();
            backgroundImage.color = Color.Lerp(backgroundImage.color, Color.black, Time.unscaledDeltaTime / (fadeToMenuDuration/2));
        }

        // Check for inputs otherwise, pausing if there's no dialogue running
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false && !DialogueManager.IsConversationActive)
        {
            pauseMenu.SetActive(true);
            background.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;

        }else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            Resume();
        }
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        background.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void MainMenu() {
        // Disappear menu buttons
        pauseMenu.SetActive(false);

        isReturningMainMenu = true;
        // Fade
        ImageFadeEffect fadeEffect = background.GetComponent<ImageFadeEffect>();
        fadeEffect.UnscaledTime = true;
        fadeEffect.TargetAlpha = 1f;

        StartCoroutine(WaitAndGoMainMenu());
    }

    IEnumerator WaitAndGoMainMenu() {
        yield return new WaitForSecondsRealtime(fadeToMenuDuration);

        // Allow to be destroyed on load
        transform.parent.parent = (new GameObject()).transform;
        Debug.Break();

        // Reset time scale
        Time.timeScale = 1f;

        GameController.controller.GoToMainMenu();
    }
}
