using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterThoughts", menuName = "ScriptableObjects/CharacterThoughts")]
public class CharacterThoughtsScriptableObject : ScriptableObject
{
    public CharacterEnum character;

    public List<IrrelevantThought> thoughtsList;
}
