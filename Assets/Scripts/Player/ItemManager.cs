using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<ItemSO> itemList;

    //CURRENT MODIFIERS

    //weps
    [Header("Weapons")]
    public double revolverDamageBuff; //Linear
    public double pistolDamageBuff;
    public double shotgunDamageBuff;
    public double rifleDamageBuff;

    public float revolverReloadSpeedBuff = 1; //Hyperbolic
    public float pistolReloadSpeedBuff;
    public float shotgunReloadSpeedBuff;
    public float rifleReloadSpeedBuff;

    public float revolverFireRateBuff; //Hyperbolic
    public float pistolFireRateBuff;
    public float shotgunFireRateBuff;
    public float rifleFireRateBuff;

    public bool laserPointer; //on/ignore


    //movement
    [Header("Movement")]
    public double movementSpeedBuff; //Linear
    public double jumpForceBuff;     //Linear
    public float rollCooldownBuff;  //Hyperbolic


    //player
    [Header("Player")]
    public float shopDiscountPercentage; //Hyperbolic
    public float damageResistanceBuff;   //Hyperbolic
    public float dodgeChance;            //Hyperbolic
    public double extraLives;             //Linear
    public double maxHealthBonus;         //Linear


    //scripts I need to change stuff in or access
    InteractableObjectPickup interactableObjectPickup;
    PlayerMovementScript playerMovementScript;
    WeaponController weaponController;
    [SerializeField]WeaponAiming weaponAiming;



    public void Awake()
    {
        itemList = new List<ItemSO>();
        interactableObjectPickup = GetComponent<InteractableObjectPickup>();
        weaponController = GetComponent<WeaponController>();
        playerMovementScript = GetComponent<PlayerMovementScript>();
        
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (interactableObjectPickup.currentItemStoodUpon != null)
            {
                RegisterANewItem(interactableObjectPickup.currentItemStoodUpon.GetComponent<InteractableItem>().item);
                Destroy(interactableObjectPickup.currentItemStoodUpon);
            }
        }
    }


    public void RegisterANewItem(ItemSO newItem)
    {
        itemList.Add(newItem);

        //weps
        if (newItem.effectRevolvers)
        {
            LinearAddition(ref revolverDamageBuff, newItem.extraDamage);
            HyperbolicAddition(ref revolverReloadSpeedBuff, newItem.reloadSpeedIncrease, newItem);
            HyperbolicAddition(ref revolverFireRateBuff, newItem.fireRate, newItem);
        }
        if (newItem.effectPistols)
        {
            LinearAddition(ref pistolDamageBuff, newItem.extraDamage);
            HyperbolicAddition(ref pistolReloadSpeedBuff, newItem.reloadSpeedIncrease, newItem);
            HyperbolicAddition(ref pistolFireRateBuff, newItem.fireRate, newItem);
        }
        if (newItem.effectShotguns)
        {
            LinearAddition(ref shotgunDamageBuff, newItem.extraDamage);
            HyperbolicAddition(ref shotgunReloadSpeedBuff, newItem.reloadSpeedIncrease, newItem);
            HyperbolicAddition(ref shotgunFireRateBuff, newItem.fireRate, newItem);
        }
        if (newItem.effectRifles)
        {
            LinearAddition(ref rifleDamageBuff, newItem.extraDamage);
            HyperbolicAddition(ref rifleReloadSpeedBuff, newItem.reloadSpeedIncrease, newItem);
            HyperbolicAddition(ref rifleFireRateBuff, newItem.fireRate, newItem);
        }

        if (newItem.enableLaserPointer) laserPointer = true;


        //movement
        LinearAddition(ref movementSpeedBuff, newItem.movementSpeedIncrease);
        LinearAddition(ref jumpForceBuff, newItem.jumpPowerIncrease);
        HyperbolicAddition(ref rollCooldownBuff, newItem.rollCooldownDecrease, newItem);


        //player
        HyperbolicAddition(ref shopDiscountPercentage, (float)newItem.shopDiscount, newItem);
        HyperbolicAddition(ref damageResistanceBuff, newItem.damageResistance, newItem);
        HyperbolicAddition(ref dodgeChance, newItem.dodgeChance, newItem);
        LinearAddition(ref extraLives, newItem.extraLivesToGive);
        LinearAddition(ref maxHealthBonus, newItem.increaseMaxHealth);



        UpdateBuffs();

    }


    private void LinearAddition(ref double statisticBeingIncreased, double increase)
    {
        statisticBeingIncreased = statisticBeingIncreased + increase;
    }

    private void HyperbolicAddition(ref float statisticBeingIncreased, double increase, ItemSO newItem)
    {
        if (increase == 0) return;

        int currentAmountOfNewItem = 0;
        foreach (ItemSO item in itemList)
        {
            if (item == newItem) currentAmountOfNewItem++;
        }
        Debug.Log("I have now picked up " + currentAmountOfNewItem + " " + newItem.itemName);

        //first pickup of type
        if (currentAmountOfNewItem == 1)
        {
            statisticBeingIncreased = (float)increase;
            return;
        }

        statisticBeingIncreased = 1 - 1 / (1 + (float)increase * currentAmountOfNewItem);
    }



    private void UpdateBuffs()
    {
        weaponController.UpdateDamageModifier();
        weaponController.UpdateReloadModifier();
        weaponController.UpdateFirerateModifier();
        weaponAiming.ToggleLaserPointer(laserPointer);

        playerMovementScript.UpdateMovementSpeedBuff();
        playerMovementScript.UpdateJumpBuff();
        playerMovementScript.UpdateRollCooldownBuff();

        //shop discount
        //damage resitstance
        //dodge chance
        //extra lives to give
        //increase max health
    }
}
