using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllGameplayInfoStruct;
using Cinemachine;

public class GetInfoFromDB : MonoBehaviour
{
    private PlayerMovementScript playerMovementScript;
    private PlayerHealth playerHealth;
    private DatabaseManager databaseManager;
    private LevelManager levelManager;


    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        playerMovementScript = gameObject.GetComponent<PlayerMovementScript>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }


    private void Start()
    {
        AllGameplayInfo myInfo;
        myInfo = databaseManager.GetProfileGeneralSettings(levelManager.GetCurrentProfileID());
        
        playerHealth.SetHealth(myInfo.profMaxHealth);
        
        playerMovementScript.MovementSpeed = (float)myInfo.profMovementSpeed;
        playerMovementScript.JumpForce = (float)myInfo.profJumpForce;
        playerMovementScript.CrouchMovementSpeed = (float)myInfo.profCrouchMovementSpeed;
        playerMovementScript.CoyoteTime = (float) myInfo.profCoyoteTime;
        playerMovementScript.RollTime = (float) myInfo.profRollLength;
        playerMovementScript.RollCooldown = (float) myInfo.profRollCooldown;
        playerMovementScript.SlideDecelleration = (float) myInfo.profSlideDeceleration;
    }
}
