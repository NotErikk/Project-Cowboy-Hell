using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewItem : MonoBehaviour
{
    private GameObject newItemUi;

    private void Awake()
    {
        newItemUi = GameObject.FindGameObjectWithTag("editProfile").GetComponent<EditProfile>().GetNewItemUi();
    }

    public void Button_AddNewItem()
    {
        newItemUi.SetActive(true);
    }
}
