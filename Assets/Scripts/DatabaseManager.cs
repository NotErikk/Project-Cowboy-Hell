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
                command.CommandText = "CREATE TABLE IF NOT EXISTS weapons (weaponID INT, displayName VARCHAR(20), firearmClass INT, bulletCapacity INT, projectilesWhenFired INT, projectileSpeed DOUBLE, baseAccuracy DOUBLE, fireRate DOUBLE, ejectCartridgeOnFire INT, gunSmokeOnFire INT, twoHanded INT, reloadAngle DOUBLE, shootingHandHoldingX FLOAT, shootingHandHoldingY FLOAT, firePointX FLOAT, firePointY FLOAT, otherHandHoldingX FLOAT, otherHandHoldingY FLOAT, loadBulletsPointX FLOAT, loadBulletsPointY FLOAT, bulletReleaseKeyX FLOAT, bulletReleaseKeyY FLOAT, cylinderLocationX FLOAT, cylinderLocationY FLOAT);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}