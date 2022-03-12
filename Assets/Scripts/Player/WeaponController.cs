using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    ///Holds current and secondary firearm scriptable objects created from FirearmSO.  assigning these should change the players current weapon
    [Header("CurrentInv")]
    [Tooltip("Current Weapon In Hand")]
    public FirearmSO CurrentFirearm;

    [Tooltip("Weapon not currently in use")]
    [SerializeField]
    private FirearmSO SecondaryFirearm;

    public int CurrentFirearmAmmoCount;
    private int SecondaryFirearmAmmoCount;

    [Header("CurrentBuffs")]

    [Tooltip("starting ReloadSpeed")]
    [SerializeField]
    float baseReloadSpeed;

    [Tooltip("reload speed Buff, increases reload speed")]
    [SerializeField]
    private float ReloadSpeedBuff;

    public float fireRateItemBuff;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////MISC
    [Header("MISC")]

    [Tooltip("How long it takes to swap weapons (in seconds)")]

    [SerializeField] float WeaponSwapTime;

    [SerializeField] int weaponDropThrowingForce;

    [HideInInspector] public bool canSwapWeapon;
    [HideInInspector] public bool canDropWeapon = true;

    [HideInInspector]
    public bool reloading;

    [Tooltip("To stop the player shooting behind them, this value is the range of which the player's click cannot be within")]
    [SerializeField] float cantShootThisCloseFloat;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Assign Before Hand
    ///assign all these before running game, just misc gameobjects and scripts for use later
    [Header("Assign Before hand")]

    [Tooltip("Assign weapon aiming on weapon")]
    [SerializeField]
    private WeaponAiming sWeaponAiming;

    [Tooltip("Assign weapon gameobject (child of player)")]
    [SerializeField]
    private GameObject WeaponGO; //the weapon holder empty game object.  used to get sprite renderer to change visually what gun is being shown

    [Tooltip("Assign HoldingPoint gameobject (child of player, child of weapon)")]
    [SerializeField]
    private GameObject HoldingPointFront; //where front hand will hold gun

    [Tooltip("Assign HoldingPointback gameobject (child of player, child of weapon)")]
    [SerializeField]
    private GameObject HoldingPointBack; //if a second hand is needed for the gun this holding point is used for the second hand

    [Tooltip("Weapon Reload Position, where the character will hold their weapon to reload it")]
    [SerializeField]
    private GameObject ReloadPosition;

    [Tooltip("Assign BackArm gameobject (child of player)")]
    [SerializeField]
    private GameObject BackArm; //game object for backarm (used to be turned on and off if not currently in use)

    [Tooltip("Assign FirePoint gameobject (child of player, child of weapon)")]
    [SerializeField]
    private GameObject FirePoint; //where the bullets come from (front of gun)

    [Tooltip("Assign PlayerPocket")]
    [SerializeField]
    private GameObject PlayerPocket;

    [Tooltip("SpentCasingPrefab to spawn on edject")]

    [SerializeField] GameObject SpentCasingPrefab;

    [Tooltip("Weapon Normal Firing Position for after a reload")]
    [SerializeField]
    private GameObject NormalFirePos;


    private SpriteRenderer WeaponSR; //weapon sprite renderer grabbed from WeaponGO

    private FirearmSO TempHolder; //used just to temp grab a FirearmSO when switching weapon

    InteractableObjectPickup sObjectPickup;

    [Header("currents")]
    public float currentAccuracy; //accuracy multiplier, this value is changed from WeaponAiming to see if the player is hipfiring or not

    public float FirerateHipFireMultiplier;//value changed from WeaponAiming when the player hip fires and un hipfires

    public float currentDamageToDeal;

    [Tooltip("Gameobject of the cursor to ensure the player is facing the correct direction")]
    [SerializeField]
    private GameObject Cursor;

    [Tooltip("Assign the interactableWeapon prefab so weapons can be dropped")]
    [SerializeField] GameObject interactableWeaponPrefab;


    ShootLibary shootLibary;
    ItemManager itemManager;

    private float LastShotTime = 0f;
    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool canPickUpWeapon;
    [HideInInspector] public bool canReload;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void Awake()
    {
        WeaponSR = WeaponGO.GetComponent<SpriteRenderer>();
        sObjectPickup = GetComponent<InteractableObjectPickup>();
        itemManager = GetComponent<ItemManager>();

        shootLibary = new ShootLibary();
        
    }
    void Start()
    {
        WeaponSR.sprite = CurrentFirearm.GunSprite;
        FirePoint.transform.localPosition = CurrentFirearm.FirePointCords;
        UpdateReloadModifier();
        UpdateFirerateModifier();
        UpdateDamageModifier();
        ResetHandPositionsForNewFirearm();
        TogglePlayerAim(true);

        canSwapWeapon = true;
        canShoot = true;
        canReload = true;
        canPickUpWeapon = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Reload") && !reloading && canReload)
        {
            StartCoroutine(Reload());
        }
        else if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }
        else if (Input.GetButtonDown("Switch Weapon") && canSwapWeapon) //switches current weapon and changes weapon grab points so hands work
        {
            SwapWeapons();
        }
        else if(Input.GetButtonDown("Drop Weapon") && canDropWeapon)
        {
            DropWeapon();
        }
        else if (Input.GetButtonDown("Interact"))
        {
            PickUpNewWeapon();
        }
    }

    private void SwapWeapons()
    {
        
        if (SecondaryFirearm == null) return;
        //swap wep ammo counts
        int CurrentFirearmAmmoCountTEMPHOLDER = CurrentFirearmAmmoCount;
        CurrentFirearmAmmoCount = SecondaryFirearmAmmoCount;
        SecondaryFirearmAmmoCount = CurrentFirearmAmmoCountTEMPHOLDER;

        //swap weps
        TempHolder = CurrentFirearm;
        CurrentFirearm = SecondaryFirearm;
        SecondaryFirearm = TempHolder;
        WeaponSR.sprite = CurrentFirearm.GunSprite;

        
        ResetHandPositionsForNewFirearm();
        UpdateFirerateModifier();
        UpdateReloadModifier();
        UpdateDamageModifier();
        LastShotTime = Time.time + WeaponSwapTime; //cooldown for pull out then shoot to avoid weapon switch spam fire
        FirePoint.transform.localPosition = CurrentFirearm.FirePointCords;
    }



    public void ResetHandPositionsForNewFirearm() //resets hands positions to work with CurrentFirearm
    {
        HoldingPointFront.transform.localPosition = CurrentFirearm.ShootingHandHoldingPoint;
        if (CurrentFirearm.TwoHanded)
        {
            BackArm.SetActive(true);
            HoldingPointBack.transform.localPosition = CurrentFirearm.OtherHandHoldingPoint;
        }
        else
        {
            BackArm.SetActive(false);
        }
    }

    public void UpdateReloadModifier()
    {
        switch (CurrentFirearm.firearmType)
        {
            case FirearmSO.FirearmClassesAvailable.revolver:
                ReloadSpeedBuff = baseReloadSpeed - (baseReloadSpeed * (1 * itemManager.revolverReloadSpeedBuff));
                break;

            case FirearmSO.FirearmClassesAvailable.pistol:
                ReloadSpeedBuff = baseReloadSpeed - (baseReloadSpeed * (1 * itemManager.pistolReloadSpeedBuff));
                break;

            case FirearmSO.FirearmClassesAvailable.shotgun:
                ReloadSpeedBuff = baseReloadSpeed - (baseReloadSpeed * (1 * itemManager.shotgunReloadSpeedBuff));
                break;

            case FirearmSO.FirearmClassesAvailable.rifle:
                ReloadSpeedBuff = baseReloadSpeed - (baseReloadSpeed * (1 * itemManager.rifleReloadSpeedBuff));
                break;
        }
    }

    public void UpdateFirerateModifier()
    {
        float baseFirerate = CurrentFirearm.Firerate;

        switch (CurrentFirearm.firearmType)
        {
            case FirearmSO.FirearmClassesAvailable.revolver:
                fireRateItemBuff = baseFirerate - (baseFirerate * (1 * itemManager.revolverFireRateBuff));
                break;

            case FirearmSO.FirearmClassesAvailable.pistol:
                fireRateItemBuff = baseFirerate - (baseFirerate * (1 * itemManager.pistolFireRateBuff));
                break;

            case FirearmSO.FirearmClassesAvailable.shotgun:
                fireRateItemBuff = baseFirerate - (baseFirerate * (1 * itemManager.shotgunFireRateBuff));
                break;

            case FirearmSO.FirearmClassesAvailable.rifle:
                fireRateItemBuff = baseFirerate - (baseFirerate * (1 * itemManager.rifleFireRateBuff));
                break;
        }
    }

    public void UpdateDamageModifier()
    {
        float baseDmg = CurrentFirearm.damage;

        switch (CurrentFirearm.firearmType)
        {
            case FirearmSO.FirearmClassesAvailable.revolver:
                currentDamageToDeal = CurrentFirearm.damage * (1 + (float)itemManager.revolverDamageBuff);
                break;

            case FirearmSO.FirearmClassesAvailable.pistol:
                currentDamageToDeal = CurrentFirearm.damage * (1 + (float)itemManager.pistolDamageBuff);
                break;

            case FirearmSO.FirearmClassesAvailable.shotgun:
                currentDamageToDeal = CurrentFirearm.damage * (1 + (float)itemManager.shotgunDamageBuff);
                break;

            case FirearmSO.FirearmClassesAvailable.rifle:
                currentDamageToDeal = CurrentFirearm.damage * (1 + (float)itemManager.rifleDamageBuff);
                break;
        }
    }

    private IEnumerator Reload()
    {
        if (CurrentFirearmAmmoCount == CurrentFirearm.BulletCapacity) yield break;

        canShoot = false;
        canSwapWeapon = false;
        canDropWeapon = false;
        canPickUpWeapon = false;

        int ShotsToReload = CurrentFirearm.BulletCapacity - CurrentFirearmAmmoCount;

            ReloadMechanics myReloadMechanics = new ReloadMechanics(sWeaponAiming, WeaponGO.transform, CurrentFirearm, ReloadPosition.transform, transform, Cursor.transform, ReloadSpeedBuff, HoldingPointBack.transform, SpentCasingPrefab, PlayerPocket.transform, BackArm, CurrentFirearm.WeaponReloadAngle, this);
            
            
            myReloadMechanics.ToggleReloadLocks(ref reloading, ref canSwapWeapon, true);
            yield return StartCoroutine(myReloadMechanics.BringGunCloseToPlayer());
            myReloadMechanics.ToggleSecondHandVisible(true);

            foreach (FirearmSO.ReloadFunctions Func in CurrentFirearm.ReloadSetUpActions) //setup
            {
                 yield return StartCoroutine(ReturnReloadAction(myReloadMechanics, Func));
            }

            for (int i = 0; i < ShotsToReload; i++)
            {
                foreach (FirearmSO.ReloadFunctions Func in CurrentFirearm.MainReloadActions) //Main
                {
                    yield return StartCoroutine(ReturnReloadAction(myReloadMechanics, Func));
                }
            }

            foreach (FirearmSO.ReloadFunctions Func in CurrentFirearm.ReloadFinishedActions) //Complete
            {
                yield return StartCoroutine(ReturnReloadAction(myReloadMechanics, Func));
            }

        

        myReloadMechanics.ToggleSecondHandVisible(CurrentFirearm.TwoHanded);
        ResetHandPositionsForNewFirearm();
            yield return StartCoroutine(myReloadMechanics.ReturnToShootingPosition());
            myReloadMechanics.ToggleReloadLocks(ref reloading, ref canSwapWeapon, false);

        canShoot = true;
        canSwapWeapon = true;
        canDropWeapon = true;
        canPickUpWeapon = true;
        yield break;
    }

    IEnumerator ReturnReloadAction(ReloadMechanics myReloadMechanics,FirearmSO.ReloadFunctions func)
    {
       
        switch (func)
        {
            
            case FirearmSO.ReloadFunctions.BringHammerHalfCock:
                yield return StartCoroutine(myReloadMechanics.BringHammerToHalfCock());
                break;

            case FirearmSO.ReloadFunctions.BringHammerToFullCock:
                yield return StartCoroutine(myReloadMechanics.BringHammerToFullCock());
                break;

            case FirearmSO.ReloadFunctions.CloseLoadingGate:
                yield return StartCoroutine(myReloadMechanics.CloseLoadingGate());
                break;

            case FirearmSO.ReloadFunctions.EjectSpentCasing:
                yield return StartCoroutine(myReloadMechanics.EjectSpentCasing());
                break;

            case FirearmSO.ReloadFunctions.GoToPocketAndGrabNewAmmo:
                yield return StartCoroutine(myReloadMechanics.GoToPocketAndGrabNewAmmo());
                break;

            case FirearmSO.ReloadFunctions.InsertNewBullet:
                yield return StartCoroutine(myReloadMechanics.InsertNewAmmo());
                break;

            case FirearmSO.ReloadFunctions.OpenLoadingGate:
                yield return StartCoroutine(myReloadMechanics.OpenLoadingGate());
                break;

            case FirearmSO.ReloadFunctions.SpinCylinder:
                yield return StartCoroutine(myReloadMechanics.SpinCylinder(1));
                break;

            default:
                Debug.LogError("Error, reload Mechanic not set in Weapon Controller");
                break;

        }
        
    }


    
    void Shoot() 
    {
        if (Vector2.Distance(Cursor.transform.position, transform.position) < cantShootThisCloseFloat) return;

        if (CurrentFirearmAmmoCount > 0 && (LastShotTime <= Time.time - (fireRateItemBuff / FirerateHipFireMultiplier)))
        {   
            CurrentFirearmAmmoCount--;
            LastShotTime = Time.time;

            switch (CurrentFirearm.myShootType)
            {
                case FirearmSO.ShootTypes.Normal:
                    shootLibary.NormalProjectile(CurrentFirearm.BulletSprite, CurrentFirearm.ProjectileSprite, CurrentFirearm.ProjectileSpeed, CurrentFirearm.BaseAccuracy, CurrentFirearm.GunSmokeOnFire, CurrentFirearm.GunFlashOnFire, CurrentFirearm.ProjectileGO, FirePoint, currentAccuracy, currentDamageToDeal);
                    break;

                case FirearmSO.ShootTypes.Shotgun:
                    shootLibary.ShotgunShot(CurrentFirearm.BulletSprite, CurrentFirearm.ProjectileSprite,CurrentFirearm.ProjectilesOnFire, CurrentFirearm.ProjectileSpeed, CurrentFirearm.BaseAccuracy, CurrentFirearm.GunSmokeOnFire, CurrentFirearm.GunFlashOnFire, CurrentFirearm.ProjectileGO, FirePoint,CurrentFirearm.ShotgunSpread, currentAccuracy, currentDamageToDeal);
                    break;

                default:
                    Debug.LogError("Current Firearm Shoot type is invalid and not set up within the weapon controller");
                    break;

            }

        }

        else if (CurrentFirearmAmmoCount <= 0 && !reloading)
        {
            StartCoroutine(Reload());
        }



    }

    private void DropWeapon()
    {
        if (SecondaryFirearm == null) return;

        GameObject droppedWeapon = Instantiate(interactableWeaponPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        InteractableWeapon interactableWeapon = droppedWeapon.GetComponent<InteractableWeapon>();
        interactableWeapon.firearm = CurrentFirearm;
        interactableWeapon.roundsInFirearm = CurrentFirearmAmmoCount;
        droppedWeapon.GetComponent<Rigidbody2D>().AddForce(transform.localScale * weaponDropThrowingForce);
        CurrentFirearm = null;
        CurrentFirearmAmmoCount = 0;
        SwapWeapons();
    }


    private void PickUpNewWeapon()
    {
        if (!canPickUpWeapon) return;
        if (sObjectPickup.currentWeaponStoodUpon == null) return;

        if (SecondaryFirearm != null) DropWeapon();

        GameObject newFirearmGO = sObjectPickup.currentWeaponStoodUpon;
        InteractableWeapon newFirearmIW = newFirearmGO.GetComponent<InteractableWeapon>();

        SwapWeapons();
        SecondaryFirearm = newFirearmIW.firearm;
        SecondaryFirearmAmmoCount = newFirearmIW.roundsInFirearm;
        SwapWeapons();


        Destroy(newFirearmGO);
        return;

    }

    public void TogglePlayerAim(bool AimBool)
    {
        sWeaponAiming.CanAim = AimBool;
    }
}