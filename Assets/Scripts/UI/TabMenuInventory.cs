using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TabMenuInventory : MonoBehaviour
{
    private ItemManager itemManager;
    [SerializeField] private GameObject uiIconPrefab, group;

    private List<string> spawnedItems = new List<string>();
    
    private void Awake()
    {
        itemManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemManager>();
    }

    [ContextMenu("update Item list")]
    public void UpdateItems()
    {
        if (itemManager.itemList.Count == 0) return;
        
        for(int i = 0; i < itemManager.itemList.Count;i++)
        {
            if (ListAlreadyHas(itemManager.itemList[i])) continue;
            
            spawnedItems.Add(itemManager.itemList[i].itemName);
            GameObject newIcon = Instantiate(uiIconPrefab, group.transform);
            newIcon.GetComponent<Image>().sprite = itemManager.itemList[i].itemSprite;
        }
    }


    bool ListAlreadyHas(ItemSO item)
    {
        return spawnedItems.Contains(item.itemName);
    }
}
