using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    ImageFadeEffect fadeEffect;

    public void MoveCameraX(float x)
    {
        Camera.main.transform.position = new Vector3(x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
    public void MoveCameraY(float y)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, y, Camera.main.transform.position.z);
    }

    public void GoToCredits()
    {
        StartCoroutine(RollCredits());
    }

    IEnumerator RollCredits()
    {
        yield return new WaitForSeconds(3);

        // Fade screen
        fadeEffect.TargetAlpha = 1f;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(16);
    }
}
