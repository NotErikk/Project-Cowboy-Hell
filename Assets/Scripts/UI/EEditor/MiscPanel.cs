using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ProfileSelectInfoStruct;


public class MiscPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputProfileName;
    [SerializeField] private TMP_InputField inputProfileDescription;

    private DatabaseManager databaseManager;
    private ProfileBasicInfo selectedProfile;
    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    public void ShowSettingsFromID(int id)
    {
        selectedProfile = databaseManager.GetProfileBasicInfoFromID(id);
        
        //Profile Settings
        inputProfileName.text = selectedProfile.profileName;
        inputProfileDescription.text = selectedProfile.profileDescription;
    }

    public void Change_Name(TMP_InputField inputField)
    {
        databaseManager.UpdateProfileName(selectedProfile.profileID,inputField.text);
    }

    public void Change_Description(TMP_InputField inputField)
    {
        databaseManager.UpdateProfileDesc(selectedProfile.profileID,inputField.text);
    }
}
