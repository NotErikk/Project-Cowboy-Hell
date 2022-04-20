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
    
    Resolution[] resolutions;
    
    void Awake()
    {
        DisplayCurrentValues();
    }

    void SetUpAndDisplayResolutionDropDown()
    {
        resolutions = Screen.resolutions;
        
        resolutionDropDown.ClearOptions();

        var resolutionStrings = new List<string>();

        int currentValue = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionStrings.Add(resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRate + "Hz");

            if (resolutions[i].width == Screen.currentResolution.width && //set correct inital value
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentValue = i;

            }
        }
        
        resolutionDropDown.AddOptions(resolutionStrings);
        resolutionDropDown.value = currentValue;
        resolutionDropDown.RefreshShownValue();
        
    }
    
    void DisplayCurrentValues()
    {
        SetUpAndDisplayResolutionDropDown();
        
        fullscreenDropDown.value = PlayerPrefs.GetInt(PlayerPrefsNames.fullscreenSetting);
        
        vsyncToggle.isOn = PlayerPrefs.GetInt(PlayerPrefsNames.vsyncSetting) == 1;
    }

    //Value Changed Funcs \/
    public void ResolutionChanged()
    {
        PlayerPrefs.SetInt(PlayerPrefsNames.resolutionSetting, resolutionDropDown.value);

        Resolution resolution = resolutions[resolutionDropDown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void FullScreenChanged()
    {

        switch (fullscreenDropDown.value)
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

        PlayerPrefs.SetInt(PlayerPrefsNames.fullscreenSetting, fullscreenDropDown.value);
    }

    public void VsyncChanged()
    {
        int value = vsyncToggle.isOn ? 1 : 0;
        
        PlayerPrefs.SetInt(PlayerPrefsNames.vsyncSetting, value);
        QualitySettings.vSyncCount = value;
    }
}
