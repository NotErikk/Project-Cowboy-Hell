using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProfileSelectInfoStruct
{
    public struct ProfileBasicInfo
    {
            public string profileName;
            public int profileID;
            public string profileDescription;
            public int profileImageID;

            public ProfileBasicInfo(int profileID, string profileName, string profileDescription, int profileImageID)
            {
                this.profileID = profileID;
                this.profileName = profileName;
                this.profileDescription = profileDescription;
                this.profileImageID = profileImageID;
            }
    }

    public struct WeaponBasicInfo
    {
        public string weaponName;
        public int weaponID;
        public Texture2D sprite;

        public WeaponBasicInfo(string weaponName, int weaponID)
        {
            this.weaponName = weaponName;
            this.weaponID = weaponID;
            this.sprite = null;
        }
    }
}