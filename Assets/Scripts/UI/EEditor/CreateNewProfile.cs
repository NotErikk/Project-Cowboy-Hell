using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewProfile : MonoBehaviour
{
    [SerializeField] private GameObject profileSelectTab;
    [SerializeField] private GameObject pictureSelectTab;
    [SerializeField] private GameObject editProfileTab;
    private EditProfile editProfile;

    [Header("Info Boxes")] 
    [SerializeField] private TMP_InputField profileNameInput;
    [SerializeField] private TMP_InputField profileDescriptionInput;

    private int imageID;

    private DatabaseManager databaseManager;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        editProfile = editProfileTab.GetComponent<EditProfile>();
    }

    public void Button_ImageSelected(int imageID)
    {
        this.imageID = imageID;
        pictureSelectTab.SetActive(false);
    }
    public void Button_BackgroundClicked()
    {
        pictureSelectTab.SetActive(false);
    }
    public void Button_ImageSelect()
    {
        pictureSelectTab.SetActive(true);
    }
    public void Button_Back()
    {
        gameObject.SetActive(false);
        profileSelectTab.SetActive(true);
    }
    public void Button_Create()
    {
        string newName = String.Concat(profileNameInput.text.Where(c => !char.IsWhiteSpace(c)));
        string newDescription = String.Concat(profileDescriptionInput.text.Where(c => !char.IsWhiteSpace(c)));
        
        if (newName != "" && newDescription != "")
        {
            databaseManager.CreateNewProfile(profileNameInput.text, profileDescriptionInput.text, imageID);
            gameObject.SetActive(false);
            editProfileTab.SetActive(true);
            editProfile.RefershAll(databaseManager.GetLatestProfileId());
        }
    }
}
