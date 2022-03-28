using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MenuColoursEnum;
using TMPro;
using UnityEngine.UI;

public class ColourAssign : MonoBehaviour
{
    [SerializeField] private MenuColours myType;
    

    private void Awake()
    {
        SetColour();
    }

    public void SetColour()
    {
        GetComponent<Image>().color = GameObject.FindGameObjectWithTag("ColourManager").GetComponent<ColourChanger>().GetMyColour(myType);
    }
}
