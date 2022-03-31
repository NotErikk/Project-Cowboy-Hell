using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AllItemInfoStruct;

namespace AllItemInfoStruct
{
    public struct AllItemInfo
    {
        public int itemID;
        
        //basic
        public string itemName;
        public int itemTier;
        public string itemBriefDesc;
        public string itemDesc;
        
        //What Weapon Does This Effect
        public bool effectRevolvers;
        public bool effectPistols;
        public bool effectShotguns;
        public bool effectRifles;

        //WeaponBuffs
        public double extraDamage;
        public double reloadSpeedBuff;
        public double fireRateBuff;
        public bool laserPointer;

        //PlayerBuffs
        public double movementSpeed;
        public double jumpPower;
        public double rollCooldown;
        public double shopDiscount;
        public double damageResistance;
        public double dodgeChance;
        public int extraLives;
        public double maxHealth;

        public AllItemInfo(int itemID, string itemName, int itemTier, string itemBriefDesc, string itemDesc, bool effectRevolvers, bool effectPistols, bool effectShotguns, bool effectRifles, double extraDamage, double reloadSpeedBuff, double fireRateBuff, bool laserPointer, double movementSpeed, double jumpPower, double rollCooldown, double shopDiscount, double damageResistance, double dodgeChance, int extraLives, double maxHealth)
        {
            this.itemID = itemID;
            
            this.itemName = itemName;
            this.itemTier = itemTier;
            this.itemBriefDesc = itemBriefDesc;
            this.itemDesc = itemDesc;
            
            this.effectRevolvers = effectRevolvers;
            this.effectPistols = effectPistols;
            this.effectShotguns = effectShotguns;
            this.effectRifles = effectRifles;
            
            this.extraDamage = extraDamage;
            this.reloadSpeedBuff = reloadSpeedBuff;
            this.fireRateBuff = fireRateBuff;
            this.laserPointer = laserPointer;
            
            this.movementSpeed = movementSpeed;
            this.jumpPower = jumpPower;
            this.rollCooldown = rollCooldown;
            this.shopDiscount = shopDiscount;
            this.damageResistance = damageResistance;
            this.dodgeChance = dodgeChance;
            this.extraLives = extraLives;
            this.maxHealth = maxHealth;
        }
    }
}

public class ItemPanel : MonoBehaviour
{
    [Header("Basic Settings")] 
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Dropdown inputItemTier;
    [SerializeField] private TMP_InputField inputBriefDesc;
    [SerializeField] private TMP_InputField inputDescription;

    [Header("What Weapon Does This Effect")] 
    [SerializeField] private Toggle inputEffectRevolvers;
    [SerializeField] private Toggle inputEffectPistols;
    [SerializeField] private Toggle inputEffectShotguns;
    [SerializeField] private Toggle inputEffectRifles;

    [Header("Weapon Buffs")] 
    [SerializeField] private TMP_InputField inputExtraDamage;
    [SerializeField] private TMP_InputField inputReloadSpeed;
    [SerializeField] private TMP_InputField inputFireRate;
    [SerializeField] private Toggle inputLaserPointer;

    [Header("Player Buffs")] 
    [SerializeField] private TMP_InputField inputMovementSpeed;
    [SerializeField] private TMP_InputField inputJumpPower;
    [SerializeField] private TMP_InputField inputRollCooldown;
    [SerializeField] private TMP_InputField inputShopDiscount;
    [SerializeField] private TMP_InputField inputDamageResistance;
    [SerializeField] private TMP_InputField inputDodgeChance;
    [SerializeField] private TMP_InputField inputExtraLives;
    [SerializeField] private TMP_InputField inputMaxHealth;

    [Header("Item Delete")] 
    [SerializeField] private GameObject itemDeleteConfirmUI;
    [SerializeField] private TMP_Text itemDeleteConfirmText;
    [SerializeField] private string deleteItemPrecursor;

    private DeleteItemConfirm deleteItemConfirm;
    private DatabaseManager databaseManager;
    private AllItemInfo currentItemInfo;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        deleteItemConfirm = itemDeleteConfirmUI.GetComponent<DeleteItemConfirm>();
    }

    public void ShowSettingsFromID(int id)
    {
        currentItemInfo = databaseManager.GetAllItemInfoFromID(id);
        
        //basic
        inputName.text = currentItemInfo.itemName;
        inputItemTier.value = currentItemInfo.itemTier;
        inputBriefDesc.text = currentItemInfo.itemBriefDesc;
        inputDescription.text = currentItemInfo.itemDesc;

        //what weapon does this effect
        inputEffectRevolvers.isOn = currentItemInfo.effectRevolvers;
        inputEffectPistols.isOn = currentItemInfo.effectPistols;
        inputEffectShotguns.isOn = currentItemInfo.effectShotguns;
        inputEffectRifles.isOn = currentItemInfo.effectRifles;

        //weapon buffs
        inputExtraDamage.text = Convert.ToString(currentItemInfo.extraDamage);
        inputReloadSpeed.text = Convert.ToString(currentItemInfo.reloadSpeedBuff);
        inputFireRate.text = Convert.ToString(currentItemInfo.fireRateBuff);
        inputLaserPointer.isOn = currentItemInfo.laserPointer;

        //player buffs
        inputMovementSpeed.text = Convert.ToString(currentItemInfo.movementSpeed);
        inputJumpPower.text = Convert.ToString(currentItemInfo.jumpPower);
        inputRollCooldown.text = Convert.ToString(currentItemInfo.rollCooldown);
        inputShopDiscount.text = Convert.ToString(currentItemInfo.shopDiscount);
        inputDamageResistance.text = Convert.ToString(currentItemInfo.damageResistance);
        inputDodgeChance.text = Convert.ToString(currentItemInfo.dodgeChance);
        inputExtraLives.text = Convert.ToString(currentItemInfo.extraLives);
        inputMaxHealth.text = Convert.ToString(currentItemInfo.maxHealth);
    }
    
    public void Button_DeleteItem()
    {
        itemDeleteConfirmUI.SetActive(true);
        deleteItemConfirm.SetID(currentItemInfo.itemID);
        itemDeleteConfirmText.text = deleteItemPrecursor + currentItemInfo.itemName + "?";
    }
    public void Change_ItemName(TMP_InputField inputField)
    {
        databaseManager.UpdateItemName(currentItemInfo.itemID, inputField.text);
    }

    public void Change_ItemTier(TMP_Dropdown dropDown)
    {
        databaseManager.UpdateItemTier(currentItemInfo.itemID, dropDown.value);
    }

    public void Change_ItemBriefDesc(TMP_InputField inputField)
    {
        databaseManager.UpdateItemBrief(currentItemInfo.itemID, inputField.text);
    }

    public void Change_ItemDesc(TMP_InputField inputField)
    {
        databaseManager.UpdateItemDesc(currentItemInfo.itemID, inputField.text);
    }

    public void Change_RevolverToggle(Toggle toggle)
    {
        int intToggle = toggle.isOn ? 1 : 0;
        databaseManager.UpdateItemEffectRevolvers(currentItemInfo.itemID, intToggle);
    }

    public void Change_PistolToggle(Toggle toggle)
    {
        int intToggle = toggle.isOn ? 1 : 0;
        databaseManager.UpdateItemEffectPistols(currentItemInfo.itemID, intToggle);
    }

    public void Change_ShotgunToggle(Toggle toggle)
    {
        int intToggle = toggle.isOn ? 1 : 0;
        databaseManager.UpdateItemEffectShotguns(currentItemInfo.itemID, intToggle);
    }

    public void Change_RifleToggle(Toggle toggle)
    {
        int intToggle = toggle.isOn ? 1 : 0;
        databaseManager.UpdateItemEffectRifles(currentItemInfo.itemID, intToggle);
    }

    public void Change_ExtraDamage(TMP_InputField inputField)
    {
        databaseManager.UpdateItemExtraDmg(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_ReloadSpeed(TMP_InputField inputField)
    {
        databaseManager.UpdateItemReloadSpeed(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_FireRate(TMP_InputField inputField)
    {
        databaseManager.UpdateFireRate(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_LaserPointer(Toggle toggle)
    {
        int intToggle = toggle.isOn ? 1 : 0;
        databaseManager.UpdateItemLaserPointer(currentItemInfo.itemID, intToggle);
    }

    public void Change_MovementSpeed(TMP_InputField inputField)
    {
        databaseManager.UpdateItemMovementSpeed(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_JumpPower(TMP_InputField inputField)
    {
        databaseManager.UpdateItemJumpPower(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_RollCooldown(TMP_InputField inputField)
    {
        databaseManager.UpdateItemRollCooldown(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_ShopDiscount(TMP_InputField inputField)
    {
        databaseManager.UpdateShopDiscount(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_DamageResistance(TMP_InputField inputField)
    {
        databaseManager.UpdateDamageResistance(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_DodgeChance(TMP_InputField inputField)
    {
        databaseManager.UpdateItemDodgeChance(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }

    public void Change_ExtraLives(TMP_InputField inputField)
    {
        databaseManager.UpdateItemExtraLives(currentItemInfo.itemID,Convert.ToInt32(inputField.text));
    }

    public void Change_MaxHealth(TMP_InputField inputField)
    {
        databaseManager.UpdateItemMaxHealth(currentItemInfo.itemID,Convert.ToDouble(inputField.text));
    }
}
