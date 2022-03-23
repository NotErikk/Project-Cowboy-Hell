using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProfileSelectInfoStruct;
using TMPro;

public class ProfileListObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myTitle;

    public void SetUp(ProfileInfoForList myInfo)
    {
        myTitle.text = myInfo.profileName;
    }

}
