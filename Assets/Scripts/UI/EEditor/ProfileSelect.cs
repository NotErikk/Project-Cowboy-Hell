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
    [SerializeField] private GameObject profileListGO;

    [Header("Selected Profile")] 
    [SerializeField] private TextMeshProUGUI selectedTitle;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image selectedImage;
    
    
    private DatabaseManager databaseManager;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    private void Start()
    {
        FillProfileList();
    }

    private void FillProfileList()
    {
        var profilesList = databaseManager.GetListOfProfileBasicInfo();

        foreach (ProfileInfoForList profile in profilesList)
        {
            GameObject newListItem = Instantiate(profileListItemPrefab);
            
            newListItem.transform.parent = profileListGO.transform;
            newListItem.transform.SetSiblingIndex(0);
            newListItem.GetComponent<ProfileListObject>().SetUp(profile);
        }
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
