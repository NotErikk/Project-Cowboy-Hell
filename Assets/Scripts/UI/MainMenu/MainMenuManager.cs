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
    [SerializeField] private GameObject quitCanvas;


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
        quitCanvas.SetActive(false);
    }
    public void Button_Continue()
    {
        bool toggle = !continueGameCanvas.activeSelf;
        HideAllCanvases();
        continueGameCanvas.SetActive(toggle);
    }

    public void Button_NewGame()
    {
        bool toggle = !newGameCanvas.activeSelf;
        HideAllCanvases();
        newGameCanvas.SetActive(toggle);
    }

    public void Button_Options()
    {
        bool toggle = !optionsCanvas.activeSelf;
        HideAllCanvases();
        optionsCanvas.SetActive(toggle);
    }

    public void Button_Editor()
    {
        bool toggle = !editorCanvas.activeSelf;
        HideAllCanvases();
        editorCanvas.SetActive(toggle);
    }

    public void Button_Quit()
    {
        bool toggle = !quitCanvas.activeSelf;
        HideAllCanvases();
        quitCanvas.SetActive(toggle);
    }
}
