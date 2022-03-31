using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private int id;
    private int editingProfileId;
    private DatabaseManager databaseManager;

    private ItemPanel itemPanel;

    private EditProfile editProfile;

    private Toggle toggle;
    
    private void Awake()
    {
        itemPanel = GameObject.FindGameObjectWithTag("WeaponPanel").GetComponent<ItemPanel>();
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
        itemPanel.ShowSettingsFromID(id);
    }

    public void Toggle_ToggleChanged(Toggle toggle)
    {
        databaseManager.ToggleAnItem(editingProfileId, id, toggle.isOn);
    }
}
