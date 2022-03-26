using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteConfirmation : MonoBehaviour
{
    private DatabaseManager databaseManager;
    private ProfileSelect profileSelect;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        profileSelect = GameObject.FindGameObjectWithTag("ProfileSelect").GetComponent<ProfileSelect>();
    }

    public void Button_Yes()
    {
        var profileId = profileSelect.GetSelectedProfile().profileID;
        
        databaseManager.DeleteProfile(profileId);
        profileSelect.RemoveProfileFromUiList(profileId);

        gameObject.SetActive(false);
    }

    public void Button_No()
    {
        gameObject.SetActive(false);
    }
}
