using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    private int id;

    private WeaponPanel wepPanel;
    
    private void Awake()
    {
        wepPanel = GameObject.FindGameObjectWithTag("WeaponPanel").GetComponent<WeaponPanel>();
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void Button_ShowSettings()
    {
        wepPanel.ShowSettingsFromID(id);
    }
}
