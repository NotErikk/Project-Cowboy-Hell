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
    [SerializeField] private GameObject selectedUi;
    [SerializeField] private TextMeshProUGUI selectedTitle;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image selectedImage;


    private ProfileBasicInfo selectedProfile;
    public ProfileBasicInfo GetSelectedProfile() => selectedProfile;

    [Header("Edit Profile")] 
    [SerializeField] private GameObject editProfileGO;
    private EditProfile editProfile;

    [Header("Remove Profile")] 
    [SerializeField] private GameObject confirmationBox;
    [SerializeField] private TextMeshProUGUI confirmationTxt;
    [SerializeField] private string confirmationPreStatement;
    
    
    private DatabaseManager databaseManager;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        editProfile = editProfileGO.GetComponent<EditProfile>();
    }

    private void Start()
    {
        FillProfileList();
    }

    private void FillProfileList()
    {
        var profilesList = databaseManager.GetListOfProfileBasicInfo();

        foreach (ProfileBasicInfo profile in profilesList)
        {
            GameObject newListItem = Instantiate(profileListItemPrefab, profileListGO.transform, true);

            newListItem.transform.SetSiblingIndex(0);
            newListItem.GetComponent<ProfileListObject>().SetUp(profile);
        }

        if (profilesList.Count <= 0)
        {
            selectedUi.SetActive(false);
            return;
        }
        
        UpdateSelectedProfile(profilesList[profilesList.Count - 1]);
        selectedProfile = databaseManager.GetProfileBasicInfoFromID(profilesList.Count - 1);
    }

    public void AddToProfileList(ProfileBasicInfo profileBasic)
    {
        GameObject newListItem = Instantiate(profileListItemPrefab, profileListGO.transform, true);

        newListItem.transform.SetSiblingIndex(0);
        newListItem.GetComponent<ProfileListObject>().SetUp(profileBasic);
    }

    public void UpdateSelectedProfile(ProfileBasicInfo info)
    {
        selectedUi.SetActive(true);
        
        selectedProfile = info;
        
        selectedTitle.text = info.profileName;
        descriptionText.text = info.profileDescription;
    }

    public void RemoveProfileFromUiList(int id)
    {
        var listOfItems = new List<GameObject>();
        listOfItems.AddRange(GameObject.FindGameObjectsWithTag("ProfileUiItem"));
        
        //remove item
        for (int i = 0; i < listOfItems.Count; i ++)
        {
            if (listOfItems[i].GetComponent<ProfileListObject>().GetProfileId != id) continue;
            
            
            Destroy(listOfItems[i]);
            listOfItems.Remove(listOfItems[i]);
            break;
        }
        
        //if item empty remove the selected profile ui otherwise set new selected profile

        if (listOfItems.Count <= 0)
        {
            selectedUi.SetActive(false);
        }
        else
        {
            UpdateSelectedProfile(listOfItems[0].GetComponent<ProfileListObject>().GetBasicInfo());
        }
    }
    
    public void Button_CreateNewProfile()
    {
        gameObject.SetActive(false);
        newProfileTab.SetActive(true);
    }

    public void Button_RemoveProfile()
    {
        confirmationBox.SetActive(true);
        confirmationTxt.text = confirmationPreStatement + selectedProfile.profileName + "?";
    }

    public void Button_EditProfile()
    {
        editProfileGO.SetActive(true);
        gameObject.SetActive(false);
        
        editProfile.RefreshAll(selectedProfile.profileID);
    }
}
