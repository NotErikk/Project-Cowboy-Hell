using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    [SerializeField] bool isTeleporterActive;
    [SerializeField] GameObject miniMapIcon;

    public void Start()
    {
        isTeleporterActive = false;
        miniMapIcon.SetActive(false);
    }

    public void ActivateTeleporter()
    {
        isTeleporterActive = true;
        miniMapIcon.SetActive(true);
    }

    public void buttonClick()
    {
        Debug.Log("click");
    }
}
