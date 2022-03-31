using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItemConfirm : MonoBehaviour
{
    private DatabaseManager databaseManager;
    private int currentID;
    private EditProfile editProfile;
    
    public void SetID(int id) => currentID = id;
    
    private void Awake()
    {
        databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        editProfile = GameObject.FindGameObjectWithTag("editProfile").GetComponent<EditProfile>();
    }

    public void Button_DeleteConfirm()
    {
        databaseManager.DeleteItem(currentID);
        
        editProfile.Button_Items();
        gameObject.SetActive(false);
    }

    public void Button_CancelDelete()
    {
        gameObject.SetActive(false);
    }
}
