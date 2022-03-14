using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetGenerator : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int coverAmountToSpawn;
    [SerializeField] int enemiesAmountToSpawn;
    [SerializeField] private int spawnLootChance;

    [Header("Assignables")]
    [SerializeField] GameObject coverCollection;
    [SerializeField] GameObject enemyCollection;
    [SerializeField] GameObject lootCollection;

    
    void Start()
    {
        int coverChildCount = coverCollection.transform.childCount;
        int enemyChildCount = enemyCollection.transform.childCount;
        int lootChildCount = lootCollection.transform.childCount;
        
        DisableAll(coverChildCount, enemyChildCount, lootChildCount);
        SpawnAssets(coverChildCount, enemyChildCount, lootChildCount);
    }
    
    
    void DisableAll(int coverChildCount, int enemyChildCount, int lootChildCount)
    {
        for (int i = 0; i < coverChildCount; i++)
        {
            coverCollection.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < enemyChildCount; i++)
        {
            enemyCollection.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        for (int i = 0; i < lootChildCount; i++)
        {
            lootCollection.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void SpawnAssets(int coverChildCount, int enemyChildCount, int lootChildCount)
    {
        //cover
        for (int i = 0; i < coverAmountToSpawn; i++)
        {
            Transform spawnObject;
            do
            {
                spawnObject = coverCollection.transform.GetChild(Random.Range(0, coverChildCount));
            } while (spawnObject.gameObject.activeSelf);

            spawnObject.gameObject.SetActive(true);
        }

        //enemies
        for (int i = 0; i < enemiesAmountToSpawn; i++)
        {
            Transform spawnObject;
            do
            {
                spawnObject = enemyCollection.transform.GetChild(Random.Range(0, enemyChildCount));
            } while (spawnObject.gameObject.activeSelf);

            spawnObject.gameObject.SetActive(true);
        }
        
        //loot
        if (Random.Range(0, 100) <= spawnLootChance)
        {
            lootCollection.transform.GetChild(Random.Range(0, lootChildCount)).gameObject.SetActive(true);
        }
    }

    public int ReturnEnemyTotal()
    {
        return enemiesAmountToSpawn;
    }


}

