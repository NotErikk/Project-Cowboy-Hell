using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    private int id;
    private DatabaseManager databaseManager;

    private WeaponPanel wepPanel;

    private EditProfile editProfile;
    
    private void Awake()
    {
        wepPanel = GameObject.FindGameObjectWithTag("WeaponPanel").GetComponent<WeaponPanel>();
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        editProfile = GameObject.FindGameObjectWithTag("editProfile").GetComponent<EditProfile>();
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void Button_ShowSettings()
    {
        wepPanel.ShowSettingsFromID(id);
    }

    public void Toggle_ToggleChanged(Toggle toggle)
    {
        databaseManager.EnableDisableAWeapon(editProfile.getEditingProfileID, id, toggle.isOn);
    }
}
