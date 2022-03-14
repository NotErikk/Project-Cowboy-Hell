using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractableObjectPickup : MonoBehaviour
{
    public GameObject currentWeaponStoodUpon;
    public GameObject currentItemStoodUpon;
    public GameObject currentLootBoxStoodUpon;

    [SerializeField] bool interacting;

    private void Update()
    {
        interacting = Input.GetButton("Interact");
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableWeapon"))
        {
            currentWeaponStoodUpon = collision.gameObject;
            collision.GetComponent<InteractableWeapon>().textGO.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("InteractableItem"))
        {
            currentItemStoodUpon = collision.gameObject;
            collision.GetComponent<InteractableItem>().textGO.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("InteractableLootBox"))
        {
            currentLootBoxStoodUpon = collision.gameObject;
            if (interacting)
            {
                currentLootBoxStoodUpon.GetComponent<LootBox>().OpenBox();
            }
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableWeapon"))
        {
            currentWeaponStoodUpon = null;
            collision.GetComponent<InteractableWeapon>().textGO.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("InteractableItem"))
        {
            currentItemStoodUpon = null;
            collision.GetComponent<InteractableItem>().textGO.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("InteractableLootBox"))
        {
            currentLootBoxStoodUpon = null;
        }
    }
}
