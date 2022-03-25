using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewWeapon : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    
    public void Button_CreateNewWeapon()
    {
        
    }

    public void Button_Close()
    {
        gameObject.SetActive(false);
        inputName.text = "";
    }
}
