using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayAboveStabaliser : MonoBehaviour
{
    [SerializeField] Transform parent;


    public void Awake()
    {
        
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0); 
    }
}
