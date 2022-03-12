using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerInfoHUD : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Rigidbody2D PlayerRB;
    private Text InfoText;
    private float AverageFPS;
    private int CurrentFPS;
    private int FrameCounter = 0;
    private PlayerMovementScript SPlayer;

    public void Start()
    {
        InfoText = GetComponent<Text>();
        PlayerRB = Player.GetComponent<Rigidbody2D>();
        SPlayer = Player.GetComponent<PlayerMovementScript>();
    }

    void Update()
    {
        FrameCounter++;

        CurrentFPS = (int)(1f / Time.unscaledDeltaTime);
        AverageFPS += (CurrentFPS - AverageFPS)/FrameCounter;
        InfoText.text = "-----INFO-----";
        InfoText.text += "\nVelocity: " + PlayerRB.velocity.ToString();
        InfoText.text += "\nPosition: " + Player.transform.position.ToString();
        InfoText.text += "\nCurrent FPS: " + CurrentFPS.ToString();
        InfoText.text += "\nAverage FPS: " + AverageFPS.ToString();
        InfoText.text += "\nTimeLastOnGround: " + SPlayer.TimeLastOnGround;
        InfoText.text += "\nJumping: " + SPlayer.Jumping;
        InfoText.text += "\nGrounded: " + SPlayer.Grounded;
        //InfoText.text += "\nCurrent Direction: " + Player.GetComponent<PlayerMovementScript>().AirDirection;
        InfoText.text += "\nLandedTime: " + SPlayer.LandedTime;
        InfoText.text += "\nCrouching: " + SPlayer.Crouching;
    }
}
