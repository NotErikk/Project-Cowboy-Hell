using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProfileSelectInfoStruct
{
    public struct ProfileInfoForList
        {
            public string profileName;
            public string profileDescription;
            public int profileImageID;

            public ProfileInfoForList(string profileName, string profileDescription, int profileImageID)
            {
                this.profileName = profileName;
                this.profileDescription = profileDescription;
                this.profileImageID = profileImageID;
            }
        }
}