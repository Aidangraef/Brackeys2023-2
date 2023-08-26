using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTypes;

[CreateAssetMenu(fileName = "DiveInfo", menuName = "ScriptableObjects/DiveInfo")]
public class DiveInfoScriptableObject : ScriptableObject
{

    public UnityScenes PreviousScene;
    public UnityScenes DivingScene;
}
