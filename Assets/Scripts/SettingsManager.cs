using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{

    //public AudioMixer audioMixer;


    public void SetVolume (float volume)
    {
        //audioMixer.SetFloat("audiomixername", volume);
        Debug.Log(volume);
    }
}
