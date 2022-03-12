using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleportDemoLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject TeleportLocation;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private string TextWithTutorial;

    [SerializeField]
    private string TextWithoutTutorial;

    private bool ShowingTutorial = false;

    private Text Textcomp;

    private void Start()
    {
        Textcomp = gameObject.GetComponent<Text>();
        TextWithTutorial = TextWithTutorial.Replace("\\n", "\n");
        TextWithoutTutorial = TextWithoutTutorial.Replace("\\n", "\n");
        Textcomp.text = TextWithoutTutorial;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            Player.transform.position = TeleportLocation.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (ShowingTutorial)
            {
                Textcomp.text = TextWithoutTutorial;
            }
            else
            {
                Textcomp.text = TextWithTutorial;
            }
            ShowingTutorial = !ShowingTutorial;
        }
    }
}
