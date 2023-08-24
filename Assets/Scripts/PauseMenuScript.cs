using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject settings;
    bool isPuased;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        isPuased = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPuased == false)
        {
            pauseMenu.SetActive(true);
            isPuased = true;
        }else if (Input.GetKeyDown(KeyCode.Escape) && isPuased == true)
        {
            pauseMenu.SetActive(false);
            isPuased = false;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPuased = false;
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

    public void MainMenu()
    {
        SceneManager.LoadScene(7);
    }
}
