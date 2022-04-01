using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AllGameplayInfoStruct;

namespace AllGameplayInfoStruct
{
    public struct AllGameplayInfo
    {
        public int profileID;
        
        public double profMaxHealth;
        public double profMovementSpeed;
        public double profCrouchMovementSpeed;
        public double profJumpForce;
        public double profCoyoteTime;
        public double profRollLength;
        public double profRollSpeed;
        public double profRollCooldown;
        public double profSlideDeceleration;

        public AllGameplayInfo(int profileID, double profMaxHealth, double profMovementSpeed, double profCrouchMovementSpeed, double profJumpForce, double profCoyoteTime, double profRollLength, double profRollSpeed, double profRollCooldown, double profSlideDeceleration)
        {
            this.profileID = profileID;
            
            this.profMaxHealth = profMaxHealth;
            this.profMovementSpeed = profMovementSpeed;
            this.profCrouchMovementSpeed = profCrouchMovementSpeed;
            this.profJumpForce = profJumpForce;
            this.profCoyoteTime = profCoyoteTime;
            this.profRollLength = profRollLength;
            this.profRollSpeed = profRollSpeed;
            this.profRollCooldown = profRollCooldown;
            this.profSlideDeceleration = profSlideDeceleration;
        }
    }
}

public class GameplayPanel : MonoBehaviour
{
    [Header("PlayerSettings")] 
    [SerializeField] private TMP_InputField inputMaxHealth;
    [SerializeField] private TMP_InputField inputMovementSpeed;
    [SerializeField] private TMP_InputField inputCrouchMovementSpeed;
    [SerializeField] private TMP_InputField inputJumpForce;
    [SerializeField] private TMP_InputField inputCoyoteTime;
    [SerializeField] private TMP_InputField inputRollLength;
    [SerializeField] private TMP_InputField inputRollSpeed;
    [SerializeField] private TMP_InputField inputRollCooldown;
    [SerializeField] private TMP_InputField inputSlideDecel;

    private DatabaseManager databaseManager;
    private AllGameplayInfo currentProfileInfo;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    public void ShowSettingsFromID(int id)
    {
        currentProfileInfo = databaseManager.GetProfileGeneralSettings(id);

        //player settings
        inputMaxHealth.text = Convert.ToString(currentProfileInfo.profMaxHealth);
        inputMovementSpeed.text = Convert.ToString(currentProfileInfo.profMovementSpeed);
        inputCrouchMovementSpeed.text = Convert.ToString(currentProfileInfo.profCrouchMovementSpeed);
        inputJumpForce.text = Convert.ToString(currentProfileInfo.profJumpForce);
        inputCoyoteTime.text = Convert.ToString(currentProfileInfo.profCoyoteTime);
        inputRollLength.text = Convert.ToString(currentProfileInfo.profRollLength);
        inputRollSpeed.text = Convert.ToString(currentProfileInfo.profRollSpeed);
        inputRollCooldown.text = Convert.ToString(currentProfileInfo.profRollCooldown);
        inputSlideDecel.text = Convert.ToString(currentProfileInfo.profSlideDeceleration);
    }

    public void Change_Health(TMP_InputField inputField)
    {
        databaseManager.UpdateMaxHealth(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
    public void Change_MovementSpeed(TMP_InputField inputField)
    {
        databaseManager.UpdateMovementSpeed(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
    public void Change_CrouchMovementSpeed(TMP_InputField inputField)
    {
        databaseManager.UpdateCrouchMovementSpeed(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
    public void Change_JumpForce(TMP_InputField inputField)
    {
        databaseManager.UpdateJumpForce(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
    public void Change_CoyoteTime(TMP_InputField inputField)
    {
        databaseManager.UpdateCoyoteTime(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
    public void Change_RollLength(TMP_InputField inputField)
    {
        databaseManager.UpdateRollLength(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
    public void Change_RollSpeed(TMP_InputField inputField)
    {
        databaseManager.UpdateRollSpeed(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
    public void Change_RollCooldown(TMP_InputField inputField)
    {
        databaseManager.UpdateRollCooldown(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
    public void Change_SlideDeceleration(TMP_InputField inputField)
    {
        databaseManager.UpdateSlideDeceleration(currentProfileInfo.profileID,Convert.ToDouble(inputField.text));
    }
}
