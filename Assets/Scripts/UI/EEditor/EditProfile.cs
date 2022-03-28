using System;
using System.Collections;
using System.Collections.Generic;
using ProfileSelectInfoStruct;
using TMPro;
using UnityEngine;

public class EditProfile : MonoBehaviour
{
    private int editingProfileID;
    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private GameObject profileSelect;
    DatabaseManager databasemanager;

    [SerializeField] private GameObject newWeaponUi;
    public GameObject GetNewWeaponUi() => newWeaponUi;
    
    [SerializeField] private GameObject buttonListObject;

    [Header("Button Prefabs")] 
    [SerializeField] private GameObject addNewWeaponButtonPrefab;
    [SerializeField] private GameObject weaponButtonPrefab;

    private void Awake()
    {
        databasemanager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    public void RefreshAll(int editingProfileID)
    {
        this.editingProfileID = editingProfileID;
        title.text = "Editing" + databasemanager.GetProfileNameFromID(editingProfileID);
        
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
    
    public void Button_Weapons()
    {
        ClearCurrentButtons();
        
        Instantiate(addNewWeaponButtonPrefab, buttonListObject.transform, true);
        foreach (WeaponBasicInfo wepInfo in databasemanager.GetListOfAllWeapons())
        {
            GameObject wep = Instantiate(weaponButtonPrefab, buttonListObject.transform, true);
            
            wep.transform.SetSiblingIndex(0);
            wep.GetComponent<WeaponButton>().SetId(wepInfo.weaponID);
            wep.GetComponentInChildren<TextMeshProUGUI>().text = wepInfo.weaponName;
        }
    }

    public void Button_Enemies()
    {
        ClearCurrentButtons();
    }

    public void Button_Items()
    {
        ClearCurrentButtons();
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
