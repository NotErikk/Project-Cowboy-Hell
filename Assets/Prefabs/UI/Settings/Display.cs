using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private TMP_Dropdown fullscreenDropDown;
    [SerializeField] private Toggle vsyncToggle;
    void Start()
    {
        DisplayCurrentValues();
    }

    void DisplayCurrentValues()
    {
        resolutionDropDown.value = PlayerPrefs.GetInt(PlayerPrefsNames.resolutionSetting);
        fullscreenDropDown.value = PlayerPrefs.GetInt(PlayerPrefsNames.fullscreenSetting);

        vsyncToggle.isOn = PlayerPrefs.GetInt(PlayerPrefsNames.vsyncSetting) == 1;
    }
    
    
    
    //Value Changed Funcs \/
    public void ResolutionChanged(TMP_Dropdown newValue)
    {
        int value = newValue.value;
        Debug.Log(value);
    }

    public void FullScreenChanged(TMP_Dropdown newValue)
    {
        
        int value = newValue.value;
        
        switch (value)
        {
            case 0: //FullScreen Window
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break; 
            
            case 1: //Exclusive Fullscreen
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            
            case 2: //Windowed
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            
            default:
                Debug.LogError("Fullscreen Setting Not set up within Display.cs");
                return;
        }

        PlayerPrefs.SetInt(PlayerPrefsNames.fullscreenSetting, value);
    }

    public void VsyncChanged(Toggle newValue)
    {
        int value = newValue.isOn ? 1 : 0;
        
        PlayerPrefs.SetInt(PlayerPrefsNames.vsyncSetting, value);
        QualitySettings.vSyncCount = value;
    }
}
