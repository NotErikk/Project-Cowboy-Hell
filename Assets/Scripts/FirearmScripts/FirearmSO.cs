using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddNewFirearm", menuName = "Custom/AddNewFirearm")]
public class FirearmSO : ScriptableObject
{
    public enum FirearmClassesAvailable
    {
        //Revolvers
        revolver,

        //Pistols
        pistol,

        //Shotguns
        shotgun,

        //LeverActions
        rifle
    };
    public enum ShootTypes 
    { 
        Normal,
        Shotgun
    }


    
    [Header("Sprites and Sounds")]

    [Tooltip("Assign Gun Sprite")]
    public Sprite GunSprite;

    [Tooltip("Assign BulletSprite(THIS IS NOT THE PROJECTILE, this will be put into the gun)")]
    public Sprite BulletSprite;

    [Tooltip("bullet casing only use if needed")]
    public Sprite CasingSprite;

    [Tooltip("Assign projectile sprite (if wrong way round re-import art rotated)")]
    public Sprite ProjectileSprite;

    [Tooltip("use list to add all varients of shooting sounds")]
    [SerializeField]
    private AudioClip[] ShootingSounds;

    [Tooltip("use list to add all reload sfx varients")]
    [SerializeField]
    private AudioClip[] ReloadingSounds;
    


    [Header("Firearm Statistics")]

    [Tooltip("Name of the gun displayed to the player")]
    public string DisplayName;

    [Tooltip("Choose the Type of firearm you are adding")]
    public FirearmClassesAvailable firearmType;

    [Tooltip("Tier")]
    public Tiers.tier myTier;

    [Tooltip("Base Damage of wep, per projectile")]
    public float damage;

    [Tooltip("How many shots does this weapon have (only 'Magazine' capacity, in chamber storage is another variable)")]
    [SerializeField]
    public int BulletCapacity;

    [Tooltip("How many projectile on fire?  mostly for shotguns")]
    public int ProjectilesOnFire;

    [Tooltip("Speed of projectile")]
    public int ProjectileSpeed;

    [Tooltip("Normal Shooting Accuracy.  30 = 30 degrees of inaccuracy")]
    public float BaseAccuracy;

    [Tooltip("Normal Firerate of this gun, shots per second")]
    public float Firerate;

    [Tooltip("How many bullets in chamber can be hold, e.g. a standard m4 mag would be 30 + 1 so 1 in the chamber.")]
    [SerializeField]
    private int HoldBulletInChamber;

    [Tooltip("If someting like a breach load shotgun do the shells auto edject on break")]
    [SerializeField]
    private bool AutoEjectingOnBreach;

    [Tooltip("Do cartridges edject from the gun on trigger pull")]
    [SerializeField]
    private bool EjectingCartridgesOnFire;

    [Tooltip("Is there gun smoke on trigger pull")]
    public bool GunSmokeOnFire;

    [Tooltip("is there a flash on trigger pull")]
    public bool GunFlashOnFire;

    [Tooltip("Amount of shotgun spread")]
    public float ShotgunSpread;

    [Tooltip("Does the gun need 2 hands to shoot")]
    public bool TwoHanded;

    [Tooltip("Angle of weapon while reloading, 0 = perfectly straight.  -20 is 20 degrees pointed down")]
    public float WeaponReloadAngle;

    public ShootTypes myShootType;
    
    
    [Header("Firearm Locations")]

    [Tooltip("local Coordinates for where the gun will be held by the dominant hand")]
    public Vector2 ShootingHandHoldingPoint;

    [Tooltip("local Coordinates for where the player will get new bullets")]
    public Vector2 GetAmmoHoldingPoint;

    [Tooltip("local Coordinates for where projectiles spawn out of the gun")]
    public Vector2 FirePointCords;

    [Tooltip("local Coordinates for where the players off hand will hold the gun if it's two handed")]
    public Vector2 OtherHandHoldingPoint;

    [Tooltip("local Coordinates for where the player will load bullets in the gun")]
    public Vector2 LoadBulletsPoint;

    [Tooltip("local Coordinates for where the guns forward cocking position is")]
    public Vector2 CockingPointNeutral;

    [Tooltip("local Coordinates for where the guns back cocking position is")]
    public Vector2 CockingPulledBack;

    [Tooltip("used for modern magasine releases or old timey bullet ejectors")]
    public Vector2 BulletReleaseKey;

    [Tooltip("used for revolvers, locate where the cylinder is")]
    public Vector2 RevolverCylinderLocation;

    [Tooltip("prefab for the projectile being shot")]
    public GameObject ProjectileGO;



    //Reload
    public enum ReloadFunctions 
    {
        OpenLoadingGate,
        BringHammerHalfCock,
        SpinCylinder,
        EjectSpentCasing,
        GoToPocketAndGrabNewAmmo,
        InsertNewBullet,
        CloseLoadingGate,
        BringHammerToFullCock
    };


    [Header("Reload")]
    public ReloadFunctions[] ReloadSetUpActions;
    public ReloadFunctions[] MainReloadActions;
    public ReloadFunctions[] ReloadFinishedActions;

}







