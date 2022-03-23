using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProfileSelectInfoStruct;
using TMPro;

public class ProfileListObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myTitle;
    private ProfileBasicInfo myBasicInfo;
    
    public void SetUp(ProfileBasicInfo myBasicInfo)
    {
        myTitle.text = myBasicInfo.profileName;
        this.myBasicInfo = myBasicInfo;
    }

}
