using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int sceneToSwitchTo;

    // Start is called before the first frame update
    void Start()
    {
        // keep the scene controller between scenes.
        //DontDestroyOnLoad(gameObject);
    }

    public void ReturnToBar()
    {
        SceneManager.LoadScene(1);
    }
}
