using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratorEngine : MonoBehaviour
{
    [HideInInspector] public int TotalRooms;
    List<GameObject> spawnPoints = new List<GameObject>(); //list to hold all spawnpoints
    [HideInInspector] public RoomsSO RoomsAvailable;
    public GameObject StartingPoint;
    int Counter = 0;
    PlayerMovementScript playerMovementScript;
    [SerializeField] private LoadingScreenShowHide loadingScreen;

    private void Awake()
    {
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
    }

    public void Start()
    {
        GameObject StartingRoom = RoomsAvailable.SpawnRooms[Random.Range(0, RoomsAvailable.SpawnRooms.Length)];
        Instantiate(StartingRoom, new Vector3(StartingPoint.transform.position.x, StartingPoint.transform.position.y, 0), Quaternion.identity);
    }

    public void FixedUpdate()
    {

        if (Counter < TotalRooms)
        {
            GameObject UseThisSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            
            string SpawnType = UseThisSpawnPoint.name;

            GameObject RandomRoom = null;
            switch (SpawnType)
            {
                case "N":
                    RandomRoom = RoomsAvailable.SRooms[Random.Range(0, RoomsAvailable.SRooms.Length)];
                    break;
                case "E":
                    RandomRoom = RoomsAvailable.WRooms[Random.Range(0, RoomsAvailable.WRooms.Length)];
                    break;
                case "S":
                    RandomRoom = RoomsAvailable.NRooms[Random.Range(0, RoomsAvailable.NRooms.Length)];
                    break;
                case "W":
                    RandomRoom = RoomsAvailable.ERooms[Random.Range(0, RoomsAvailable.ERooms.Length)];
                    break;

            }

            Instantiate(RandomRoom, new Vector3(UseThisSpawnPoint.transform.position.x, UseThisSpawnPoint.transform.position.y, 0), Quaternion.identity);
            Counter++;
            return;
        }

        else if (Counter >= TotalRooms)
        {

            List<GameObject> EndRoomLocations = new List<GameObject>();
            foreach (GameObject spawnPoint in spawnPoints)
            {
                if (spawnPoint.name == "E")
                {
                    EndRoomLocations.Add(spawnPoint);
                }
            }
            if (EndRoomLocations.Count <= 0)
            {
                ResetRoomGeneration();
                return;
            }
            GameObject UseThisSpawnPoint = EndRoomLocations[Random.Range(0, EndRoomLocations.Count)];

            Instantiate(RoomsAvailable.EndRooms[Random.Range(0, RoomsAvailable.EndRooms.Length)], new Vector3(UseThisSpawnPoint.transform.position.x, UseThisSpawnPoint.transform.position.y, 0), Quaternion.identity);
            playerMovementScript.GetBridgeList();

            
            foreach (GameObject spawnPoint in GameObject.FindGameObjectsWithTag("SpawnPoint"))
            {
                Destroy(spawnPoint);
            }
            Debug.Log("Generation for " + TotalRooms + " rooms, took a total of " + (Time.timeSinceLevelLoad) + " Seconds");
            Destroy(StartingPoint);
            Destroy(gameObject);
        }
    }


    void ResetRoomGeneration()
    {
        spawnPoints.Clear();
        GameObject[] allRooms = GameObject.FindGameObjectsWithTag("GeneratedRoom");

        foreach (GameObject room in allRooms)
        {
            Destroy(room);
        }
        Counter = 1;

        GameObject StartingRoom = RoomsAvailable.SpawnRooms[Random.Range(0, RoomsAvailable.SpawnRooms.Length)];
        Instantiate(StartingRoom, new Vector3(StartingPoint.transform.position.x, StartingPoint.transform.position.y, 0), Quaternion.identity);

        Debug.Log("Reset World gen");
    }

    public void RegisterSpawnPoint(GameObject spawnPoint)
    {
        spawnPoints.Add(spawnPoint);
    }
    public void DeRegisterSpawnPoint(GameObject spawnPoint)
    {
        spawnPoints.Remove(spawnPoint);
    }

    private void OnDestroy()
    {
        loadingScreen.FinishLoading();
    }
}
