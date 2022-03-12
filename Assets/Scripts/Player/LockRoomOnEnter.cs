using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRoomOnEnter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomTrigger"))
        {
            collision.GetComponent<RoomStateManager>().EnterRoom();
        }
    }
}
