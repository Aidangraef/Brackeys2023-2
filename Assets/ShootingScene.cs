using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingScene : MonoBehaviour
{
    GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gc.didIJustShootSomeone = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBackToBar()
    {
        SceneManager.LoadScene(1);
    }
}