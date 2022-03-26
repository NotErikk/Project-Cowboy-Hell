using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProfileSelectInfoStruct;
using TMPro;

public class ProfileListObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myTitle;
    private ProfileBasicInfo myBasicInfo;
    public ProfileBasicInfo GetBasicInfo() => myBasicInfo;
    
    private ProfileSelect profileSelect;

    public int GetProfileId => myBasicInfo.profileID;
    private void Awake()
    {
        profileSelect = GameObject.FindGameObjectWithTag("ProfileSelect").GetComponent<ProfileSelect>();
    }

    public void SetUp(ProfileBasicInfo myBasicInfo)
    {
        myTitle.text = myBasicInfo.profileName;
        this.myBasicInfo = myBasicInfo;
    }

    public void Button_Clicked()
    {
        profileSelect.UpdateSelectedProfile(myBasicInfo);
    }

}
