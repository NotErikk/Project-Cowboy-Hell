using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadMechanics
{
    WeaponAiming sWeaponAiming;
    public Transform WeaponTransform, ReloadPosition, Self, Cursor, SecondArmTransform, PlayerPocket;
    GameObject BackArm, SpentCasingPrefab;
    FirearmSO CurrentFirearm;
    WeaponController WepController;
    float ReloadSpeedBuff, WeaponReloadAngle;



    public ReloadMechanics(WeaponAiming sWeaponAiming, Transform WeaponTransform, FirearmSO CurrentFirearm, Transform ReloadPosition, Transform Self, Transform Cursor, float ReloadSpeedBuff, Transform SecondArmTransform, GameObject SpentCasingPrefab, Transform PlayerPocket, GameObject BackArm, float WeaponReloadAngle, WeaponController WepController)
    {
        this.sWeaponAiming = sWeaponAiming;

        this.WeaponTransform = WeaponTransform;
        this.ReloadPosition = ReloadPosition;
        this.Self = Self;
        this.Cursor = Cursor;
        this.SecondArmTransform = SecondArmTransform;
        this.PlayerPocket = PlayerPocket;

        this.CurrentFirearm = CurrentFirearm;

        this.ReloadSpeedBuff = ReloadSpeedBuff;
        this.WeaponReloadAngle = WeaponReloadAngle;

        this.BackArm = BackArm;
        this.SpentCasingPrefab = SpentCasingPrefab;

        this.WepController = WepController;
    }

    public void ToggleSecondHandVisible(bool toggle)
    {
        BackArm.SetActive(toggle);
    }


    public void ToggleReloadLocks(ref bool Reloading, ref bool CanSwapWeapon, bool Toggle)
    {
        Toggle = !Toggle;
        TogglePlayerAim(Toggle);
        CanSwapWeapon = Toggle;

        Reloading = !Toggle;

        if (!Toggle)
        {
            WeaponTransform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            WeaponTransform.localScale = new Vector3(-1f, -1f, 1f);
        }
    }


    private void TogglePlayerAim(bool CanIAimBool)
    {
        sWeaponAiming.CanAim = CanIAimBool;
    }

    public IEnumerator BringGunCloseToPlayer()
    {
        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingPosition = WeaponTransform.transform;


        if (Cursor.position.x > Self.position.x)
        {
            WeaponTransform.rotation = Quaternion.AngleAxis(WeaponReloadAngle, Vector3.forward);
        }
        else
        {
            WeaponTransform.rotation = Quaternion.AngleAxis(-WeaponReloadAngle, Vector3.forward);
        }
        while (ratio <= 1f)
        {
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            WeaponTransform.position = Vector3.Slerp(StartingPosition.position, ReloadPosition.position, ratio);
        }
        Debug.Log("BringGunClose");
        yield return null;
    }

    public IEnumerator OpenLoadingGate()
    {
        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingTransform = SecondArmTransform;

        while (ratio <= 1f)
        {
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            SecondArmTransform.localPosition = Vector3.Lerp(StartingTransform.localPosition, CurrentFirearm.LoadBulletsPoint, ratio);
            yield return null;
        }

        Debug.Log("OpenLoadGate");
        yield return null;
    }

    public IEnumerator BringHammerToHalfCock()
    {
        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingPosition = SecondArmTransform;

        while (ratio <= 1f)
        {
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            SecondArmTransform.localPosition = Vector3.Lerp(StartingPosition.localPosition, CurrentFirearm.CockingPointNeutral, ratio);
            yield return null;
        }

        Debug.Log("BringHammerToHalfCock");
        yield return null;
    }

    public IEnumerator SpinCylinder(int Spins)
    {
        for (int i = 0; i < Spins; i++)
        {
            Debug.Log("SpinCylinder");

            float ElapsedTime = 0;
            float duration = (1 * ReloadSpeedBuff) / 2;
            float ratio = ElapsedTime / duration;

            Transform StartingPosition = SecondArmTransform;

            while (ratio <= 1f)
            {
                ElapsedTime += Time.deltaTime;
                ratio = ElapsedTime / duration;
                SecondArmTransform.localPosition = Vector3.Lerp(StartingPosition.localPosition, (CurrentFirearm.RevolverCylinderLocation + new Vector2(0, -0.05f)), ratio);
                yield return null;
            }
            ElapsedTime = 0;
            ratio = ElapsedTime / duration;
            while (ratio <= 1f)
            {
                ElapsedTime += Time.deltaTime;
                ratio = ElapsedTime / duration;
                SecondArmTransform.localPosition = Vector3.Lerp(StartingPosition.localPosition, (CurrentFirearm.RevolverCylinderLocation + new Vector2(0, 0.05f)), ratio);
                yield return null;
            }

        }

        yield return null;
    }

    public IEnumerator EjectSpentCasing()
    {
        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingPosition = SecondArmTransform;

        while (ratio <= 1f)
        {
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            SecondArmTransform.localPosition = Vector3.Lerp(StartingPosition.localPosition, CurrentFirearm.BulletReleaseKey, ratio);
            yield return null;
        }

        Vector2 SpawnPosition = Self.TransformPoint(CurrentFirearm.LoadBulletsPoint);
        GameObject SpentCasing = GameObject.Instantiate(SpentCasingPrefab, SpawnPosition, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360))) as GameObject;
        SpentCasing.GetComponent<SpriteRenderer>().sprite = CurrentFirearm.CasingSprite;

        Debug.Log("EjectSpentCasing");
        yield return null;
    }

    public IEnumerator GoToPocketAndGrabNewAmmo()
    {
        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingPosition = SecondArmTransform;

        while (ratio <= 1f)
        {
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            SecondArmTransform.position = Vector3.Lerp(StartingPosition.position, PlayerPocket.position, ratio);
            yield return null;
        }

        Debug.Log("GoToPocket");
        yield return null;
    }



    public IEnumerator InsertNewAmmo()
    {
        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingPosition = SecondArmTransform;

        while (ratio <= 1f)
        {
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            SecondArmTransform.localPosition = Vector3.Lerp(StartingPosition.localPosition, CurrentFirearm.LoadBulletsPoint, ratio);
            yield return null;
        }

        
        
        WepController.CurrentFirearmAmmoCount++;
        Debug.Log("insertNewBullet");
        yield return null;
    }

    public IEnumerator CloseLoadingGate()
    {
        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingPosition = SecondArmTransform;

        while (ratio <= 1f)
        {
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            SecondArmTransform.localPosition = Vector3.Lerp(StartingPosition.localPosition, CurrentFirearm.LoadBulletsPoint, ratio);
            yield return null;
        }

        Debug.Log("CloseLoadingGate");
        yield return null;
    }

    public IEnumerator BringHammerToFullCock()
    {
        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingPosition = SecondArmTransform;

        while (ratio <= 1f)
        {
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            SecondArmTransform.localPosition = Vector3.Lerp(StartingPosition.localPosition, CurrentFirearm.CockingPointNeutral, ratio);
            yield return null;
        }

        Debug.Log("BringHammerToFullCock");
        yield return null;
    }

    public IEnumerator ReturnToShootingPosition()
    {
        Debug.Log("put gun back to shooting position");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mousePos + (Camera.main.transform.forward * 10.0f);
        mousePos = mousePos - Self.position;

        mousePos = Vector3.ClampMagnitude(mousePos, sWeaponAiming.NormalWeaponDistanceArmRange);

        float ElapsedTime = 0;
        float duration = 1 * ReloadSpeedBuff;
        float ratio = ElapsedTime / duration;

        Transform StartingPosition = WeaponTransform;

        float lookrotation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;


        if (Cursor.transform.position.x > Self.position.x)
        {
            WeaponTransform.localScale = new Vector3(1f, 1, 1);
        }
        else
        {
            WeaponTransform.localScale = new Vector3(-1f, -1, 1);
        }

        WeaponTransform.rotation = Quaternion.AngleAxis(lookrotation, Vector3.forward);

        while (ratio <= 1f)
        {
            Vector2 Target = mousePos + sWeaponAiming.NormalFirePosition.transform.position;
            ElapsedTime += Time.deltaTime;
            ratio = ElapsedTime / duration;
            WeaponTransform.position = Vector3.Lerp(WeaponTransform.position, Target, ratio);

            yield return null;
        }

        yield return null;
    }
}
