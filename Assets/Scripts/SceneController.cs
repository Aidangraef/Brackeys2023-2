using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        // keep the scene controller between scenes.
        //DontDestroyOnLoad(gameObject);
    }

    public void ReturnToBar()
    {
        StartCoroutine(ChangeScene());
    }

    public void ReturnToMenu()
    {
        StartCoroutine(ChangeScene2());
    }

    IEnumerator ChangeScene2()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3);
        AkSoundEngine.PostEvent("exitMemory", this.gameObject);
        SceneManager.LoadScene(1);
    }
}
