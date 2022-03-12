using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookPointScript : MonoBehaviour
{
    [SerializeField] private GameObject Cursor;
    [SerializeField] private GameObject PlayerAimPoint;
    [SerializeField] private float CameraFollowDistance;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(Cursor.transform.position, PlayerAimPoint.transform.position, CameraFollowDistance);
    }
}
