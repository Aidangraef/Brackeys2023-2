using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiveButtonController : MonoBehaviour
{
    public GameObject diveButton;
    public int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        diveButton.SetActive(false);
    }

    public void ShowDiveButton()
    {
        diveButton.SetActive(true);
    }

    public void HideDiveButton()
    {
        diveButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMemoryDiveScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetSceneIndex(int si)
    {
        sceneIndex = si;
    }
}
