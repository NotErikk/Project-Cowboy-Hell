using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject newGameCanvas;
    [SerializeField] private GameObject continueGameCanvas;
    [SerializeField] private GameObject editorCanvas;
    [SerializeField] private GameObject optionsCanvas;


    private void Start()
    {
        HideAllCanvases();
    }

    private void HideAllCanvases()
    {
        newGameCanvas.SetActive(false);
        continueGameCanvas.SetActive(false);
        editorCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
    }
    public void Button_Continue()
    {
        
    }

    public void Button_NewGame()
    {
        
    }

    public void Button_Options()
    {
        optionsCanvas.SetActive(!optionsCanvas.activeSelf);
    }

    public void Button_Editor()
    {
        
    }

    public void Button_Quit()
    {
        
    }
}
