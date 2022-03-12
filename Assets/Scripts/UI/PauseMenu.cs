using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] GameObject optionsMenuPrefab;
    [SerializeField] MiniMap miniMapScript;

    WeaponController weaponController;
    PlayerMovementScript movementScript;
    WeaponAiming weaponAiming;
    CursorIcon cursorIcon;

    bool isPaused;

    public void Start()
    {
        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>();
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
        weaponAiming = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponAiming>();
        cursorIcon = GameObject.FindGameObjectWithTag("PlayerMouse").GetComponent<CursorIcon>();
        optionsMenuPrefab.SetActive(false);
        PauseGame();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        pauseMenuCanvas.SetActive(isPaused);


        if (isPaused) //pause
        {
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //mini map
            miniMapScript.toggleFullScreenMap(false);
            miniMapScript.toggleMiniMap(false);
            miniMapScript.allowedToUse = false;

            toggleScripts(false);
        }
        else //unpause
        {
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            //minimap
            miniMapScript.toggleMiniMap(true);
            miniMapScript.allowedToUse = true;

            toggleScripts(true);
        }
    }
    private void toggleScripts(bool toggle)
    {
        movementScript.enabled = toggle;
        weaponController.enabled = toggle;
        weaponAiming.enabled = toggle;
        cursorIcon.enabled = toggle;
    }

    //pause menu button functionality
    public void ResumeButtonPressed()
    {
        isPaused = false;
        PauseGame();
    }

    public void OptionsButtonPressed()
    {
        optionsMenuPrefab.SetActive(!optionsMenuPrefab.active);
    }

    public void QuitToMenu()
    {

    }



}
