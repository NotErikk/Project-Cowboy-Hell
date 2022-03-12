using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealthManager : MonoBehaviour
{
    [SerializeField] float health;
    public GameObject myRoom;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            TakeDamage(collision.collider.GetComponent<projectileScript>().damage);
        }
    }

    void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            if (myRoom != null) myRoom.GetComponent<RoomStateManager>().EnemyDied();
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomTrigger"))
        {
            myRoom = collision.gameObject;
        }
    }
}
