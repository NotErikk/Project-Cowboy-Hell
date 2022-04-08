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

    private GameObject deathScreenUi;
    
    private void Awake()
    {
        itemManager = GetComponent<ItemManager>();
        deathScreenUi = GameObject.FindGameObjectWithTag("DeathScreen");
        deathScreenUi.SetActive(false);
    }

    public void SetHealth(double newHealth)
    {
        currentHealth = (float)newHealth;
        maximumHealth = startingHealth;
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
    [ContextMenu("Kill Player")]
    void KillPlayer()
    {
        deathScreenUi.SetActive(true);
        deathScreenUi.GetComponent<DeathScreen>().DeathScreenSequence();

    }
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
    }

    public void UpdateExtraLives(int extraLives)
    {
        lives = extraLives;
    }
    
}
