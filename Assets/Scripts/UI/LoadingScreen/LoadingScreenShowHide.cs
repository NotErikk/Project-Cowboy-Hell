using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenShowHide : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI loadingText;
    

    private void Start()
    {
        loadingText.text = "Generating World...";
    }

    public void FinishLoading()
    {
        Destroy(gameObject);
    }

}
