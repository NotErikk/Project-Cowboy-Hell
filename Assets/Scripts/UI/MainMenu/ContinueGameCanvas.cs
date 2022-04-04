using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicGameSaveInfoStruct;
using UnityEngine.SceneManagement;

namespace BasicGameSaveInfoStruct
{
    public struct GameSaveInfo
    {
        public int saveID;
        public string saveName;
        public int saveProfileID;

        public GameSaveInfo(int saveID, string saveName, int saveProfileID)
        {
            this.saveID = saveID;
            this.saveName = saveName;
            this.saveProfileID = saveProfileID;
        }
    }
}



public class ContinueGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject listObject;
    [SerializeField] private GameObject gameSavePrefab;

    private DatabaseManager databaseManager;
    
    private int selectedSave;

    [ContextMenu("Fill List")]
    public void RefreshSaveDisplayList()
    {
        if (databaseManager == null) databaseManager = GameObject.FindGameObjectWithTag("DatabaseManager").GetComponent<DatabaseManager>();
        
        ClearList();
        
        var listOfAllSaveInfo = databaseManager.GetListOfAllGameSavesBasicInfo();

        foreach (var save in listOfAllSaveInfo)
        {
            GameObject newSaveButton = Instantiate(gameSavePrefab, listObject.transform, true);
            newSaveButton.GetComponent<GameSavePrefab>().SetID(save);
        }
    }

    private void ClearList()
    {
        foreach (var listItem in listObject.GetComponentsInChildren<Transform>())
        {
            if (listItem.gameObject == listObject) continue;
            
            Destroy(listItem.gameObject);
        }
    }
    
    
    public void SetSelectedSave(int id)
    {
        selectedSave = id;
    }

    public void Button_Play()
    {
        SceneManager.LoadScene(1);
    }
}
