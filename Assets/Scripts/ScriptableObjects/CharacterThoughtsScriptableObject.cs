using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterThoughts", menuName = "ScriptableObjects/CharacterThoughts")]
public class CharacterThoughtsScriptableObject : ScriptableObject
{
    [Serializable]
    public class IrrelevantThought {
        // Maps a thought to a feeling, this way the feeling is what is in the thought balloon
        public string feeling;
        public string thought;
    }

    public CharacterEnum character;

    public List<IrrelevantThought> thoughtsList;
}
