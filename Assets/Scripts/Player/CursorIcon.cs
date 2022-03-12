using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIcon : MonoBehaviour
{
    
    [SerializeField] private Camera MainCamera;

    private Vector3 CursorTransform;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }


    void Update()
    {
        CursorTransform = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        CursorTransform.z = 0;
        transform.position = CursorTransform;
    }
}
