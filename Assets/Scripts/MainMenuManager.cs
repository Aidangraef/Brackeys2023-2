using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public float moveSpeed = 2.0f;   // Adjust the speed of camera movement
    public float leftBound = -10.0f; // Adjust the left boundary
    public float rightBound = 10.0f; // Adjust the right boundary

    [SerializeField]
    float playDelay = 2.5f;

    public GameObject menu;
    public GameObject credits;
    public GameObject settings;

    private bool movingRight = true;

    [SerializeField]
    ImageFadeEffect fadeEffect;

    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(false);
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position based on movement direction
        Vector3 newPosition = transform.position + (movingRight ? Vector3.right : Vector3.left) * moveSpeed * Time.deltaTime;

        // Check if the camera has reached the bounds and reverse direction
        if (newPosition.x >= rightBound)
        {
            movingRight = false;
        }
        else if (newPosition.x <= leftBound)
        {
            movingRight = true;
        }

        // Apply the new position to the camera's transform
        transform.position = newPosition;
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
        menu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        //Debug.Log("Quit");
    }
}
