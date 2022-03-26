using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewWeapon : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Dropdown inputClass;
    [SerializeField] private TMP_Dropdown inputShotType;
    [SerializeField] private TMP_InputField inputAmmoCap;
    [SerializeField] private TMP_InputField inputProjectileSpeed;
    [SerializeField] private TMP_InputField inputAccuracy;
    [SerializeField] private TMP_InputField inputFireRate;
    [SerializeField] private Toggle inputTwoHanded;

    private DatabaseManager databaseManager;

    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
    }


    public void Button_CreateNewWeapon()
    {
        int twoHanded = inputTwoHanded.isOn ? 1 : 0;
        databaseManager.CreateNewWeapon(inputName.text, inputClass.value, Convert.ToInt32(inputAmmoCap.text),1, Convert.ToDouble(inputProjectileSpeed.text), Convert.ToDouble(inputAccuracy.text), Convert.ToDouble(inputFireRate.text), twoHanded, inputShotType.value);
    }

    public void Button_Close()
    {
        gameObject.SetActive(false);
        
        inputName.text = "";
        inputClass.value = 0;
        inputShotType.value = 0;
        inputAmmoCap.text = "";
        inputProjectileSpeed.text = "";
        inputAccuracy.text = "";
        inputFireRate.text = "";
        inputTwoHanded.isOn = false;
    }
}
