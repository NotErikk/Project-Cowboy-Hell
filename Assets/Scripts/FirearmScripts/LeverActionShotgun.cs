using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActionShotgun
{
    Vector2 MousePositionForLoop;

    public void Shoot(Sprite BulletSprite, Sprite ProjectileSprite, int ProjectilesOnFire, float ProjectileSpeed, float BaseAccuracy, bool GunSmokeOnFire, bool GunFlashOnFire, GameObject ProjecticleGO, GameObject ShootingPoint, float Spread, float AccuracyMultiplier)
    {
        Vector3 mousePos = GetMousePos(ShootingPoint); //get the mouse position and assign it the mousePos


        BaseAccuracy *= AccuracyMultiplier;
        BaseAccuracy = Mathf.Clamp(BaseAccuracy, 0f, 0.9f);
       
        DrawDebugAccuracy(ShootingPoint, mousePos, BaseAccuracy); //draws Debug.Log rays to visualise accuracy


        for (int i = 0; i < ProjectilesOnFire; i++)  //shoot as many projectiles as requested towards the cursor
        {

            GameObject instancedObj = GameObject.Instantiate(ProjecticleGO, ShootingPoint.transform.position, Quaternion.identity) as GameObject; //spawn new projectile
            instancedObj.GetComponent<SpriteRenderer>().sprite = ProjectileSprite; //set requested sprite to the projectile
            Rigidbody2D instObj = instancedObj.GetComponent<Rigidbody2D>(); //get the rigidbody component from the new projectile (for adding its velocity)


            //change mouse position based on accuracy
            MousePositionForLoop.x = mousePos.x + Random.Range(-BaseAccuracy, BaseAccuracy);
            MousePositionForLoop.y = mousePos.y + Random.Range(-BaseAccuracy, BaseAccuracy);


            Debug.DrawRay(ShootingPoint.transform.position, MousePositionForLoop, Color.cyan, 2, false); //debug.draw a ray for the projectile


            instObj.velocity = MousePositionForLoop.normalized * ProjectileSpeed; //sets velocity of this new projectile towards its new target


            //rotate this new projectile towards its target, it'll look like its looking towards the target flying towards it
            float lookrotation = Mathf.Atan2(MousePositionForLoop.y, MousePositionForLoop.x) * Mathf.Rad2Deg;
            instObj.transform.rotation = Quaternion.AngleAxis(lookrotation, Vector3.forward);
        }
    }

    private Vector3 GetMousePos(GameObject ShootingPoint) //Collects the mouse position and returns it as a Vector3
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mousePos + (Camera.main.transform.forward * 10.0f);
        return (mousePos - ShootingPoint.transform.position).normalized;
    }

    private void DrawDebugAccuracy(GameObject ShootingPoint, Vector3 mousePos, float BaseAccuracy)
    {
        Debug.DrawRay(ShootingPoint.transform.position, new Vector2(mousePos.x + BaseAccuracy, mousePos.y - BaseAccuracy).normalized * 50, Color.magenta, 3, false);
        Debug.DrawRay(ShootingPoint.transform.position, new Vector2(mousePos.x - BaseAccuracy, mousePos.y + BaseAccuracy).normalized * 50, Color.magenta, 3, false);
        Debug.DrawRay(ShootingPoint.transform.position, new Vector2(mousePos.x - BaseAccuracy, mousePos.y - BaseAccuracy).normalized * 50, Color.magenta, 3, false);
        Debug.DrawRay(ShootingPoint.transform.position, new Vector2(mousePos.x + BaseAccuracy, mousePos.y + BaseAccuracy).normalized * 50, Color.magenta, 3, false);
    }

    public void Reload(ref bool CanShoot)
    {
        CanShoot = true;
    }
}
