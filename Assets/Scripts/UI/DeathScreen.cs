using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private GameObject playerGO;
    private GameObject pauseMenuGO;
    private GameObject tabMenuGO;
    private GameObject playerMouse;
    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        pauseMenuGO = GameObject.FindGameObjectWithTag("PauseMenu");
        tabMenuGO = GameObject.FindGameObjectWithTag("tabMenu");
        playerMouse = GameObject.FindGameObjectWithTag("PlayerMouse");
    }
    
    public void DeathScreenSequence()
    {
        DisableOnDeath();
        DestroyAi();
        PlayerPrefs.SetInt(PlayerPrefsNames.currentLevel, 1);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    void DisableOnDeath()
    {
        playerGO.SetActive(false);
        pauseMenuGO.SetActive(false);
        tabMenuGO.SetActive(false);
        playerMouse.SetActive(false);
    }

    void DestroyAi()
    {
        foreach (GameObject ai in GameObject.FindGameObjectsWithTag("ai"))
        {
            Destroy(ai);
        }
    }
    
    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
