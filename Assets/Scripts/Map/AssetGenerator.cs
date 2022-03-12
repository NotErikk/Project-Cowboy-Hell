using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetGenerator : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int coverAmountToSpawn;
    [SerializeField] int enemiesAmountToSpawn;

    [Header("Assignables")]
    [SerializeField] GameObject coverCollection;
    [SerializeField] GameObject enemyCollection;

    

    void Start()
    {
        int coverChildCount = coverCollection.transform.childCount;
        int enemyChildCount = enemyCollection.transform.childCount;

        DisableAllCoverAndEnemies(coverChildCount, enemyChildCount);
        SpawnCoverAndEnemyGameObjects(coverChildCount, enemyChildCount);
    }


    void DisableAllCoverAndEnemies(int coverChildCount, int enemyChildCount)
    {
        for (int i = 0; i < coverChildCount; i++)
        {
            coverCollection.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < enemyChildCount; i++)
        {
            enemyCollection.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void SpawnCoverAndEnemyGameObjects(int coverChildCount, int enemyChildCount)
    {
        for (int i = 0; i < coverAmountToSpawn; i++)
        {
            Transform spawnObject;
            do
            {
                spawnObject = coverCollection.transform.GetChild(Random.Range(0, coverChildCount));
            } while (spawnObject.gameObject.activeSelf);

            spawnObject.gameObject.SetActive(true);
        }

        for (int i = 0; i < enemiesAmountToSpawn; i++)
        {
            Transform spawnObject;
            do
            {
                spawnObject = enemyCollection.transform.GetChild(Random.Range(0, enemyChildCount));
            } while (spawnObject.gameObject.activeSelf);

            spawnObject.gameObject.SetActive(true);
        }
    }

    public int ReturnEnemyTotal()
    {
        return enemiesAmountToSpawn;
    }


}

