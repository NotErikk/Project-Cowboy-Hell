using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [Header("Change this to change what item this represesents")]
    public ItemSO item;
    [Header("Leave this alone, no need to change")]
    public GameObject textGO;
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] TMPro.TextMeshProUGUI myTextToChange;


    public void Start()
    {
        mySpriteRenderer.sprite = item.itemSprite;
        myTextToChange.text = item.itemName;
        textGO.SetActive(false);
    }
}
