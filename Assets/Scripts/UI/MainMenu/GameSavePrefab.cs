using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSavePrefab : MonoBehaviour
{
    private ContinueGameCanvas continueGameCanvas;
    private int mySaveID;
    public void SetID(int id) => mySaveID = id;
    private void Awake()
    {
        continueGameCanvas = GameObject.FindGameObjectWithTag("ContinueCanvas").GetComponent<ContinueGameCanvas>();
    }

    public void Button_GameSaveClicked()
    {
        continueGameCanvas.SetSelectedSave(mySaveID);
    }
}
