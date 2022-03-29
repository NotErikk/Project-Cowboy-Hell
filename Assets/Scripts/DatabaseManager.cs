using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using ProfileSelectInfoStruct;
using AllWeaponInfoStruct;
using Mono.Data.Sqlite;
using UnityEngine.UI;


public class DatabaseManager : MonoBehaviour
{
    private static string dbName;
    private SqliteConnection connection;

    private void Awake()
    {
        dbName = "URI=file:" + Application.persistentDataPath + "/PCHDB";
        connection = new SqliteConnection(dbName);
        CreateDB();
    }

    private void CreateDB()
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //gameProfiles
                command.CommandText = "CREATE TABLE IF NOT EXISTS gameProfiles (profileID INTEGER PRIMARY KEY AUTOINCREMENT, profileName VARCHAR(20), pictureID INT, profileDescription VARCHAR(50), playerHealth INT, playerMovementSpeed DOUBLE, playerJumpForce DOUBLE, playerCrouchMovementSpeed DOUBLE, playerCoyoteTime FLOAT, playerRollLength DOUBLE, playerRollSpeed DOUBLE, playerRollCooldown DOUBLE, playerSlideDeceleration DOUBLE);";
                command.ExecuteNonQuery();
                
                //weapons
                command.CommandText = "CREATE TABLE IF NOT EXISTS weapons (weaponID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,weaponSprite VARCHAR, bulletSprite VARCHAR, casingSprite VARCHAR, projectileSprite VARCHAR,displayName VARCHAR(20), firearmClass INT, bulletCapacity INT, projectilesWhenFired INT, projectileSpeed DOUBLE, baseAccuracy DOUBLE, fireRate DOUBLE, ejectCartridgeOnFire INT, gunSmokeOnFire INT, twoHanded INT, reloadAngle DOUBLE, shootingHandHoldingX FLOAT, shootingHandHoldingY FLOAT, firePointX FLOAT, firePointY FLOAT, otherHandHoldingX FLOAT, otherHandHoldingY FLOAT, loadBulletsPointX FLOAT, loadBulletsPointY FLOAT, bulletReleaseKeyX FLOAT, bulletReleaseKeyY FLOAT, cylinderLocationX FLOAT, cylinderLocationY FLOAT, shootType INT);";
                command.ExecuteNonQuery();
                
                //items
                command.CommandText = "CREATE TABLE IF NOT EXISTS items (itemID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, itemName VARCHAR(20), myTier INT, itemSprite VARCHAR,itemBriefDescription VARCHAR(50), itemDescription VARCHAR(50), effectRevolvers INT, effectPistols INT, effectShotguns INT, effectRifles INT, extraDamage DOUBLE, reloadSpeedIncrease DOUBLE, fireRate DOUBLE, enableLaserPointer INT, movementSpeedBuff DOUBLE, jumpPowerBuff DOUBLE, rollCooldownDecrease DOUBLE, shopDiscount DOUBLE, damageResistance DOUBLE, dodgeChance DOUBLE, extraLivesToGive DOUBLE, increaseMaxHealth DOUBLE);";
                command.ExecuteNonQuery();
                
                //gameProfile_weapons
                command.CommandText = "CREATE TABLE IF NOT EXISTS gameProfiles_weapons (gameProfilesID INT, weaponsID INT);";
                command.ExecuteNonQuery();
                
                //gameProfile_items
                command.CommandText = "CREATE TABLE IF NOT EXISTS gameProfiles_items (gameProfilesID INT, itemsID INT);";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }
    
    public void CreateNewProfile(string newProfileName, string newProfileDescription, int newImageID)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO gameProfiles (profileName, pictureID, profileDescription, playerHealth, playerMovementSpeed, playerJumpForce, playerCrouchMovementSpeed, playerCoyoteTime, playerRollLength, playerRollSpeed, playerRollCooldown, playerSlideDeceleration) VALUES (' "+newProfileName+" ', '"+newImageID+"', '"+newProfileDescription+"', 100, 10, 23, 5, 0.2, 0.5, 11, 0.7, 0.3);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void CreateNewWeapon(string weaponName, int weaponClass, int bulletCapacity, int projectilesWhenFired, double projectileSpeed, double baseAccuracy, double fireRate, int twoHanded, int shootType)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO weapons (weaponSprite, bulletSprite, casingSprite, projectileSprite, displayName, firearmClass, bulletCapacity, projectilesWhenFired, projectileSpeed, baseAccuracy, fireRate, ejectCartridgeOnFire, gunSmokeOnFire, twoHanded, reloadAngle, shootingHandHoldingX, shootingHandHoldingY, firePointX, firePointY, otherHandHoldingX, otherHandHoldingY, loadBulletsPointX, loadBulletsPointY, bulletReleaseKeyX, bulletReleaseKeyY, cylinderLocationX, cylinderLocationY, shootType) "+
                                       "VALUES ('weaponSprite' , 'bulletSprite', 'castingSprite', 'projectileSprite', '"+weaponName+"', '"+weaponClass+"', '"+bulletCapacity+"', '"+projectilesWhenFired+"', '"+projectileSpeed+"', '"+baseAccuracy+"', '"+fireRate+"', 0, 0, '"+twoHanded+"', -30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '"+shootType+"');";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public List<WeaponBasicInfo> GetListOfAllWeapons()
    {
        var wepList = new List<WeaponBasicInfo>();
        
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT weaponID, displayName FROM weapons";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        wepList.Add(new WeaponBasicInfo((string)reader["displayName"],Convert.ToInt32(reader["weaponID"])));
                    }

                    reader.Close();
                }
            }
            connection.Close();
        }

        return wepList;
    }
    
    public void DeleteProfile(int id)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM gameProfiles WHERE profileID= '"+id+"';";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        
        
    }

    public int GetLatestProfileId()
    {
        int profileID = 0;
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT profileID FROM gameProfiles WHERE profileID = (SELECT MAX(profileID) FROM gameProfiles)";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        profileID = Convert.ToInt32(reader["profileID"]);
                    }

                    reader.Close();
                }
            }
            connection.Close();
        }

        return profileID;
    }

    public string GetProfileNameFromID(int id)
    {
        string returningProfileName = "";
        
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT profileName FROM gameProfiles WHERE profileID="+id+";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returningProfileName = (string)reader["profileName"];
                    }

                    reader.Close();
                }
            }
            connection.Close();
        }

        return returningProfileName;
    }
    
     public List<ProfileBasicInfo> GetListOfProfileBasicInfo()
    {
        var infoList = new List<ProfileBasicInfo>();
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT profileID, profileName, profileDescription, pictureID FROM gameProfiles";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        infoList.Add(new ProfileBasicInfo(Convert.ToInt32(reader["profileID"]), (string)reader["profileName"], (string)reader["profileDescription"], Convert.ToInt32(reader["pictureID"])));
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
        return infoList;
    }

     public ProfileBasicInfo GetProfileBasicInfoFromID(int myProfileID)
     {
         ProfileBasicInfo returnInfo = default;
         using (connection)
         {
             connection.Open();

             using (var command = connection.CreateCommand())
             {
                 command.CommandText = "SELECT profileName, profileDescription, pictureID FROM gameProfiles";
                 using (IDataReader reader = command.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         returnInfo = new ProfileBasicInfo(myProfileID, (string)reader["profileName"], (string)reader["profileDescription"], Convert.ToInt32(reader["pictureID"]));
                     }
                     reader.Close();
                 }
             }
             connection.Close();
         }
         
         return returnInfo;
     }
     
     public AllWeaponInfo GetAllWeaponInfoFromID(int id)
     {
         AllWeaponInfo returnInfo = default;
         using (connection)
         {
             connection.Open();

             using (var command = connection.CreateCommand())
             {
                 command.CommandText = "SELECT * FROM weapons WHERE weaponID="+id+"";
                 using (IDataReader reader = command.ExecuteReader())
                 {
                     bool twoHanded = (Convert.ToInt32(reader["twoHanded"]) == 1);
                     returnInfo = new AllWeaponInfo(Convert.ToInt32(reader["weaponID"]), (string)reader["displayName"], Convert.ToInt32(reader["bulletCapacity"]), (double)reader["fireRate"], twoHanded, Convert.ToInt32(reader["firearmClass"]), Convert.ToInt32(reader["shootType"]), Convert.ToInt32(reader["projectilesWhenFired"]),(double)reader["projectileSpeed"], (double)reader["baseAccuracy"], (double)reader["reloadAngle"]);
                        
                     reader.Close();
                 }
             }
             connection.Close();
         }

         return returnInfo;
     }

     public void FullyRemoveWeapon(int id)
     {
         Debug.Log("removing wep " + id);
     }
     
     public void EnableDisableAWeapon(int profileId, int weaponId, bool toggle)
     {
         //if turning wep on
         if (toggle)
         {
             
         }
     }

}