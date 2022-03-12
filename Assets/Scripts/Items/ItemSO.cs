using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddNewItem", menuName = "Custom/AddNewItem")]
public class ItemSO : ScriptableObject
{
    //
    // there are two types of stacking.  Linear and Hyperbolic, see here: https://riskofrain2.fandom.com/wiki/Item_Stacking#Linear_Stacking
    //
    // Linear function = 1 + effect * amount of effects
    // Hyperbolic function = 1 - 1/(1 + effect * amount of effects)
    //

    [Tooltip("Display name of item used within game")]
    public string itemName;

    [Tooltip("My Tier")]
    public Tiers.tier myTier;

    [Tooltip("Sprite used for item when dropped/inventory")]
    public Sprite itemSprite;

    [Tooltip("Description to be shown when standing over item")]
    public string briefDescription;

    [Tooltip("Description to be shown in inventory")]
    public string description;

    

    [Header("Firearm type/s To Affect")]
    public bool effectRevolvers;
    public bool effectPistols;
    public bool effectShotguns;
    public bool effectRifles;


    [Header("Firearm modifiers")]

    [Tooltip("Linear +% increase")]
    public double extraDamage;

    [Tooltip("Hyperbolic +% increase")]
    public double reloadSpeedIncrease;

    [Tooltip("Hyperbolic +% increase")]
    public double fireRate;

    [Tooltip("on/ignore")]
    public bool enableLaserPointer;



    [Header("Movement modifiers")]

    [Tooltip("Linear +% increase")]
    public double movementSpeedIncrease;

    [Tooltip("Linear +% increase")]
    public double jumpPowerIncrease;

    [Tooltip("Hyperbolic +% increase")]
    public double rollCooldownDecrease;



    [Header("Player modifiers")]

    [Tooltip("Hyperbolic +% increase.")]
    public double shopDiscount;

    [Tooltip("Hyperbolic +% increase")]
    public double damageResistance;

    [Tooltip("Hyperbolic +% increase")]
    public double dodgeChance;

    [Tooltip("int to increase by")]
    public double extraLivesToGive;

    [Tooltip("int to increase health by")]
    public double increaseMaxHealth;


    

}
