using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using ProfileSelectInfoStruct;
using AllWeaponInfoStruct;
using Mono.Data.Sqlite;
using AllItemInfoStruct;
using AllGameplayInfoStruct;
using BasicGameSaveInfoStruct;

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
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS gameProfiles (profileID INTEGER PRIMARY KEY AUTOINCREMENT, profileName VARCHAR(20), pictureID INT, profileDescription VARCHAR(50), playerHealth INT, playerMovementSpeed DOUBLE, playerJumpForce DOUBLE, playerCrouchMovementSpeed DOUBLE, playerCoyoteTime FLOAT, playerRollLength DOUBLE, playerRollSpeed DOUBLE, playerRollCooldown DOUBLE, playerSlideDeceleration DOUBLE);";
                command.ExecuteNonQuery();

                //weapons
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS weapons (weaponID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,weaponSprite VARCHAR, bulletSprite VARCHAR, casingSprite VARCHAR, projectileSprite VARCHAR,displayName VARCHAR(20),weaponTier INT ,firearmClass INT, bulletCapacity INT, projectilesWhenFired INT, projectileSpeed DOUBLE, baseAccuracy DOUBLE, fireRate DOUBLE, ejectCartridgeOnFire INT, gunSmokeOnFire INT, twoHanded INT, reloadAngle DOUBLE, shootingHandHoldingX FLOAT, shootingHandHoldingY FLOAT, firePointX FLOAT, firePointY FLOAT, otherHandHoldingX FLOAT, otherHandHoldingY FLOAT, loadBulletsPointX FLOAT, loadBulletsPointY FLOAT, bulletReleaseKeyX FLOAT, bulletReleaseKeyY FLOAT, cylinderLocationX FLOAT, cylinderLocationY FLOAT, shootType INT);";
                command.ExecuteNonQuery();

                //items
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS items (itemID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, itemName VARCHAR(20), myTier INT, itemSprite VARCHAR,itemBriefDescription VARCHAR(50), itemDescription VARCHAR(50), effectRevolvers INT, effectPistols INT, effectShotguns INT, effectRifles INT, extraDamage DOUBLE, reloadSpeedIncrease DOUBLE, fireRate DOUBLE, enableLaserPointer INT, movementSpeedBuff DOUBLE, jumpPowerBuff DOUBLE, rollCooldownDecrease DOUBLE, shopDiscount DOUBLE, damageResistance DOUBLE, dodgeChance DOUBLE, extraLivesToGive DOUBLE, increaseMaxHealth DOUBLE);";
                command.ExecuteNonQuery();

                //gameProfile_weapons
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS gameProfiles_weapons (gameProfilesID INT, weaponsID INT);";
                command.ExecuteNonQuery();

                //gameProfile_items
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS gameProfiles_items (gameProfilesID INT, itemsID INT);";
                command.ExecuteNonQuery();
                
                //gameSaves
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS gameSaves (saveID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, saveName VARCHAR(20), profileID INT, moneyOnPerson INT, moneyInBank INT, currentStoryPoint INT);";
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
                command.CommandText =
                    "INSERT INTO gameProfiles (profileName, pictureID, profileDescription, playerHealth, playerMovementSpeed, playerJumpForce, playerCrouchMovementSpeed, playerCoyoteTime, playerRollLength, playerRollSpeed, playerRollCooldown, playerSlideDeceleration) VALUES (' " +
                    newProfileName + " ', '" + newImageID + "', '" + newProfileDescription +
                    "', 100, 10, 23, 5, 0.2, 0.5, 11, 0.7, 0.3);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void CreateNewWeapon(string weaponName,int weaponTier ,int weaponClass, int bulletCapacity, int projectilesWhenFired,
        double projectileSpeed, double baseAccuracy, double fireRate, int twoHanded, int shootType)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO weapons (weaponSprite, bulletSprite, casingSprite, projectileSprite, displayName, weaponTier,firearmClass, bulletCapacity, projectilesWhenFired, projectileSpeed, baseAccuracy, fireRate, ejectCartridgeOnFire, gunSmokeOnFire, twoHanded, reloadAngle, shootingHandHoldingX, shootingHandHoldingY, firePointX, firePointY, otherHandHoldingX, otherHandHoldingY, loadBulletsPointX, loadBulletsPointY, bulletReleaseKeyX, bulletReleaseKeyY, cylinderLocationX, cylinderLocationY, shootType) " +
                    "VALUES ('weaponSprite' , 'bulletSprite', 'castingSprite', 'projectileSprite', '" + weaponName +
                    "',"+weaponTier+", '" + weaponClass + "', '" + bulletCapacity + "', '" + projectilesWhenFired + "', '" +
                    projectileSpeed + "', '" + baseAccuracy + "', '" + fireRate + "', 0, 0, '" + twoHanded +
                    "', -30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '" + shootType + "');";
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
                        wepList.Add(new WeaponBasicInfo((string) reader["displayName"],
                            Convert.ToInt32(reader["weaponID"])));
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
                command.CommandText = "DELETE FROM gameProfiles WHERE profileID= '" + id + "';";
                command.ExecuteNonQuery();
                
                command.CommandText = "DELETE FROM gameProfiles_weapons WHERE gameProfilesID='" + id + "';";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM gameProfiles_items WHERE gameProfilesID='" + id + "';";
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
                command.CommandText =
                    "SELECT profileID FROM gameProfiles WHERE profileID = (SELECT MAX(profileID) FROM gameProfiles)";
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
                command.CommandText = "SELECT profileName FROM gameProfiles WHERE profileID=" + id + ";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returningProfileName = (string) reader["profileName"];
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
                        infoList.Add(new ProfileBasicInfo(Convert.ToInt32(reader["profileID"]),
                            (string) reader["profileName"], (string) reader["profileDescription"],
                            Convert.ToInt32(reader["pictureID"])));
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
                command.CommandText = "SELECT profileName, profileDescription, pictureID FROM gameProfiles WHERE profileID="+myProfileID+"";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnInfo = new ProfileBasicInfo(myProfileID, (string) reader["profileName"],
                            (string) reader["profileDescription"], Convert.ToInt32(reader["pictureID"]));
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return returnInfo;
    }

    public AllGameplayInfo GetProfileGeneralSettings(int id)
    {
        AllGameplayInfo returnInfo = default;
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT profileID, playerHealth, playerMovementSpeed, playerJumpForce, playerCrouchMovementSpeed, playerCoyoteTime, playerRollLength, playerRollSpeed, playerRollCooldown, playerSlideDeceleration FROM gameProfiles WHERE profileID="+id+";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnInfo = new AllGameplayInfo(Convert.ToInt32(reader["profileID"]),
                            Convert.ToDouble(reader["playerHealth"]),Convert.ToDouble(reader["playerMovementSpeed"]) ,Convert.ToDouble(reader["playerCrouchMovementSpeed"]),
                            Convert.ToDouble(reader["playerJumpForce"]),
                            Convert.ToDouble(reader["playerCoyoteTime"]), Convert.ToDouble(reader["playerRollLength"]),
                            Convert.ToDouble(reader["playerRollSpeed"]), Convert.ToDouble(reader["playerRollCooldown"]),
                            Convert.ToDouble(reader["playerSlideDeceleration"]));
                    }
                    reader.Close();
                }
            }

            connection.Close();
        }

        return returnInfo;
    }

    public void UpdateMaxHealth(int id, double newHealth)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerHealth="+newHealth+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void UpdateMovementSpeed(int id, double newMovementSpeed)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerMovementSpeed="+newMovementSpeed+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateCrouchMovementSpeed(int id, double newCrouchMovementSpeed)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerCrouchMovementSpeed="+newCrouchMovementSpeed+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateJumpForce(int id, double newJumpForce)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerJumpForce="+newJumpForce+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateCoyoteTime(int id, double newCoyoteTime)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerCoyoteTime="+newCoyoteTime+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateRollLength(int id, double newRollLength)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerRollLength="+newRollLength+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateRollSpeed(int id, double newRollSpeed)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerRollSpeed="+newRollSpeed+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateRollCooldown(int id, double newRollCooldown)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerRollCooldown="+newRollCooldown+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateSlideDeceleration(int id, double newDecel)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET playerSlideDeceleration="+newDecel+" WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    

    public AllWeaponInfo GetAllWeaponInfoFromID(int id)
    {
        AllWeaponInfo returnInfo = default;
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM weapons WHERE weaponID=" + id + "";
                using (IDataReader reader = command.ExecuteReader())
                {
                    bool twoHanded = (Convert.ToInt32(reader["twoHanded"]) == 1);
                    returnInfo = new AllWeaponInfo(Convert.ToInt32(reader["weaponID"]), (string) reader["displayName"],Convert.ToInt32(reader["weaponTier"]) ,
                        Convert.ToInt32(reader["bulletCapacity"]), (double) reader["fireRate"], twoHanded,
                        Convert.ToInt32(reader["firearmClass"]), Convert.ToInt32(reader["shootType"]),
                        Convert.ToInt32(reader["projectilesWhenFired"]), (double) reader["projectileSpeed"],
                        (double) reader["baseAccuracy"], (double) reader["reloadAngle"]);

                    reader.Close();
                }
            }

            connection.Close();
        }

        return returnInfo;
    }

    public void FullyRemoveWeapon(int id)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM weapons WHERE weaponID= '" + id + "';";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM gameProfiles_weapons WHERE weaponsID= '" + id + "';";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void EnableDisableAWeapon(int profileId, int weaponId, bool toggle)
    {
        //if turning wep on
        if (toggle)
        {
            using (connection)
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO gameProfiles_weapons (gameProfilesID, weaponsID) VALUES(" +
                                          profileId + ", " + weaponId + ");";
                    command.ExecuteNonQuery();

                }

                connection.Close();
            }

            return;
        }

        //delete weapon in profile
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM gameProfiles_weapons WHERE weaponsID= '" + weaponId +
                                      "' AND gameProfilesID='" + profileId + "';";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public bool IsThisWeaponEnabled(int weaponID, int profileID)
    {
        bool returningBool = false;
        
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT 1 FROM gameProfiles_weapons WHERE gameProfilesID="+profileID+" AND weaponsID="+weaponID+";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returningBool = true;
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return returningBool;
    }

    public void UpdateWeaponName(int id, string newName)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET displayName='"+newName+"' WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateWeaponTier(int id, int newTier)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET weaponTier='"+newTier+"' WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateBulletCapacity(int id, int newCapacity)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET bulletCapacity="+newCapacity+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateFireRate(int id, double newFireRate)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET fireRate="+newFireRate+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateTwoHanded(int id, int newTwoHanded)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET twoHanded="+newTwoHanded+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateClass(int id, int newClass)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET firearmClass="+newClass+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateShotType(int id, int newShotType)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET shootType="+newShotType+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateProjectilesWhenFired(int id, int newProjectilesWhenFired)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET projectilesWhenFired="+newProjectilesWhenFired+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }
    public void UpdateProjectileSpeed(int id, double newProjectileSpeed)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET projectileSpeed="+newProjectileSpeed+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateAccuracy(int id, double newAccuracy)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET baseAccuracy="+newAccuracy+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateReloadAngle(int id, double newReloadAngle)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE weapons SET reloadAngle="+newReloadAngle+" WHERE weaponID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void CreateNewItem(string itemName, int itemTier, string itemSprite, string itemBriefDescription, string itemDescription, int effectRevolvers, int effectPistols, int effectShotguns, int effectRifles, double extraDamage, double reloadSpeedBuff, double fireRateIncrease, int laserPointer, double movementSpeedBuff, double jumpPowerIncrease, double rollCooldownDecrease, double shopDiscount, double damageResistance, double dodgeChance, int extraLivesToGive, double maxHealthIncrease)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO items (itemName, myTier, itemSprite, itemBriefDescription, itemDescription, effectRevolvers, effectPistols,effectShotguns,effectRifles,extraDamage,reloadSpeedIncrease,fireRate,enableLaserPointer,movementSpeedBuff,jumpPowerBuff,rollCooldownDecrease,shopDiscount,damageResistance,dodgeChance,extraLivesToGive,increaseMaxHealth)" +
                "VALUES('"+itemName + "', " + itemTier + ", '" + itemSprite + "', '" + itemBriefDescription + "', '" + itemDescription + "', " + effectRevolvers + ", " + effectPistols + ", " + effectShotguns + ", " + effectRifles + ", " + extraDamage + ", " + reloadSpeedBuff + ", " + fireRateIncrease + ", " + laserPointer + ", " + movementSpeedBuff + ", " + jumpPowerIncrease + ", " + rollCooldownDecrease + ", " + shopDiscount + ", " + damageResistance + ", " + dodgeChance + ", " + extraLivesToGive + ", " + maxHealthIncrease + " );";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void DeleteItem(int id)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM items WHERE itemID= '" + id + "';";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM gameProfiles_items WHERE itemsID= '" + id + "';";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public AllItemInfo GetAllItemInfoFromID(int id)
    {
        AllItemInfo returnInfo = default;
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM items WHERE itemID=" + id + "";
                using (IDataReader reader = command.ExecuteReader())
                {
                    bool effectRevolvers = Convert.ToInt32(reader["effectRevolvers"]) == 1;
                    bool effectPistols = Convert.ToInt32(reader["effectPistols"]) == 1;
                    bool effectShotguns = Convert.ToInt32(reader["effectShotguns"]) == 1;
                    bool effectRifles = Convert.ToInt32(reader["effectRifles"]) == 1;
                    
                    bool laserPointer = Convert.ToInt32(reader["enableLaserPointer"]) == 1;
                    
                    
                    returnInfo = new AllItemInfo(Convert.ToInt32(reader["itemID"]), (string)reader["itemName"], Convert.ToInt32(reader["myTier"]), (string)reader["itemBriefDescription"], (string)reader["itemDescription"], effectRevolvers, effectPistols, effectShotguns, effectRifles, Convert.ToDouble(reader["extraDamage"]),Convert.ToDouble(reader["reloadSpeedIncrease"]), Convert.ToDouble(reader["fireRate"]), laserPointer, Convert.ToDouble(reader["movementSpeedBuff"]),Convert.ToDouble(reader["jumpPowerBuff"]), Convert.ToDouble(reader["rollCooldownDecrease"]), Convert.ToDouble(reader["shopDiscount"]), Convert.ToDouble(reader["damageResistance"]), Convert.ToDouble(reader["dodgeChance"]), Convert.ToInt32(reader["extraLivesToGive"]), Convert.ToDouble(reader["increasemaxHealth"]));

                    reader.Close();
                }
            }
            connection.Close();
        }

        return returnInfo;
    }
    
    public void ToggleAnItem(int profileID, int itemID, bool toggle)
    {
        //if turning item on
        if (toggle)
        {
            using (connection)
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO gameProfiles_items (gameProfilesID, itemsID) VALUES(" + profileID + ", " + itemID + ");";
                    command.ExecuteNonQuery();

                }

                connection.Close();
            }

            return;
        }

        //delete item in profile
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM gameProfiles_items WHERE itemsID= '" + itemID + "' AND gameProfilesID='" + profileID + "';";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public bool IsThisItemEnabled(int itemID, int profileID)
    {
        bool returningBool = false;
        
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT 1 FROM gameProfiles_items WHERE gameProfilesID="+profileID+" AND itemsID="+itemID+";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returningBool = true;
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return returningBool;
    }

    public List<ItemBasicInfo> GetListOfAllItems()
    {
        var itemList = new List<ItemBasicInfo>();

        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT itemID, itemName FROM items";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itemList.Add(new ItemBasicInfo((string) reader["itemName"],Convert.ToInt32(reader["itemID"])));
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }

        return itemList;
    }

    public void UpdateItemName(int id, string newName)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET itemName='"+newName+"' WHERE itemID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateItemTier(int id, int newTier)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET myTier="+newTier+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemBrief(int id, string newBrief)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET itemBriefDescription='"+newBrief+"' WHERE itemID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateItemDesc(int id, string newDesc)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET itemDescription='"+newDesc+"' WHERE itemID="+id+";";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void UpdateItemEffectRevolvers(int id, int toggle)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET effectRevolvers="+toggle+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemEffectPistols(int id, int toggle)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET effectPistols="+toggle+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemEffectShotguns(int id, int toggle)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET effectShotguns="+toggle+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemEffectRifles(int id, int toggle)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET effectRifles="+toggle+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemExtraDmg(int id, double newExtraDmg)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET extraDamage="+newExtraDmg+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemReloadSpeed(int id, double newReloadSpeed)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET reloadSpeedIncrease="+newReloadSpeed+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemFireRate(int id, double newFireRate)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET fireRate="+newFireRate+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemLaserPointer(int id, int toggle)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET enableLaserPointer="+toggle+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemMovementSpeed(int id, double newMovementSpeed)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET movementSpeedBuff="+newMovementSpeed+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemJumpPower(int id, double newJumpPower)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET jumpPowerBuff="+newJumpPower+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemRollCooldown(int id, double newRollCooldown)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET rollCoolDownDecrease="+newRollCooldown+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateShopDiscount(int id, double newDiscount)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET shopDiscount="+newDiscount+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
    
    public void UpdateDamageResistance(int id, double newResistance)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET damageResistance="+newResistance+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
    public void UpdateItemDodgeChance(int id, double newDodgeChance)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET dodgeChance="+newDodgeChance+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemExtraLives(int id, int newLives)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET extraLivesToGive="+newLives+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateItemMaxHealth(int id, double newMaxHealth)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE items SET increaseMaxHealth="+newMaxHealth+" WHERE itemID="+id+";";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateProfileName(int id, string newName)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET profileName='"+newName+"' WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateProfileDesc(int id, string newDesc)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE gameProfiles SET profileDescription='"+newDesc+"' WHERE profileID="+id+";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void CreateNewGameSave(string newGameName, int profileID)
    {
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO gameSaves (saveName, profileID, moneyOnPerson, moneyInBank, currentStoryPoint) VALUES ('"+newGameName+"', "+profileID+", 0,0,0);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public List<GameSaveInfo> GetListOfAllGameSavesBasicInfo()
    {
        List<GameSaveInfo> returnInfo = new List<GameSaveInfo>();
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT saveID, saveName, profileID FROM gameSaves";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnInfo.Add(new GameSaveInfo(Convert.ToInt32(reader["saveID"]), (string)reader["saveName"], Convert.ToInt32(reader["profileID"])));
                    }

                    reader.Close();
                }
            }
            connection.Close();
        }

        return returnInfo;
    }

    public int GetProfileIDFromSaveID(int saveID)
    {
        int profileInt = 0;
        
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT profileID FROM gameSaves WHERE saveID="+saveID+"";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        profileInt = Convert.ToInt32(reader["profileID"]);
                    }

                    reader.Close();
                }
            }
            connection.Close();
        }

        return profileInt;
    }

    public List<AllWeaponInfo> GetAllWeaponInfoFromTierAndProfile(int tier, int profileID)
    {
        var returningList = new List<AllWeaponInfo>();
        List<int> allWepIdsUnderProfile = new List<int>();
        
        //get all wep ids from this profile
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT weaponsID FROM gameProfiles_weapons WHERE gameProfilesID=" + profileID + "";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allWepIdsUnderProfile.Add(Convert.ToInt32(reader["weaponsID"]));
                    }
                    reader.Close();
                }

                foreach (var wepId in allWepIdsUnderProfile)
                {
                    command.CommandText = "SELECT * FROM weapons WHERE weaponID=" + wepId + " AND weaponTier="+tier+"";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool twoHanded = Convert.ToInt32(reader["twoHanded"]) == 1;
                            
                            returningList.Add(new AllWeaponInfo(wepId, (string)reader["displayName"], tier,
                                Convert.ToInt32(reader["bulletCapacity"]), Convert.ToDouble(reader["fireRate"]),
                                twoHanded, Convert.ToInt32(reader["firearmClass"]),
                                Convert.ToInt32(reader["shootType"]), Convert.ToInt32(reader["projectilesWhenFired"]),
                                Convert.ToDouble(reader["projectileSpeed"]), Convert.ToDouble(reader["baseAccuracy"]),
                                Convert.ToDouble(reader["reloadAngle"])));
                        }
                        reader.Close();
                    }
                }

            }

            connection.Close();
        }
        return returningList;
    }

    public List<AllItemInfo> GetAllItemInfoFromTierAndProfileID(int tier, int profileID)
    {
        var returningList = new List<AllItemInfo>();
        List<int> allItemIdsUnderProfile = new List<int>();
        
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT itemsID FROM gameProfiles_items WHERE gameProfilesID=" + profileID + "";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allItemIdsUnderProfile.Add(Convert.ToInt32(reader["itemsID"]));
                    }
                    reader.Close();
                }
                
                foreach (var itemID in allItemIdsUnderProfile)
                {
                    command.CommandText = "SELECT * FROM items WHERE itemID=" + itemID + " AND myTier="+tier+"";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool effectRevolvers = Convert.ToInt32(reader["effectRevolvers"]) == 1;
                            bool effectPistols = Convert.ToInt32(reader["effectPistols"]) == 1;
                            bool effectShotguns = Convert.ToInt32(reader["effectShotguns"]) == 1;
                            bool effectRifles = Convert.ToInt32(reader["effectRifles"]) == 1;
                    
                            bool laserPointer = Convert.ToInt32(reader["enableLaserPointer"]) == 1;
                            
                            returningList.Add(new AllItemInfo(itemID, (string)reader["itemName"], Convert.ToInt32(reader["myTier"]), (string)reader["itemBriefDescription"], (string)reader["itemDescription"], effectRevolvers, effectPistols, effectShotguns, effectRifles, Convert.ToDouble(reader["extraDamage"]), Convert.ToDouble(reader["reloadSpeedIncrease"]), Convert.ToDouble(reader["fireRate"]), laserPointer, Convert.ToDouble(reader["movementSpeedBuff"]), Convert.ToDouble(reader["jumpPowerBuff"]), Convert.ToDouble(reader["rollCooldownDecrease"]), Convert.ToDouble(reader["shopDiscount"]), Convert.ToDouble(reader["damageResistance"]), Convert.ToDouble(reader["dodgeChance"]), Convert.ToInt32(reader["extraLivesToGive"]), Convert.ToDouble(reader["increaseMaxHealth"])));
                        }
                        reader.Close();
                    }
                }

            }

            connection.Close();
        }
        return returningList;
    }

    public int GetLatestSaveId()
    {
        int saveId = 0;
        using (connection)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT saveID FROM gameSaves WHERE saveID = (SELECT MAX(saveID) FROM gameSaves)";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saveId = Convert.ToInt32(reader["saveID"]);
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return saveId;
    }
}

