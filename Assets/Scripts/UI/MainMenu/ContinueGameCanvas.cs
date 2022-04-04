using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class ContinueGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject listObject;
    [SerializeField] private GameObject gameSavePrefab;

    private int selectedSave;
    
    public void FillListWithSaves()
    {
        
    }

    public void SetSelectedSave(int id)
    {
        selectedSave = id;
    }
}
