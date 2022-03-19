using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameCanvasManager : MonoBehaviour
{
    public void Button_NewGameStart()
    {
        SceneManager.LoadScene(1);
    }
}
