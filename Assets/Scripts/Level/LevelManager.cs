using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [Range(1,6)]
    [SerializeField] private int currentLevel;

    [SerializeField] private bool forceCurrentLevel;
    
    [Tooltip("Min value, 1")]
    [SerializeField] private int roomsPerLevelLowerRange;
    
    [Tooltip("Min value 1")]
    [SerializeField] private int roomsPerLevelUpperRange;
    
    [SerializeField] private RoomsSO[] levels;

    private GeneratorEngine generatorEngine;
    private void Awake()
    {
        if (!forceCurrentLevel)
        {
            currentLevel = PlayerPrefs.GetInt(PlayerPrefsNames.currentLevel);
        }
        if (currentLevel != 0) currentLevel--;     
        
        if (roomsPerLevelLowerRange < 1) roomsPerLevelLowerRange = 1;
        if (roomsPerLevelUpperRange < 1) roomsPerLevelUpperRange = 1;

        generatorEngine = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<GeneratorEngine>();

        generatorEngine.RoomsAvailable = levels[currentLevel];
        generatorEngine.TotalRooms = Random.Range(roomsPerLevelLowerRange, roomsPerLevelUpperRange);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
