using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ProfileSelectInfoStruct;

public class ProfileSelect : MonoBehaviour
{
    [SerializeField] private GameObject profileListItemPrefab;
    [SerializeField] private GameObject newProfileTab;
    [Header("Select Profile")]
    [SerializeField] private GameObject profileList;

    [Header("Selected Profile")] 
    [SerializeField] private TextMeshProUGUI selectedTitle;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image selectedImage;
    
    
    private DatabaseManager databaseManager;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    private void FillProfileList()
    {
        
    }
    public void Button_CreateNewProfile()
    {
        gameObject.SetActive(false);
        newProfileTab.SetActive(true);
    }

    public void Button_RemoveProfile()
    {
        
    }

    public void Button_EditProfile()
    {
        
    }
}
