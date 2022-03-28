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

        public AllWeaponInfo(string name, int bulletCapacity, double fireRate, bool twoHanded, int weaponClass, int shotType, int projectilesWhenFired, double projectileSpeed, double accuracy, double reloadAngle)
        {
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


    private DatabaseManager databaseManager;
    private AllWeaponInfo currentWeaponInfo;
    
    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }

    public void ShowSettingsFromID(int id)
    {
        currentWeaponInfo = databaseManager.GetAllWeaponInfoFromID(id);

        //basic
        inputName.text = currentWeaponInfo.name;
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
    
    
}
