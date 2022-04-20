using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    [Header("Aiming Settings")]

    [Tooltip("At what range will the player begin hip firing")]
    [SerializeField]
    private float HipFireDistance;

    [Tooltip("how far should the arm be from origin when hip firing")]
    [SerializeField]
    private float HipFireDistanceArmRange;

    [Tooltip("When hip firing how inaccurate should the player be (equation: BaseAccuracy *= AccuracyMultiplier)")]
    [SerializeField]
    private float HipFireDebufAccuracy;

    [Tooltip("How much faster should the player shoot while hip firing? (equation: Firerate / FirerateMultiplier) lower the firerate the quicker")]
    [SerializeField]
    private float HipFireFirerateBuff;

    [Tooltip("how far should the arm be from origin when aiming normally")]
    public float NormalWeaponDistanceArmRange;

    [Header("Assignables")]

    [Tooltip("Origin Position for shooting normally")]
    public GameObject NormalFirePosition;

    [Tooltip("Origin Position for Hip Firing")]
    [SerializeField]
    private GameObject HipFirePosition;

    [Tooltip("Assign the Player Gameobject")]
    [SerializeField]
    private GameObject PlayerGO;

    [Tooltip("Assign WeaponController script which is on the player gameobject")]
    [SerializeField]
    private WeaponController WepController;

    [HideInInspector]
    public bool CanAim;

    [SerializeField]private GameObject laser;

    [SerializeField] GameObject firePoint;

    public void Awake()
    {
       laser.SetActive(false);
    }

   

    void Update()
    {
        if (!CanAim) return;
        
        //gets player mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mousePos + (Camera.main.transform.forward * 10.0f);
        mousePos = mousePos - PlayerGO.transform.position;

        //if the mouse is far away from the player shoot normally but if it's close hip fire (close and far depending on HipfireDistance)
        if (mousePos.magnitude <= HipFireDistance)
        {
            WepController.currentAccuracy = HipFireDebufAccuracy;
            WepController.FirerateHipFireMultiplier = HipFireFirerateBuff;

            mousePos = Vector3.ClampMagnitude(mousePos, HipFireDistanceArmRange);
            transform.position = mousePos + HipFirePosition.transform.position;
        }
        else
        {
            WepController.currentAccuracy = 1;
            WepController.FirerateHipFireMultiplier = 1;

            mousePos = Vector3.ClampMagnitude(mousePos, NormalWeaponDistanceArmRange);
            transform.position = mousePos + NormalFirePosition.transform.position;
        }

        //aims the weapon at the target
        float lookrotation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0f, 0f, lookrotation);
        // transform.
    }

    public void ToggleLaserPointer(bool toggle)
    {
        laser.SetActive(toggle);
    }
}
