using System;
using System.Collections;
using System.Collections.Generic;
using ProfileSelectInfoStruct;
using TMPro;
using UnityEngine;

public class EditProfile : MonoBehaviour
{
    private int editingProfileID;
    public int getEditingProfileID => editingProfileID;
    
    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private GameObject profileSelect;
    DatabaseManager databaseManager;

    [SerializeField] private GameObject newWeaponUi;
    public GameObject GetNewWeaponUi() => newWeaponUi;
    
    [SerializeField] private GameObject buttonListObject;

    [Header("Settings UIs")]
    [SerializeField] private GameObject weaponsSettingsUi;

    private WeaponPanel wepPanel;

    [SerializeField] private GameObject itemsSettingsUi;
    private ItemPanel itemPanel;
    
    
    [Header("Button Prefabs")] 
    [SerializeField] private GameObject addNewWeaponButtonPrefab;
    [SerializeField] private GameObject weaponButtonPrefab;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        
        wepPanel = weaponsSettingsUi.GetComponentInChildren<WeaponPanel>();
        itemPanel = itemsSettingsUi.GetComponentInChildren<ItemPanel>();
    }

    public void RefreshAll(int editingProfileID)
    {
        this.editingProfileID = editingProfileID;
        title.text = "Editing" + databaseManager.GetProfileNameFromID(editingProfileID);
        
        Button_Weapons();
    }
    
    private void ClearCurrentButtons()
    {
        foreach (var listItem in buttonListObject.GetComponentsInChildren<Transform>())
        {
            if (listItem.gameObject == buttonListObject) continue;
            
            Destroy(listItem.gameObject);
        }
    }

    private void ClearAllUiPanels()
    {
        weaponsSettingsUi.SetActive(false);
        itemsSettingsUi.SetActive(false);
    }
    
    public void Button_Weapons()
    {
        ClearAllUiPanels();
        ClearCurrentButtons();
        
        weaponsSettingsUi.SetActive(true);
        var listOfAllWeps = databaseManager.GetListOfAllWeapons();
        
        
        Instantiate(addNewWeaponButtonPrefab, buttonListObject.transform, true);
        foreach (WeaponBasicInfo wepInfo in listOfAllWeps)
        {
            GameObject wep = Instantiate(weaponButtonPrefab, buttonListObject.transform, true);
            
            wep.transform.SetSiblingIndex(0);
            wep.GetComponent<WeaponButton>().SetId(wepInfo.weaponID);
            wep.GetComponentInChildren<TextMeshProUGUI>().text = wepInfo.weaponName;
        }
        wepPanel.ShowSettingsFromID(listOfAllWeps[listOfAllWeps.Count - 1].weaponID);
        
    }

    public void Button_Enemies()
    {
        ClearCurrentButtons();
    }

    public void Button_Items()
    {
        ClearAllUiPanels();
        ClearCurrentButtons();
        
        itemsSettingsUi.SetActive(true);

        
    }

    public void Button_Gameplay()
    {
        ClearCurrentButtons();
    }

    public void Button_Misc()
    {
        ClearCurrentButtons();
    }
    
    public void Button_Back()
    {
        gameObject.SetActive(false);
        profileSelect.SetActive(true);
    }
}
