using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLibary
{

    public void NormalProjectile(Sprite BulletSprite, Sprite ProjectileSprite, float ProjectileSpeed, float BaseAccuracy, bool GunSmokeOnFire, bool GunFlashOnFire, GameObject ProjecticleGO, GameObject ShootingPoint, float AccuracyMultiplier, float damage)
    {
        var mousePosition = GetMousePosition();
        float myAngle = ShootingPoint.transform.eulerAngles.z;

        BaseAccuracy *= AccuracyMultiplier;
        myAngle += Random.Range(-BaseAccuracy / 2, BaseAccuracy / 2);


        GameObject instancedObj = GameObject.Instantiate(ProjecticleGO, ShootingPoint.transform.position, Quaternion.Euler(0f, 0f, myAngle)) as GameObject;
        instancedObj.GetComponent<SpriteRenderer>().sprite = ProjectileSprite;

        Debug.DrawRay(ShootingPoint.transform.position, instancedObj.transform.right * 15f, Color.green, 3f);

        instancedObj.GetComponent<Rigidbody2D>().velocity = instancedObj.transform.right * ProjectileSpeed;
        instancedObj.GetComponent<projectileScript>().damage = damage;

    }

    public void ShotgunShot(Sprite BulletSprite, Sprite ProjectileSprite, int ProjectilesOnFire, float ProjectileSpeed, float BaseAccuracy, bool GunSmokeOnFire, bool GunFlashOnFire, GameObject ProjecticleGO, GameObject ShootingPoint, float Spread, float AccuracyMultiplier, float damage)
    {
        var mousePosition = GetMousePosition();
        float myAngle = ShootingPoint.transform.eulerAngles.z;

        BaseAccuracy *= AccuracyMultiplier;
        float aimingPoint = myAngle + Random.Range(-BaseAccuracy / 2, BaseAccuracy / 2);


        Debug.DrawRay(ShootingPoint.transform.position, (mousePosition - ShootingPoint.transform.position).normalized * 15, Color.red, 3f);

        for (int i = 0; i < ProjectilesOnFire; i++)
        {
            myAngle = aimingPoint + Random.Range(-Spread / 2, Spread / 2);

            GameObject instancedObj = GameObject.Instantiate(ProjecticleGO, ShootingPoint.transform.position, Quaternion.Euler(0f, 0f, myAngle)) as GameObject;
            instancedObj.GetComponent<SpriteRenderer>().sprite = ProjectileSprite;

            Debug.DrawRay(ShootingPoint.transform.position, instancedObj.transform.right * 15f, Color.green, 3f);

            instancedObj.GetComponent<Rigidbody2D>().velocity = instancedObj.transform.right * ProjectileSpeed;
            instancedObj.GetComponent<projectileScript>().damage = damage;
        }
    }


    private Vector3 GetMousePosition() //Collects the mouse position and returns it as a Vector3
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mousePos + (Camera.main.transform.forward * 10.0f);
        return mousePos;
    }
}
