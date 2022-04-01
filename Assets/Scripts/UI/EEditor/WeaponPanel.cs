using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AllWeaponInfoStruct;

namespace AllWeaponInfoStruct
{
    public struct AllWeaponInfo
    {
        //basic
        public int weaponId;
        public int weaponTier;
        public string name;
        public int bulletCapacity;
        public double fireRate;
        public bool twoHanded;
        public int weaponClass;
        public int shotType;
        
        //projectile
        public int projectilesWhenFired;
        public double projectileSpeed;

        //accuracy
        public double accuracy;

        //reloadAngle
        public double reloadAngle;

        public AllWeaponInfo(int weaponId, string name, int weaponTier, int bulletCapacity, double fireRate, bool twoHanded, int weaponClass, int shotType, int projectilesWhenFired, double projectileSpeed, double accuracy, double reloadAngle)
        {
            this.weaponId = weaponId;
            this.weaponTier = weaponTier;
            this.name = name;
            this.bulletCapacity = bulletCapacity;
            this.fireRate = fireRate;
            this.twoHanded = twoHanded;
            this.weaponClass = weaponClass;
            this.shotType = shotType;
            
            this.projectilesWhenFired = projectilesWhenFired;
            this.projectileSpeed = projectileSpeed;
            
            this.reloadAngle = reloadAngle;
            
            this.accuracy = accuracy;
        }
    }
}

public class WeaponPanel : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Dropdown inputTier;
    [SerializeField] private TMP_InputField inputBulletCapacity;
    [SerializeField] private TMP_InputField inputFirerate;
    [SerializeField] private Toggle inputTwoHanded;
    [SerializeField] private TMP_Dropdown inputClass;
    [SerializeField] private TMP_Dropdown inputShotType;

    [Header("Projectile Settings")] 
    [SerializeField] private TMP_InputField inputProjectilesWhenFired;
    [SerializeField] private TMP_InputField inputProjectileSpeed;

    [Header("Accuracy")] 
    [SerializeField] private Slider inputAccuracy;

    [Header("ReloadAngle")] 
    [SerializeField] private Slider inputReloadAngle;

    [Header("weapon delete")] 
    [SerializeField] private GameObject weaponDeleteConfirmUi;
    [SerializeField] private TMP_Text weaponDeleteConfirmText;

    [SerializeField] private string deleteMsgPrecursor;
    
    private DeleteWeaponConfirm deleteWepConfirm;
    private DatabaseManager databaseManager;
    private AllWeaponInfo currentWeaponInfo;
    
    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        deleteWepConfirm = weaponDeleteConfirmUi.GetComponent<DeleteWeaponConfirm>();
    }

    public void ShowSettingsFromID(int id)
    {
        currentWeaponInfo = databaseManager.GetAllWeaponInfoFromID(id);

        //basic
        inputName.text = currentWeaponInfo.name;
        inputTier.value = currentWeaponInfo.weaponTier;
        inputBulletCapacity.text = Convert.ToString(currentWeaponInfo.bulletCapacity);
        inputFirerate.text = Convert.ToString(currentWeaponInfo.fireRate);
        inputTwoHanded.isOn = currentWeaponInfo.twoHanded;
        inputClass.value = currentWeaponInfo.weaponClass;
        inputShotType.value = currentWeaponInfo.shotType;

        //projectile
        inputProjectilesWhenFired.text = Convert.ToString(currentWeaponInfo.projectilesWhenFired);
        inputProjectileSpeed.text = Convert.ToString(currentWeaponInfo.projectileSpeed);

        //accuracy
        inputAccuracy.value = (float)currentWeaponInfo.accuracy;

        //reloadAngle
        inputReloadAngle.value = (float) currentWeaponInfo.reloadAngle;

    }

    public void Button_DeleteWeapon()
    {
        weaponDeleteConfirmUi.SetActive(true);
        deleteWepConfirm.SetID(currentWeaponInfo.weaponId);
        weaponDeleteConfirmText.text = deleteMsgPrecursor + currentWeaponInfo.name + "?";
        
    }

    public void Change_WeaponName(TMP_InputField inputField)
    {
        databaseManager.UpdateWeaponName(currentWeaponInfo.weaponId, inputField.text);
    }

    public void Change_WeaponTier(TMP_Dropdown dropDown)
    {
        databaseManager.UpdateWeaponTier(currentWeaponInfo.weaponId, dropDown.value);
    }
    
    public void Change_BulletCapacity(TMP_InputField inputField)
    {
        databaseManager.UpdateBulletCapacity(currentWeaponInfo.weaponId,  Convert.ToInt32(inputField.text));
    }

    public void Change_FireRate(TMP_InputField inputField)
    {
        databaseManager.UpdateFireRate(currentWeaponInfo.weaponId, Convert.ToDouble(inputField.text));
    }

    public void Change_TwoHanded(Toggle toggle)
    {
        int newInt = toggle.isOn == true ? 1 : 0;
        
        databaseManager.UpdateTwoHanded(currentWeaponInfo.weaponId, newInt);
    }

    public void Change_Class(TMP_Dropdown dropDown)
    {
        databaseManager.UpdateClass(currentWeaponInfo.weaponId, dropDown.value);
    }

    public void Change_ShotType(TMP_Dropdown dropDown)
    {
        databaseManager.UpdateShotType(currentWeaponInfo.weaponId, dropDown.value);
    }

    public void Change_ProjectilesWhenFired(TMP_InputField inputField)
    {
        databaseManager.UpdateProjectilesWhenFired(currentWeaponInfo.weaponId,Convert.ToInt32(inputField.text));
    }

    public void Change_ProjectileSpeed(TMP_InputField inputField)
    {
        databaseManager.UpdateProjectileSpeed(currentWeaponInfo.weaponId, Convert.ToDouble(inputField.text));
    }

    public void Change_Accuracy(Slider slider)
    {
        databaseManager.UpdateAccuracy(currentWeaponInfo.weaponId, slider.value);
    }

    public void Change_ReloadAngle(Slider slider)
    {
        databaseManager.UpdateReloadAngle(currentWeaponInfo.weaponId, slider.value);
    }
}
