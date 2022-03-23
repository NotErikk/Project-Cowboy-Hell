using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProfileSelectInfoStruct
{
    struct ProfileInfoForList
        {
            public string profileName;
            public string profileDescription;
            public Sprite profileImage;

            public ProfileInfoForList(string profileName, string profileDescription)
            {
                this.profileName = profileName;
                this.profileDescription = profileDescription;
                this.profileImage = null;
            }
        }
}