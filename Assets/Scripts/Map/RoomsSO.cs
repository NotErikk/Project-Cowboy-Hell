using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomTypes", menuName = "Custom/RoomTypes")]
public class RoomsSO : ScriptableObject
{
    public GameObject[] NRooms;
    public GameObject[] ERooms;
    public GameObject[] SRooms;
    public GameObject[] WRooms;
    public GameObject[] SpawnRooms;
    public GameObject[] EndRooms;
}
