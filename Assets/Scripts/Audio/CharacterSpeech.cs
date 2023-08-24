using Language.Lua;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeech : MonoBehaviour
{
    public static int currentSpeechSpeed = 0;
    public static int charPassed = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
   
    }
    public void OnCharacterTypewriter()
    {
        
        if (charPassed >= currentSpeechSpeed)
            {
                AkSoundEngine.PostEvent("npcTalk", this.gameObject);
                charPassed = 0;
            }
            else
            {
                charPassed += 1;
            }
    }
}
