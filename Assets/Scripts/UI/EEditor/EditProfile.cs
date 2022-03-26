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

    [SerializeField] private GameObject buttonListObject;
    
    [Header("Button Prefabs")] 
    [SerializeField] private GameObject weaponButtonPrefab;

    private void Awake()
    {
        databasemanager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    public void RefreshAll(int editingProfileID)
    {
        this.editingProfileID = editingProfileID;
        title.text = "Editing " + databasemanager.GetProfileNameFromID(editingProfileID);
    }


    public void Button_Weapons()
    {
        foreach (WeaponBasicInfo wepInfo in databasemanager.GetListOfAllWeapons())
        {
            GameObject wep = Instantiate(weaponButtonPrefab, buttonListObject.transform, true);

            wep.GetComponentInChildren<TextMeshProUGUI>().text = wepInfo.weaponName;
        }
        databasemanager.GetListOfAllWeapons();
    }

    public void Button_Enemies()
    {
        
    }

    public void Button_Items()
    {
        
    }

    public void Button_Gameplay()
    {
        
    }

    public void Button_Misc()
    {
        
    }
    
    public void Button_Back()
    {
        gameObject.SetActive(false);
        profileSelect.SetActive(true);
    }
}
