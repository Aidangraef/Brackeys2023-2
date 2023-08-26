using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeLevel : MonoBehaviour
{
    public const string SFX_VOLUME_PREF = "SFXVolume";
    public const string MUSIC_VOLUME_PREF = "musicVolume";
    public const float DEFAULT_VOLUME = 80f;

    [SerializeField]
    public Slider sfxSlider;
    [SerializeField]
    public Slider musicSlider;
    [SerializeField]
    public float sfxVolume;
    [SerializeField]
    public float musicVolume;

    private void Start() {
        float currentSFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_PREF, DEFAULT_VOLUME);
        sfxSlider.value = currentSFXVolume;
        AkSoundEngine.SetRTPCValue(SFX_VOLUME_PREF, currentSFXVolume);

        float currentMusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREF, DEFAULT_VOLUME);
        musicSlider.value = currentMusicVolume;
        AkSoundEngine.SetRTPCValue(MUSIC_VOLUME_PREF, currentMusicVolume);
    }

    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.value;
        PlayerPrefs.SetFloat(SFX_VOLUME_PREF, sfxVolume);
        AkSoundEngine.SetRTPCValue(SFX_VOLUME_PREF, sfxVolume);
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PREF, musicVolume);
        AkSoundEngine.SetRTPCValue(MUSIC_VOLUME_PREF, musicVolume);
    }
}
