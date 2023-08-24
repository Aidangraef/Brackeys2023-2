using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeLevel : MonoBehaviour
{
    public Slider thisSlider;
    public float SFXVolume;
    public float musicVolume;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetSpecificVolume(string whatValue)
    {
        float sliderValue = thisSlider.value;

        if (whatValue == "SetBGMVolume")
        {
            Debug.Log("changed BGM volume to :" + thisSlider.value);
            musicVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("musicVolume", musicVolume);
        }
        if (whatValue == "SetSFXVolume")
        {
            Debug.Log("changed SFX volume to:" + thisSlider.value);
            SFXVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("SFXVolume", SFXVolume);
        }
    }
}
