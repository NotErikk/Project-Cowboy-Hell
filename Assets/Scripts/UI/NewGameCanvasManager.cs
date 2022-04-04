using System;
using System.Collections;
using System.Collections.Generic;
using ProfileSelectInfoStruct;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameCanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown profilesDropdown;
    [SerializeField] private TMP_InputField profileName;

    private DatabaseManager databaseManager;
    List<String> allProfileNames;
    private List<ProfileBasicInfo> allProfileBasicInfo;

    public void FillProfileList()
    {
        if (databaseManager == null) databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        
        
        profilesDropdown.ClearOptions();
        
        allProfileNames = new List<string>();
        
        allProfileBasicInfo = new List<ProfileBasicInfo>();
        allProfileBasicInfo.AddRange(databaseManager.GetListOfProfileBasicInfo());
        
        foreach (ProfileBasicInfo profile in allProfileBasicInfo)
        {
            allProfileNames.Add(profile.profileName);
        }
        
        profilesDropdown.AddOptions(allProfileNames);
        
    }

    public void Button_NewGameStart()
    {
        int value = profilesDropdown.value;
        Debug.Log(value + " Which means " + allProfileBasicInfo[value].profileName);

        //try
        //{
            databaseManager.CreateNewGameSave(profileName.text, allProfileBasicInfo[value].profileID);
        //}
        //catch
        //{
            return;
        //}
        SceneManager.LoadScene(1);
    }
}