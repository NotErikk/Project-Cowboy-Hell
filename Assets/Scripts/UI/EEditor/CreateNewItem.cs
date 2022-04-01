using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewItem : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputItemName;
    [SerializeField] private TMP_Dropdown inputTier;
    [SerializeField] private TMP_InputField inputBriefDescription;
    [SerializeField] private TMP_InputField inputDescription;
    
    private DatabaseManager databaseManager;
    private EditProfile editProfile;
    
    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        editProfile = GetComponentInParent<EditProfile>();
    }
    
    public void Button_CreateNewItem()
    {
        try
        {
            databaseManager.CreateNewItem(inputItemName.text, inputTier.value, "", inputBriefDescription.text, inputDescription.text, 0, 0, 0, 0, 0.0, 0.0, 0.0, 0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0,0.0);
        }
        catch
        {
            return;
        }

        gameObject.SetActive(false);
        editProfile.Button_Weapons();
        ClearInputs();
    }
    
    public void Button_Close()
    {
        gameObject.SetActive(false);
        
        ClearInputs();
    }

    private void ClearInputs()
    {
        inputItemName.text = "";
        inputTier.value = 0;
        inputBriefDescription.text = "";
        inputDescription.text = "";
    }
}
