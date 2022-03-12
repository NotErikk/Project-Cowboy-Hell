using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private GameObject graphicsPanel;
    [SerializeField] private GameObject keyBindPanel;
    [SerializeField] private GameObject controlsPanel;
    
    
    void Start()
    {
        ButtonDisplayPressed();
    }

    public void ButtonDisplayPressed()
    {
        DisableAllPanels();
        displayPanel.SetActive(true);
    }

    public void ButtonSoundPressed()
    {
        DisableAllPanels();
        soundPanel.SetActive(true);
    }

    public void ButtonGraphicsPressed()
    {
        DisableAllPanels();
        graphicsPanel.SetActive(true);
    }

    public void ButtonKeyBindingsPressed()
    {
        DisableAllPanels();
        keyBindPanel.SetActive(true);
    }

    public void ButtonControlsPressed()
    {
        DisableAllPanels();
        controlsPanel.SetActive(true);
    }

    private void DisableAllPanels()
    {
        displayPanel.SetActive(false);
        soundPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        keyBindPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }
}



