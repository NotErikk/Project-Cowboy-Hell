using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown idleRollDirectionDropDown;
    private PlayerMovementScript playerMovementScript;
    
    void Awake()
    {
        DisplayCurrentValues();
        Debug.Log("Roll direction:"+ PlayerPrefs.GetInt(PlayerPrefsNames.idleRollDirection));
    }

    void DisplayCurrentValues()
    {
        switch (PlayerPrefs.GetInt(PlayerPrefsNames.idleRollDirection))
        {
            case 1: //forwards
                idleRollDirectionDropDown.value = 0;
                break;
            
            case -1: //backwards
                idleRollDirectionDropDown.value = 1;
                break;
            
        }
    }
    
    
    
    //Value Changed Funcs \/

    public void IdleRollDirectionChanged()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
            switch (idleRollDirectionDropDown.value)
            {
                case 0: //Backwards
                    playerMovementScript.idleRollDirection = 1;
                    PlayerPrefs.SetInt(PlayerPrefsNames.idleRollDirection, 1);
                    break;

                case 1: //Forwards
                    playerMovementScript.idleRollDirection = -1;
                    PlayerPrefs.SetInt(PlayerPrefsNames.idleRollDirection, -1);
                    break;
                default:
                    Debug.LogError("IdleRollDirection Setting Not set up within Controls.cs");
                    return;
            }
        }

        
    }
}
