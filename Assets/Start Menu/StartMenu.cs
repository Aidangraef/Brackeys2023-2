using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * =======================================
 *        WELCOME TO THE SANDBOX!
 * =======================================
 * I am using these scripts as starters for
 * common scripts that most games will need
 * (menus, movers, etc). These scripts will
 * each be labeled and commented to explain
 * their use. You're welcome!
 *                            
 *                            - Past Aidan
 */

/*
 * =======================================
 *              START MENU
 * =======================================
 * Use this script to create a start menu!
 * This script requires:
 *  - Nothing! It can sit on any GameObject
 *  
 *  Use buttons to call the menu functions.
 */

public class StartMenu : MonoBehaviour
{

    // Starts the game
    // Adjust build index as needed
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quits the game
    public void Quit()
    {
        Application.Quit();
    }
}
