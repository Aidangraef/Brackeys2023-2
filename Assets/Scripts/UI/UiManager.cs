using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTypes;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ((UnityScenes)scene.buildIndex == UnityScenes.Minigame)
        {
            //GameObject.FindGameObjectWithTag(UnityTags.DialogueManager.ToString()).SetActive(false);
        }
    }
}
