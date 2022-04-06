using System.Collections;
using System.Collections.Generic;
using AllWeaponInfoStruct;
using UnityEngine;
using UnityEngine.UI;

public class InteractableWeapon : MonoBehaviour
{
    [Header("Change this to change what weapon this represesents")]
    public FirearmSO firearm;
    [Header("Leave this alone, no need to change")]
    public GameObject textGO;
    public int roundsInFirearm;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] TMPro.TextMeshProUGUI myTextToChange;
    

    public void Start()
    {
        mySpriteRenderer.sprite = firearm.GunSprite;
        myTextToChange.text = firearm.DisplayName;
        textGO.SetActive(false);
    }
}
