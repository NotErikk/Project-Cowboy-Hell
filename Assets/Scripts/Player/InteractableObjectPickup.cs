using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectPickup : MonoBehaviour
{
    public GameObject currentWeaponStoodUpon;
    public GameObject currentItemStoodUpon;


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableWeapon"))
        {
            currentWeaponStoodUpon = collision.gameObject;
            collision.GetComponent<InteractableWeapon>().textGO.SetActive(true);
        }
        if (collision.gameObject.CompareTag("InteractableItem"))
        {
            currentItemStoodUpon = collision.gameObject;
            collision.GetComponent<InteractableItem>().textGO.SetActive(true);
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableWeapon"))
        {
            currentWeaponStoodUpon = null;
            collision.GetComponent<InteractableWeapon>().textGO.SetActive(false);
        }
        if (collision.gameObject.CompareTag("InteractableItem"))
        {
            currentItemStoodUpon = null;
            collision.GetComponent<InteractableItem>().textGO.SetActive(false);
        }
    }
}
