using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BasicGameSaveInfoStruct;

public class GameSavePrefab : MonoBehaviour
{
    private ContinueGameCanvas continueGameCanvas;
    private GameSaveInfo mySave;
    public void SetID(GameSaveInfo info) => mySave = info;

    [Header("Ui")] 
    [SerializeField] private TextMeshProUGUI saveName;

    [SerializeField] private TextMeshProUGUI profileName;
    private void Awake()
    {
        continueGameCanvas = GameObject.FindGameObjectWithTag("ContinueCanvas").GetComponent<ContinueGameCanvas>();
    }

    private void Start()
    {
        UpdateUiComponents();
    }

    private void UpdateUiComponents()
    {
        saveName.text = mySave.saveName;
        profileName.text = Convert.ToString(mySave.saveProfileID);
    }
    
    public void Button_GameSaveClicked()
    {
        continueGameCanvas.SetSelectedSave(mySave.saveID);
    }
}
