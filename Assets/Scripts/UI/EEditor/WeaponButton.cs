using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    private int id;
    private int editingProfileId;
    private DatabaseManager databaseManager;

    private WeaponPanel wepPanel;

    private EditProfile editProfile;

    private Toggle toggle;
    
    private void Awake()
    {
        wepPanel = GameObject.FindGameObjectWithTag("WeaponPanel").GetComponent<WeaponPanel>();
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        editProfile = GameObject.FindGameObjectWithTag("editProfile").GetComponent<EditProfile>();
        toggle = GetComponentInChildren<Toggle>();
    }


    private void Start()
    {
        editingProfileId = editProfile.getEditingProfileID;
        toggle.isOn = databaseManager.IsThisWeaponEnabled(id, editingProfileId);
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
        databaseManager.EnableDisableAWeapon(editingProfileId, id, toggle.isOn);
    }
}
