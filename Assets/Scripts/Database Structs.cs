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
            sprite = null;
        }
    }

    public struct ItemBasicInfo
    {
        public string itemName;
        public int itemID;
        public Texture2D sprite;

        public ItemBasicInfo(string itemName, int itemID)
        {
            this.itemName = itemName;
            this.itemID = itemID;
            sprite = null;
        }
    }
}