using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewWeapon : MonoBehaviour
{
    private GameObject newWeaponUi;

    private void Awake()
    {
        newWeaponUi = GameObject.FindGameObjectWithTag("NewWeaponUi");
    }

    public void Button_AddNewWeapon()
    {
            newWeaponUi.SetActive(true);
    }
}
