using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTypes;

public class DiveButtonController : MonoBehaviour
{
    public GameObject diveButton;
    public int sceneIndex;

    private GameObject _diveButtonGo;

    public void ShowDiveButton()
    {
        if (_diveButtonGo != null)
        {
            Destroy(_diveButtonGo);
        }
        _diveButtonGo = Instantiate(diveButton,
                new Vector3(0, 0, 0),
                Quaternion.identity); 
        _diveButtonGo.transform.SetParent(transform, false);
    }

    public void HideDiveButton()
    {
        if (_diveButtonGo != null)
        {
            Destroy(_diveButtonGo);
        }
    }

    public void SetSceneIndex(int si)
    {
        sceneIndex = si;
    }
}
