using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Graphics : MonoBehaviour
{
    [SerializeField] private Toggle bloomToggle;
    [SerializeField] private Toggle vignetteToggle;


    void Awake()
    {
        DisplayCurrentValues();
    }
    void DisplayCurrentValues()
    {
        bloomToggle.isOn = PlayerPrefs.GetInt(PlayerPrefsNames.bloomSetting) == 1;
        vignetteToggle.isOn = PlayerPrefs.GetInt(PlayerPrefsNames.vignetteSetting) == 1;
    }
    
    
    
    //Value Changed Funcs \/

    public void VignetteChanged()
    {
        int value = vignetteToggle.isOn ? 1 : 0;
        
        PlayerPrefs.SetInt(PlayerPrefsNames.vignetteSetting, value);
        //changed post processing effects
    }
    
    public void BloomChanged()
    {
        int value = bloomToggle.isOn ? 1 : 0;
        
        PlayerPrefs.SetInt(PlayerPrefsNames.bloomSetting, value);
        //changed post processing effects
    }
}
