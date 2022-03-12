using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [Header("Map GameObjects")]
    [SerializeField] GameObject miniMapGameObjectInHUD;
    [SerializeField] GameObject bigMiniMapGameObject;

    [Header("Cameras")]
    [SerializeField] GameObject smallMapCamera;
    [SerializeField] GameObject bigMapCamera;

    [Header("Settings")]
    [SerializeField] float zoomSens;
    [SerializeField] float highestZoomRange;
    Camera cameraBigMapCamera;
    [HideInInspector] public bool allowedToUse;

    public void Start()
    {
        toggleMiniMap(true);
        toggleFullScreenMap(false);
        allowedToUse = true;
        cameraBigMapCamera = bigMapCamera.GetComponent<Camera>();
    }

    public void Update()
    {
        if (Input.GetButton("OpenMap") && allowedToUse)
        {
            toggleMiniMap(false);
            toggleFullScreenMap(true);

            float x = Input.GetAxisRaw("MouseScollWheel");

            float clamped = Mathf.Clamp(cameraBigMapCamera.orthographicSize -= x * zoomSens, 1, highestZoomRange);

            cameraBigMapCamera.orthographicSize = clamped;

        }
        else if (Input.GetButtonUp("OpenMap") && allowedToUse)
        {
            toggleMiniMap(true);
            toggleFullScreenMap(false);
        }
    }

    public void toggleMiniMap(bool toggle)
    {
        miniMapGameObjectInHUD.SetActive(toggle);
        smallMapCamera.SetActive(toggle);
    }

    public void toggleFullScreenMap(bool toggle)
    {
        bigMiniMapGameObject.SetActive(toggle);
        bigMapCamera.SetActive(toggle);
    }


}
