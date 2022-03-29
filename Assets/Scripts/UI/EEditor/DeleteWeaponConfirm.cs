using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWeaponConfirm : MonoBehaviour
{
    private DatabaseManager databaseManager;
    private int currentID;
    
    public void SetID(int id) => currentID = id;
    
    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    public void Button_DeleteConfirm()
    {
        databaseManager.FullyRemoveWeapon(currentID);
        
        gameObject.SetActive(false);
    }

    public void Button_CancelDelete()
    {
        gameObject.SetActive(false);
    }
}
