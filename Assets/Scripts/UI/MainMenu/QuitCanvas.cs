using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCanvas : MonoBehaviour
{
    public void Button_QuitYes()
    {
        Application.Quit();
    }

    public void Button_QuitNo()
    {
        gameObject.SetActive(false);
    }
}
