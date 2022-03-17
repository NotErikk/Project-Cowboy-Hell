using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : Node
{
    ShootingBrain brain;
    FirearmSO currentFirearm;
    ShootLibary shootLibary;

    public ShootAtPlayer(ShootingBrain brain, FirearmSO currentFirearm)
    {
        this.brain = brain;
        this.currentFirearm = currentFirearm;
        shootLibary = new ShootLibary();
    }

    public override NodeState Evaluate()
    {
        switch (currentFirearm.myShootType)
            {
                case FirearmSO.ShootTypes.Normal:
                    shootLibary.NormalProjectile(currentFirearm.BulletSprite, currentFirearm.ProjectileSprite, currentFirearm.ProjectileSpeed, currentFirearm.BaseAccuracy, currentFirearm.GunSmokeOnFire, currentFirearm.GunFlashOnFire, currentFirearm.ProjectileGO, brain.firePoint, 1, 20, 17);
                    break;

                case FirearmSO.ShootTypes.Shotgun:
                    shootLibary.ShotgunShot(currentFirearm.BulletSprite, currentFirearm.ProjectileSprite,currentFirearm.ProjectilesOnFire, currentFirearm.ProjectileSpeed, currentFirearm.BaseAccuracy, currentFirearm.GunSmokeOnFire, currentFirearm.GunFlashOnFire, currentFirearm.ProjectileGO, brain.firePoint,currentFirearm.ShotgunSpread, 1, 20/currentFirearm.ProjectilesOnFire, 17);
                    break;

                default:
                    Debug.LogError("Current Firearm Shoot type is invalid and not set up within the weapon controller");
                    break;
            }
        
        
        
        brain.timeSinceLastShot = Time.time;
        return NodeState.Success;
    }
}
