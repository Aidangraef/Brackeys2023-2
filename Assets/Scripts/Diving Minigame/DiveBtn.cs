using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTypes;

public class DiveBtn : MonoBehaviour
{
    public DiveInfoScriptableObject DiveInfo;

    public void GoToMiniGame() {
        AkSoundEngine.PostEvent("divePlay", this.gameObject);
        GameController.controller.SavePlayerTransform();
        Invoke("GoToMiniGameScene", 3f);

        // Avoid player movement
        FindObjectOfType<CharacterController>().IsDiving = true;

        // Play dive transition
        FindObjectOfType<DiveTransition>().enabled = true;
    }

    public void SetDiveScene(int divingSceneId)
    {
        DiveInfo.PreviousScene = (UnityScenes)SceneManager.GetActiveScene().buildIndex;
        DiveInfo.DivingScene = (UnityScenes)divingSceneId;
    }

    public void GoToMiniGameScene()
    {
        SceneManager.LoadScene((int)(UnityScenes.Minigame));
    }
}
