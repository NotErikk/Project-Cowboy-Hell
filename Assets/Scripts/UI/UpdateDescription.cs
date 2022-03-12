using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpdateDescription : MonoBehaviour
{
    [SerializeField] private string description;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI titleTxt;


    private void Update()
    {
        OnMouseOver();
    }

    void OnMouseOver()
    {
        titleTxt.text = gameObject.name;
        descriptionTxt.text = description;
    }
}
