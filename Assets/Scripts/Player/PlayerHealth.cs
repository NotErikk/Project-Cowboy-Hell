using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float maximumHealth;

    private float currentHealth;
    int lives;
    private ItemManager itemManager;
    
    private void Awake()
    {
        currentHealth = startingHealth;
        itemManager = GetComponent<ItemManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Took " + damage + " damage, current health = " + currentHealth);
        
        if (currentHealth <= 0)
        {
            ExtraLifeCheck();
        }
    }

    private void ExtraLifeCheck()
    {
        if (lives <= 0) KillPlayer();
        else RevivePlayer();
    }

    void RevivePlayer()
    {
        lives--;
        itemManager.extraLives--;
        currentHealth = maximumHealth;
        Debug.Log("Revive Player");
    }
    void KillPlayer()
    {
        Debug.Log("kill Player");
    }
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
    }

    public void UpdateExtraLives(int extraLives)
    {
        lives = extraLives;
        Debug.Log("Gained a life!");
    }
    
}
