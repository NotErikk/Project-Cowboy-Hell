using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillDBDefault : MonoBehaviour
{
    private DatabaseManager databaseManager;
    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    void Start()
    {
        //if the player has booted the game before do not proceed
        if (PlayerPrefs.GetInt(PlayerPrefsNames.firstTimeBoot) != 0) return;
        
        
        databaseManager.CreateNewProfile("Default", "The Vanilla Game Experience", 0);
        int defaultProfileID = databaseManager.GetLatestProfileId();
        
        databaseManager.CreateNewWeapon("Percussion Revolver", 0, 0, 5, 1, 10, 30, 0.6, 0, 0);
        databaseManager.CreateNewWeapon("Big Iron", 1, 0, 6, 1, 15, 20, 0.4, 0, 0);
        databaseManager.CreateNewWeapon("Single Action Revolver", 2, 0, 6, 1, 20, 10, 0.4, 0, 0);
        databaseManager.CreateNewWeapon("The Number Three", 3, 0, 6, 1, 20, 30, 0.2, 0, 0);
        databaseManager.CreateNewWeapon("German Pistol", 4, 0, 10, 1, 20, 10, 0.06, 0, 0);

        databaseManager.CreateNewItem("+10% Jump Power", 0, "", "+10% Jump Power", "Grants an extra 10% to your jump boost!", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.10, 0, 0, 0, 0, 0, 0);
        databaseManager.CreateNewItem("+10% Revolver Damage", 1, "", "+10% Revolver Damage", "Grants an extra 10% to your revolver damage!", 1, 0, 0, 0, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        databaseManager.CreateNewItem("+10% Revolver Reload Speed", 2, "", "+10% Revolver Reload Speed", "Grants an extra 10% to your revolver reload speed", 1, 0, 0, 0, 0, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        databaseManager.CreateNewItem("+30 Firerate", 3, "", "+30% Firerate", "Grants an extra 30% to your firerate!", 1, 1, 1, 1, 0, 0, 0.3, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        databaseManager.CreateNewItem("Extra Life", 4, "", "+1 Extra Life", "Revives the player on death", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0);

        foreach (var item in databaseManager.GetListOfAllItems())
        {
            databaseManager.ToggleAnItem(defaultProfileID, item.itemID, true);
        }

        foreach (var weapon in databaseManager.GetListOfAllWeapons())
        {
            databaseManager.EnableDisableAWeapon(defaultProfileID, weapon.weaponID, true);
        }
        
        PlayerPrefs.SetInt(PlayerPrefsNames.firstTimeBoot, 1);
    }

}
