using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class EndLevelOnEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("EndLevelPortal")) return;
        
        PlayerPrefs.SetInt(PlayerPrefsNames.currentLevel, PlayerPrefs.GetInt(PlayerPrefsNames.currentLevel) + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
