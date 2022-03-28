using System;
using System.Collections;
using System.Collections.Generic;
using MenuColoursEnum;
using UnityEngine;
using Object = System.Object;

public class ColourChanger : MonoBehaviour
{
    public Color buttonColour;
    public Color panelColour;
    public Color innerPanelColour;
    public Color titleColour;

    [ContextMenu("Set All Ui Elements To Correct Colour")]
    private void RestAllColours()
    {
        ColourAssign[] items = (ColourAssign[]) FindObjectsOfType(typeof(ColourAssign));

        foreach (ColourAssign item in items)
        {
            item.SetColour();
        }
    }
    
    
    public Color GetMyColour(MenuColours type)
    {
        return type switch
        {
            MenuColours.buttonColour => buttonColour,
            MenuColours.panelColour => panelColour,
            MenuColours.innerPanelColour => innerPanelColour,
            MenuColours.title => titleColour,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
