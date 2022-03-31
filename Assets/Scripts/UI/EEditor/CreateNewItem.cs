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
            databaseManager.CreateNewItem() //HGAHAHHA YO UHAVE TO TYPE ALL THIS SHIT OUT HAHAHAHAHAHHAHA LFMAOOOO O HAVE FUN LAMFFOAOOOO LOLOLOLOL AHAHAHAHAH
        }
        catch
        {
            return;
        }

        gameObject.SetActive(false);
        editProfile.Button_Weapons();
        ClearInputs();
    }
}
