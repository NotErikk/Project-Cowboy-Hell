using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimOrigin : MonoBehaviour
{
    
    [SerializeField] private GameObject Cursor;
    [SerializeField] private GameObject AimOrigin;
    
    void Update()
    {
        Debug.DrawLine(AimOrigin.transform.position, Cursor.transform.position + AimOrigin.transform.forward);
       
    }
}
