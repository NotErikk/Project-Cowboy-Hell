using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditProfile : MonoBehaviour
{
    private int editingProfileID;
    [SerializeField] private TextMeshProUGUI title;
    
    DatabaseManager databasemanager;

    private void Awake()
    {
        databasemanager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    public void RefreshAll(int editingProfileID)
    {
        this.editingProfileID = editingProfileID;

        title.text = "Editing " + databasemanager.GetProfileNameFromID(editingProfileID);
    }
}
