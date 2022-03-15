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
    
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("LevelManager") != null)
        {
            currentLevel = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().GetCurrentLevel();
            currentLevel++;
        }
        
        if (myLootType == LootBoxTypes.Random)
        {
            int randomValue = Random.Range(0, 2);
            myLootType = (LootBoxTypes)randomValue;
        }
        SetTierOfLoot();
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
            ;
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
        var allWeps = GetAllWeaponsOfTier();
        droppedWeapon.GetComponent<InteractableWeapon>().firearm = allWeps[Random.Range(0, allWeps.Count)];
        
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
        var allItems = GetAllItemsOfTier();
        droppedItem.GetComponent<InteractableItem>().item = allItems[Random.Range(0, allItems.Count)];
        
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
