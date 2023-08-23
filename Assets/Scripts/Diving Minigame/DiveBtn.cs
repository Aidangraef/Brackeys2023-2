using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTypes;

public class DiveBtn : MonoBehaviour
{
    public DiveInfoScriptableObject DiveInfo;

    public void GoToMiniGame() => SceneManager.LoadScene((int)UnityScenes.Minigame);

    public void SetDiveScene(int divingSceneId)
    {
        DiveInfo.PreviousScene = (UnityScenes)SceneManager.GetActiveScene().buildIndex;
        DiveInfo.DivingScene = (UnityScenes)divingSceneId;
    }
}
