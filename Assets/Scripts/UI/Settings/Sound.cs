using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer masterMixer;

    void Awake()
    {
        DisplayCurrentValues();
    }
    
    
    void DisplayCurrentValues()
    {
        masterSlider.value = PlayerPrefs.GetFloat(PlayerPrefsNames.masterVolumeSetting);
        Debug.Log(masterSlider.value = PlayerPrefs.GetFloat(PlayerPrefsNames.masterVolumeSetting));
        musicSlider.value = PlayerPrefs.GetFloat(PlayerPrefsNames.musicVolumeSetting);
        sfxSlider.value = PlayerPrefs.GetFloat(PlayerPrefsNames.sfxVolumeSetting);
    }
    
    
    
    //Value Changed Funcs \/
    public void MasterChanged()
    {
        masterMixer.SetFloat("volumeOfMaster", masterSlider.value);
        PlayerPrefs.SetFloat(PlayerPrefsNames.masterVolumeSetting, masterSlider.value);
    }

    public void MusicChanged()
    {
        masterMixer.SetFloat("volumeOfMusic", musicSlider.value);
        PlayerPrefs.SetFloat(PlayerPrefsNames.musicVolumeSetting, musicSlider.value);
    }
    
    public void SfxChanged()
    {
        masterMixer.SetFloat("volumeOfSFX", sfxSlider.value);
        PlayerPrefs.SetFloat(PlayerPrefsNames.sfxVolumeSetting, sfxSlider.value);
    }
}
