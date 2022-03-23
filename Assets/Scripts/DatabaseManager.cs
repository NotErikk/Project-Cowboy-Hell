using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class DatabaseManager : MonoBehaviour
{
    private static string dbName = "URI=file:PCHDB.db";

    private void Start()
    {
        CreateDB();
    }

    private void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //gameProfiles
                command.CommandText = "CREATE TABLE IF NOT EXISTS gameProfiles (profileID INT , profileName VARCHAR(20), pictureID INT, profileDescription VARCHAR(50), playerHealth INT, playerMovementSpeed DOUBLE, playerJumpForce DOUBLE, playerCrouchMovementSpeed DOUBLE, playerCoyoteTime FLOAT, playerRollLength DOUBLE, playerRollSpeed DOUBLE, playerRollCooldown DOUBLE, playerSlideDeceleration DOUBLE);";
                command.ExecuteNonQuery();
                
                //weapons
                command.CommandText = "CREATE TABLE IF NOT EXISTS weapons (weaponID INT,weaponSprite BLOB, bulletSprite BLOB, casingSprite BLOB, projectileSprite BLOB,displayName VARCHAR(20), firearmClass INT, bulletCapacity INT, projectilesWhenFired INT, projectileSpeed DOUBLE, baseAccuracy DOUBLE, fireRate DOUBLE, ejectCartridgeOnFire INT, gunSmokeOnFire INT, twoHanded INT, reloadAngle DOUBLE, shootingHandHoldingX FLOAT, shootingHandHoldingY FLOAT, firePointX FLOAT, firePointY FLOAT, otherHandHoldingX FLOAT, otherHandHoldingY FLOAT, loadBulletsPointX FLOAT, loadBulletsPointY FLOAT, bulletReleaseKeyX FLOAT, bulletReleaseKeyY FLOAT, cylinderLocationX FLOAT, cylinderLocationY FLOAT);";
                command.ExecuteNonQuery();
                
                //items
                command.CommandText = "CREATE TABLE IF NOT EXISTS items (itemName VARCHAR(20), myTier INT, itemSprite BLOB,itemBriefDescription VARCHAR(50), itemDescription VARCHAR(50), effectRevolvers INT, effectPistols INT, effectShotguns INT, effectRifles INT, extraDamage DOUBLE, reloadSpeedIncrease DOUBLE, fireRate DOUBLE, enableLaserPointer INT, movementSpeedBuff DOUBLE, jumpPowerBuff DOUBLE, rollCooldownDecrease DOUBLE, shopDiscount DOUBLE, damageResistance DOUBLE, dodgeChance DOUBLE, extraLivesToGive DOUBLE, increaseMaxHealth DOUBLE);";
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
}