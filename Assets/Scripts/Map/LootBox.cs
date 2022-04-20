using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LootBox : MonoBehaviour
{
    
    enum LootBoxTypes
    {
        Item,
        Weapon,
        Random
    }
    [Header("Settings")]
    [SerializeField] private LootBoxTypes myLootType;

    
    [Range(1,6)][SerializeField] private int currentLevel;
    [SerializeField] Tiers.tier myTier;

    [SerializeField] private float throwObjectOnOpenForce;
    
    [Header("Assignables")]
    [SerializeField] private GameObject interactableItemPrefab;
    [SerializeField] private GameObject interactableWeaponPrefab;

    [Header("Blank Weapon")] 
    [SerializeField] private FirearmSO blankWeapon;

    [Header("Blank Item")] 
    [SerializeField] private ItemSO blankItem;
    
    [Header("Weapons")] 
    [SerializeField] FirearmSO[] tier1Weps;
    [SerializeField] FirearmSO[] tier2Weps;
    [SerializeField] FirearmSO[] tier3Weps;
    [SerializeField] FirearmSO[] tier4Weps;
    [SerializeField] FirearmSO[] tier5Weps;
    
    [Header("Items")]
    [SerializeField] ItemSO[] tier1Items;
    [SerializeField] ItemSO[] tier2Items;
    [SerializeField] ItemSO[] tier3Items;
    [SerializeField] ItemSO[] tier4Items;
    [SerializeField] ItemSO[] tier5Items;

    [Header("Odds, Tier1 = 0, Tier 5 = 4")]
    [SerializeField] private float[] level1Odds;
    [SerializeField] private float[] level2Odds;
    [SerializeField] private float[] level3Odds;
    [SerializeField] private float[] level4Odds;
    [SerializeField] private float[] level5Odds;
    [SerializeField] private float[] level6Odds;

    private DatabaseManager databaseManager;
    private int currentProfileID;
    
    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();


        //Get current level
        if (GameObject.FindGameObjectWithTag("LevelManager") != null) 
        {
            currentLevel = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().GetCurrentLevel();
            currentLevel++;
        }
        
        //weapon or item?
        if (myLootType == LootBoxTypes.Random)
        {
            int randomValue = Random.Range(0, 2);
            myLootType = (LootBoxTypes)randomValue;
        }
        
        SetTierOfLoot();
    }

    private void Start()
    {
        currentProfileID = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().GetCurrentProfileID();
    }

    [ContextMenu("Re-Roll Tier")]
    void SetTierOfLoot()
    {
        float tierRoll = Random.Range(0.0f, 100.0f);
        switch (currentLevel)
        {
            case 1:
                myTier = GetTierFromOddsAndRoll(tierRoll, level1Odds);
                return;
            
            case 2:
                myTier = GetTierFromOddsAndRoll(tierRoll, level2Odds);
                return;
            
            case 3:
                myTier = GetTierFromOddsAndRoll(tierRoll, level3Odds);
                return;
            
            case 4:
                myTier = GetTierFromOddsAndRoll(tierRoll, level4Odds);
                return;
            
            case 5:
                myTier = GetTierFromOddsAndRoll(tierRoll, level5Odds);
                return;
            
            case 6:
                myTier = GetTierFromOddsAndRoll(tierRoll, level6Odds);
                return;
            
            default:
                Debug.LogError("Invalid currentLevel within SetTierOfLoot() in LootBox.cs");
                return;
        }
    }
    Tiers.tier GetTierFromOddsAndRoll(float Roll, float[] Odds)
    {
        if (Roll <= Odds[0])
        {
            return Tiers.tier.tier1;
        }
        else if (Roll <= Odds[0] + Odds[1])
        {
            return Tiers.tier.tier2;
        }
        else if (Roll <= Odds[0] + Odds[1] + Odds[2])
        {
            return Tiers.tier.tier3;
        }
        else if (Roll <= Odds[0] + Odds[1] + Odds[2] + Odds[3])
        {
            return Tiers.tier.tier4;
        }
        else if (Roll <= Odds[0] + Odds[1] + Odds[2] + Odds[3] + Odds[4])
        {
            return Tiers.tier.tier5;
        }
        
        Debug.LogError("Invalid Roll within GetTierFromOddsAndRoll() in LootBox.cs");
        return Tiers.tier.tier1;
    }
    
    
    [ContextMenu("Open Loot Box")]
    public void OpenBox()
    {
        if (myLootType == LootBoxTypes.Weapon)
        {
            OutputWeapon();
        }
        else
        {
            OutputItem();
        }
        Destroy(gameObject);
    }
    
    
    
    //weps
    void OutputWeapon()
    {
        //spawn dropped wep
        var myTransform = transform;
        GameObject droppedWeapon = Instantiate(interactableWeaponPrefab, myTransform.position, myTransform.rotation);
        
        //set to random wep
        var allWeps = databaseManager.GetAllWeaponInfoFromTierAndProfile((int)myTier, currentProfileID);
        var wep = allWeps[Random.Range(0, allWeps.Count)];

        var firearm = ScriptableObject.CreateInstance<FirearmSO>();

        //database data into new firearm
        firearm.GunSprite = blankWeapon.GunSprite;
        firearm.BulletSprite = blankWeapon.BulletSprite;
        firearm.CasingSprite = blankWeapon.CasingSprite;
        firearm.ProjectileSprite = blankWeapon.ProjectileSprite;
        firearm.DisplayName = wep.name;
        firearm.myTier = (Tiers.tier) wep.weaponTier;
        firearm.damage = blankWeapon.damage;
        firearm.firearmType = (FirearmSO.FirearmClassesAvailable) wep.weaponClass;
        firearm.BulletCapacity = wep.bulletCapacity;
        firearm.ProjectilesOnFire = wep.projectilesWhenFired;
        firearm.ProjectileSpeed = (int) wep.projectileSpeed;
        firearm.BaseAccuracy = (float) wep.accuracy;
        firearm.Firerate = (float) wep.fireRate;
        firearm.TwoHanded = wep.twoHanded;
        firearm.WeaponReloadAngle = (float) wep.reloadAngle;
        firearm.myShootType = (FirearmSO.ShootTypes) wep.shotType;
        firearm.ProjectileGO = blankWeapon.ProjectileGO;
        
        firearm.ReloadSetUpActions = blankWeapon.ReloadSetUpActions;
        firearm.MainReloadActions = blankWeapon.MainReloadActions;
        firearm.ReloadFinishedActions = blankWeapon.ReloadFinishedActions;
        
        
        droppedWeapon.GetComponent<InteractableWeapon>().firearm = firearm;

        //throw wep into air
        Rigidbody2D droppedWepRb = droppedWeapon.GetComponent<Rigidbody2D>();
        droppedWepRb.AddForce(new Vector2(0.0f,throwObjectOnOpenForce));
    }

    List<FirearmSO> GetAllWeaponsOfTier()
    {
        switch (myTier)
        {
            case Tiers.tier.tier1:
                 return tier1Weps.ToList();

            case Tiers.tier.tier2:
                return tier2Weps.ToList();

            case Tiers.tier.tier3:
                return tier3Weps.ToList();

            case Tiers.tier.tier4:
                return tier4Weps.ToList();

            case Tiers.tier.tier5:
                return tier5Weps.ToList();
            
            default:
                Debug.LogError("LootBox.cs weapon tier not assigned correctly, GetAllItemsOfTier()");
                return tier1Weps.ToList();
        }
        
    }
    
    //items
    void OutputItem()
    {
        //spawn dropped item
        var myTransform = transform;
        GameObject droppedItem = Instantiate(interactableItemPrefab, myTransform.position, myTransform.rotation);
        
        //set to random item
        var allItems = databaseManager.GetAllItemInfoFromTierAndProfileID((int)myTier, currentProfileID);
        var item = allItems[Random.Range(0, allItems.Count)];

        item.itemName = item.itemName;
        var newItem = ScriptableObject.CreateInstance<ItemSO>();

        newItem.itemName = item.itemName;
        newItem.myTier = (Tiers.tier) item.itemTier;
        newItem.itemSprite = blankItem.itemSprite;
        newItem.briefDescription = item.itemBriefDesc;
        newItem.description = item.itemDesc;
        
        newItem.effectRevolvers = item.effectRevolvers;
        newItem.effectPistols = item.effectPistols;
        newItem.effectShotguns = item.effectShotguns;
        newItem.effectRifles = item.effectRifles;
        
        newItem.extraDamage = item.extraDamage;
        newItem.reloadSpeedIncrease = item.reloadSpeedBuff;
        newItem.fireRate = item.fireRateBuff;
        newItem.enableLaserPointer = item.laserPointer;
        
        newItem.movementSpeedIncrease = item.movementSpeed;
        newItem.jumpPowerIncrease = item.jumpPower;
        newItem.rollCooldownDecrease = item.rollCooldown;

        newItem.shopDiscount = item.shopDiscount;
        newItem.damageResistance = item.damageResistance;
        newItem.dodgeChance = item.dodgeChance;
        newItem.extraLivesToGive = item.extraLives;
        newItem.increaseMaxHealth = item.maxHealth;

        droppedItem.GetComponent<InteractableItem>().item = newItem;
        
        //throw item into air
        Rigidbody2D droppedItemRb = droppedItem.GetComponent<Rigidbody2D>();
        droppedItemRb.AddForce(new Vector2(0.0f,throwObjectOnOpenForce));
    }

    List<ItemSO> GetAllItemsOfTier()
    {
        switch (myTier)
        {
            case Tiers.tier.tier1:
                return tier1Items.ToList();

            case Tiers.tier.tier2:
                return tier2Items.ToList();

            case Tiers.tier.tier3:
                return tier3Items.ToList();

            case Tiers.tier.tier4:
                return tier4Items.ToList();

            case Tiers.tier.tier5:
                return tier5Items.ToList();
            
            default:
                Debug.LogError("LootBox.cs item tier not assigned correctly, GetAllItemsOfTier()");
                return tier1Items.ToList();
        }
    }
}
