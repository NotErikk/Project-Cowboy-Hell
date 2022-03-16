using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private enum PlayerStates {
        Idle,              //0
        Walking,           //1
        WalkingBackwards,  //2
        Crouching,         //3
        CrouchWalking,     //4
        CrouchWalkingBackwards, //5
        Rolling,           //6
        Sliding,           //7
        Jumping,           //8
        Falling,           //9
        InCoyoteTime,      //10
    };

    [SerializeField]
    private PlayerStates PlayerState;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Grounded Movement
    [Header("Grounded Movement")]

    [Tooltip("Maximum ground speed of player")]
    public float MovementSpeed;

    [Tooltip ("Movement Speed Buff from items")]
    [SerializeField] float movementSpeedBuff;

    [Tooltip("Gravity of player rigidbody2D while grounded")]
    [SerializeField]
    private float GravityOnGround;

    [HideInInspector]
    public bool Grounded;

    [Tooltip("Length of raycast for grounded check (coloured red in debug)")]
    [SerializeField]
    private float GroundCheckRaycastDistance;

    [Tooltip("Input Ground/floor layerMask for grounded check")]
    [SerializeField]
    private LayerMask GroundLayerMask;

    [Tooltip("Where the grounded check raycast will originate from")]
    [SerializeField]
    private GameObject GroundCheckOrigin;

    [Tooltip("Maximum movement speed of a crouching player")]
    [SerializeField]
    private float CrouchMovementSpeed;

    [HideInInspector]
    public bool Crouching;

    private Vector2 GravityMultiplier;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Ramp Movement
    [Header("Ramp Movement")]

    [Tooltip("How long the raycast is for slope checks (coloured yellow in debug)")]
    [SerializeField]
    private float SlopeGradientRayDistance;

    [Tooltip("Gravity multiplier on the player when they're stood on a ramp")]
    [SerializeField]
    private float GravityRampMultiplier;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Jumping and in air movement
    [Header("Jumping and in Air Movement")]

    [Tooltip("Jump force when the player jumps")]
    [SerializeField]
    private float JumpForce;

    [Tooltip("jump force buff from items")]
    [SerializeField]
    float jumpForceBuff;

    [HideInInspector]
    public bool Jumping;

    [Tooltip("How much controllability the player has in the air and how fast they will accelerate when holding a direction")]
    [SerializeField]
    private float InAirInertia;

    [Tooltip("Gravity of the player rigidbody2d in air")]
    [SerializeField]
    private float GravityInAir;

    [Tooltip("Amount of time the player has after walking of an edge to jump")]
    [SerializeField]
    private float CoyoteTime;

    [Tooltip("Maximum speed of the player in air")]
    [SerializeField]
    private float InAirMaxSpeed;

    [Tooltip("Jumping cooldown for player to restrict bunny hopping, measured in seconds")]
    [SerializeField]
    private float JumpCooldown;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Bridges
    [Header("Bridges")]

    [Tooltip("Player layermask to remove on platform effector to allow the player to pass through the platform")]
    [SerializeField]
    private LayerMask PlayerLayer;

    [Tooltip("After the player drops through a platform this is the cooldown untill they can step on another platform (this stops the player getting stuck while dropping through a platform)")]
    [SerializeField]
    private float BridgeDropCooldown;

    private List<PlatformEffector2D> AllBridges;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Rolling and Dodging
    [Header("Roll/Dodge")]

    [Tooltip("How long a roll will last")]
    [SerializeField]
    private float RollTime;

    [Tooltip("How fast the player is moving during a roll")]
    [SerializeField]
    private float RollSpeed;

    [Tooltip("How long the player needs to wait inbetween rolls")]
    [SerializeField]
    private float RollCooldown;


    [Tooltip("buff given from items")]
    [SerializeField]
    float rollCooldownBuff;

    private bool Rolling;

    private float RollingDirection;

    private float LastRollTime;

    private bool Rolled;

    [HideInInspector] public float idleRollDirection;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Slide Settings
    [Header("Slide")]

    [SerializeField]
    [Tooltip("Decelamount")]
    private float SlideDecelleration;   
    
    private int SlideDirection;

    private bool Sliding;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////MISC
    [Header("Misc")]

    [Tooltip("Gameobject of the cursor to ensure the player is facing the correct direction")]
    [SerializeField]
    private GameObject Cursor;

    [Tooltip("Animator component of the player sprite so animations can be changed accoringly")]
    [SerializeField]
    private Animator PlayerSpriteAnimator;

    [HideInInspector]
    public float TimeLastOnGround;
  
    [HideInInspector]
    public float AirDirection;

    private Rigidbody2D PlayerRigidbody2D;
    private float Horizontal;
    private float xMovement;

    [HideInInspector]
    public float LandedTime;
   
    private bool CanControlPlayer;

    [SerializeField]
    private GameObject WeaponGO;
    SpriteRenderer weaponSpriteRenderer;
    SpriteRenderer armFrontSpriteRenderer;
    SpriteRenderer armbackSpriteRenderer;

    [SerializeField]
    private GameObject ArmFront;

    [SerializeField]
    private GameObject ArmBack;

    private Transform WeapoTransform;

    [SerializeField]
    private WeaponController wepcontroller;

    bool CoyoteTimeStarted;
    float CoyoteTimeEndTime;

    bool IsCrouching;
    bool IsJumping;
    bool IsDodge;

    ItemManager itemManager;

    private HitboxSizeManager hitboxManager;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    public void Awake()
    {
        weaponSpriteRenderer = WeaponGO.GetComponent<SpriteRenderer>();
        armFrontSpriteRenderer = ArmFront.GetComponent<SpriteRenderer>();
        armbackSpriteRenderer = ArmBack.GetComponent<SpriteRenderer>();
        itemManager = GetComponent<ItemManager>();

        WeapoTransform = WeaponGO.transform;
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        idleRollDirection = PlayerPrefs.GetInt(PlayerPrefsNames.idleRollDirection);
        if (idleRollDirection == 0) idleRollDirection = 1;

        hitboxManager = GetComponentInChildren<HitboxSizeManager>();
    }

    private void Start()
    {
        SetGravityAir();
        //GetBridgeList();

        UpdateMovementSpeedBuff();
        UpdateJumpBuff();
        UpdateRollCooldownBuff();

        CanControlPlayer = true;
        PlayerState = PlayerStates.Idle;
    }

    
    private void FixedUpdate()
    {     
       UpdatedMovementSystem();       
    }

    void UpdatedMovementSystem()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        switch (PlayerState)
        {
            case PlayerStates.Idle:
                IdleFixed();
                break;
            case PlayerStates.Walking:
                WalkingFixed();
                break;
            case PlayerStates.WalkingBackwards:
                WalkingBackwards();
                break;
            case PlayerStates.Crouching:
                CrouchingFixed();
                break;
            case PlayerStates.CrouchWalking:
                CrouchWalking();
                break;
            case PlayerStates.CrouchWalkingBackwards:
                CrouchWalkingBackwards();
                break;
            case PlayerStates.Rolling:
                DoARollFIXED();
                break;
            case PlayerStates.Sliding:
                Slide();
                break;
            case PlayerStates.Jumping:
                JumpFixed();
                break;
            case PlayerStates.Falling:
                Falling();
                break;
            case PlayerStates.InCoyoteTime:
                CoyoteTimeFIXED();
                break;
        }
    }

    private void Update()
    {
        IsCrouching = Input.GetButton("Crouch");
        IsJumping = Input.GetButton("Jump");
        IsDodge = Input.GetButton("Dodge");

        PlayerSpriteAnimator.SetInteger("State", (int)PlayerState);


        PlatformCheck();
        if (Rolling == false && Sliding == false)
        {
            PlayerLookingDirection();
        }
        if (Horizontal != 0)
        {
            PlayerSpriteAnimator.SetBool("Running", true);
        }
        else
        {
            PlayerSpriteAnimator.SetBool("Running", false);
        }
    }

    void Slide() //legal tranitions: jump, crouch. crouchwalking. crouchwalkingbackwards, idle, walking, walkingbackwards
    {
        if (!Sliding) //get sliding direction
        {
            Sliding = true;
            SlideDirection = ((int)Horizontal);
            transform.localScale = new Vector3(GetPlayerDirection(), 1, 1);
        }      

        if (IsGrounded()) //if still grounded decellerate and give option to jump
        {
            var velocity = PlayerRigidbody2D.velocity;
            velocity = new Vector2(velocity.x - SlideDecelleration * SlideDirection, velocity.y);

            PlayerRigidbody2D.velocity = velocity;

            if (IsJumping) //check jump
            {
                Sliding = false;
                hitboxManager.DefaultHitbox();
                PlayerState = PlayerStates.Jumping;
                return;
            }
        }
      

        if (!IsCrouching) //if player releases crouch, stand
        {
            Sliding = false;
            if (Horizontal == 0)
            {
                hitboxManager.DefaultHitbox();
                PlayerState = PlayerStates.Idle; //check idle
                return;
            }
            else if (Horizontal == transform.localScale.x)
            {
                hitboxManager.DefaultHitbox();
                PlayerState = PlayerStates.Walking; //check walking
                return;
            }
            else if (Horizontal != transform.localScale.x)
            {
                hitboxManager.DefaultHitbox();
                PlayerState = PlayerStates.WalkingBackwards;  //check walkingbackwards
                return;
            }
        }


        
        if (PlayerRigidbody2D.velocity.x < 1 && PlayerRigidbody2D.velocity.x > -1) //if velocity between -1 : 1 then stop sliding
        {
            Sliding = false;
            if (IsCrouching)
            {
                hitboxManager.CrouchHitbox();
                
                if (Horizontal == 0)
                {
                    PlayerState = PlayerStates.Crouching; //check crouching
                    return;
                }
                else if (Horizontal == transform.localScale.x)
                {
                    PlayerState = PlayerStates.CrouchWalking; //check crouchwalking
                    return;
                }
                PlayerState = PlayerStates.CrouchWalkingBackwards;  //check crouchwalkingbackwards   
                return;
            }



            

        }
    }


    void CoyoteTimeFIXED() //legal transitions: falling, jumping, idle, walking, walkingbackwards, roll
    {
        InAirMovement();

        if (!CoyoteTimeStarted)
        {
            CoyoteTimeStarted = true;
            CoyoteTimeEndTime = Time.time + CoyoteTime;
        }
        else if (Time.time >= CoyoteTimeEndTime)
        {
            CoyoteTimeStarted = false;
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Falling;
        }
        else if (IsJumping) //check jumping
        {
            CoyoteTimeStarted = false;
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Jumping;
        }
        else if (IsDodge) //check dodge
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
            {
                PlayerState = PlayerStates.Rolling;
            }
        }


        else if (IsGrounded()) //IF the players hits the floor
        {
            hitboxManager.DefaultHitbox();
            CoyoteTimeStarted = false;
            if (Horizontal != 0) //is the player trying to walk?
            {
                if (Horizontal == transform.localScale.x)
                {
                    PlayerState = PlayerStates.Walking; //check walking
                }
                PlayerState = PlayerStates.WalkingBackwards;  //check walkingbackwards    
            }
            PlayerState = PlayerStates.Idle;
        }
        
    }


    void Falling() //legal transitions: idle, walking, walkingbackwards, roll
    {
        InAirMovement();

       if (IsDodge) //check dodge
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
                PlayerState = PlayerStates.Rolling;
        }
        else if (IsGrounded())
        {
            hitboxManager.DefaultHitbox();
            if (Horizontal != 0) //is the player trying to walk?
            {
                if (Horizontal == transform.localScale.x)
                {
                    PlayerState = PlayerStates.Walking; //check walking
                }
                PlayerState = PlayerStates.WalkingBackwards;  //check walkingbackwards    
            }
            PlayerState = PlayerStates.Idle;
        }

    }

    void DoARollFIXED() //legal transitions: falling, idle, walking, walkingbackwards, crouching, crouchingwalking, crouchingwalkingbackwards
    {
        if (!Rolling && !Rolled) //do a roll
        {            
            Rolling = true;
            StartCoroutine(DoARollUPDATED());
        }
        else if (Rolling)
        {
            PlayerRigidbody2D.velocity = new Vector2(RollSpeed * RollingDirection, PlayerRigidbody2D.velocity.y);
        }
        else if (Rolled) //now that the player has rolled find out what state to move to
        {
            Rolled = false;

            if (Mathf.Sign(PlayerRigidbody2D.velocity.y) == -1 && !IsGrounded()) //check falling
            {
                hitboxManager.DefaultHitbox();
                PlayerState = PlayerStates.Falling;
            }


            else if (Horizontal != 0) //if the player wants to move  then \/
            {
                
                if (!IsCrouching)
                {
                    if (Horizontal == transform.localScale.x) //check walking
                    {
                        hitboxManager.DefaultHitbox();
                        PlayerState = PlayerStates.Walking;
                    }               
                    hitboxManager.DefaultHitbox();
                    PlayerState = PlayerStates.WalkingBackwards; //check walking backwards
                    
                }
                else if (IsCrouching)
                {
                    if (Horizontal == transform.localScale.x) //check crouchwalking
                    {
                        hitboxManager.CrouchHitbox();
                        PlayerState = PlayerStates.CrouchWalking;
                    }
                    hitboxManager.CrouchHitbox();
                    PlayerState = PlayerStates.CrouchWalkingBackwards; //check crouch walking back
                }
            }


            else if (Horizontal == 0)
            {
                if (Input.GetButton("Crouch")) //check crouch idle
                {
                    hitboxManager.CrouchHitbox();
                    PlayerState = PlayerStates.Crouching;
                }
                hitboxManager.DefaultHitbox();
                PlayerState = PlayerStates.Idle; //check idle

            }
        }
    }

    private IEnumerator DoARollUPDATED() //initiates a roll.  is called when a roll starts but code for physically moving the player is in fixed update.
    {
        hitboxManager.ToggleHitbox(false);
        ToggleHideArmsAndWeapon(false); //hide weps and arms
        RollingDirection = GetPlayerDirection();
        transform.localScale = new Vector3(RollingDirection, 1, 1);
        LastRollTime = Time.time + RollTime;
        yield return new WaitForSeconds(RollTime);
        ToggleHideArmsAndWeapon(true);
        Rolling = false;
        Rolled = true;
        hitboxManager.ToggleHitbox(true);
    }

    void CrouchWalkingBackwards() //legal transitions: crouching, crouchwalking, jumping, dodge, coyotetime, walking
    {
        SlopeCheck();
        xMovement = Horizontal * CrouchMovementSpeed * movementSpeedBuff;
        PlayerRigidbody2D.velocity = new Vector2(xMovement, PlayerRigidbody2D.velocity.y);

        if (Horizontal == 0)
        {
            hitboxManager.CrouchHitbox();
            PlayerState = PlayerStates.Crouching; //check crouching
        }
        else if (Horizontal == transform.localScale.x) //go to crouching or crouchwalking backwards
        {   
            hitboxManager.CrouchHitbox();
            PlayerState = PlayerStates.CrouchWalking; //check crouchingwalkingbackwards
        }
        else if (IsJumping) //check jumping
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Jumping;
        }

        else if (IsDodge) //check dodge
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
            PlayerState = PlayerStates.Rolling;
        }

        else if (!IsCrouching) //check no longer holding crouch
        {
            hitboxManager.DefaultHitbox();
            if (Horizontal == 0)                //check Idle
            {
                PlayerState = PlayerStates.Idle;
            }
            PlayerState = PlayerStates.WalkingBackwards; //check walking and stood up
        }
        else if (Mathf.Sign(PlayerRigidbody2D.velocity.y) == -1 && !IsGrounded()) //check falling and activate coyote
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.InCoyoteTime;
        }
    }


    void CrouchWalking() //legal transitions: crouching, crouchwalkingbackwards, jumping, dodge, coyotetime, walking
    {
        SlopeCheck();
        xMovement = Horizontal * CrouchMovementSpeed * movementSpeedBuff;
        PlayerRigidbody2D.velocity = new Vector2(xMovement, PlayerRigidbody2D.velocity.y);


        if (Horizontal != transform.localScale.x) //go to crouching or crouchwalking backwards
        {
            if (Horizontal == 0)
            {
                hitboxManager.CrouchHitbox();
                PlayerState = PlayerStates.Crouching; //check crouching
            }
            hitboxManager.CrouchHitbox();
            PlayerState = PlayerStates.CrouchWalkingBackwards; //check crouchingwalkingbackwards
        }

        else if (IsJumping) //check jumping
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Jumping;
        }

        else if (!IsCrouching) //check no longer holding crouch
        {
            hitboxManager.DefaultHitbox();
            if (Horizontal == 0)                //check Idle
            {
                PlayerState = PlayerStates.Idle;
            }
            PlayerState = PlayerStates.Walking; //check walking and stood up
        }

        else if (IsDodge) //check dodge
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
                PlayerState = PlayerStates.Rolling;
        }

        else if (Mathf.Sign(PlayerRigidbody2D.velocity.y) == -1 && !IsGrounded()) //check falling and activate coyote
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.InCoyoteTime;
        }
    }


    void CrouchingFixed() //legal transitions: crouchwalking, crouchwalkingbackwards, idle, jumping, rolling, incoyotetime
    {
        SlopeCheck();
        PlayerRigidbody2D.velocity = new Vector2(0, PlayerRigidbody2D.velocity.y);
        if (!IsCrouching) //check for idle
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Idle;
        }

        else if (Horizontal != 0) //is the player trying to walk?
        {
            hitboxManager.CrouchHitbox();
            if (Horizontal == transform.localScale.x) //check crouch walking
            {
                PlayerState = PlayerStates.CrouchWalking;
            }
            PlayerState = PlayerStates.CrouchWalkingBackwards; //check crouch walking backwards
            
        }

        else if (IsJumping) //check jumping
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Jumping;
        }

        else if (IsDodge) //check rolling
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
                PlayerState = PlayerStates.Rolling;
        }

        else if (Mathf.Sign(PlayerRigidbody2D.velocity.y) == -1 && !IsGrounded()) //check fallking activated coyotoe time
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.InCoyoteTime;
        }
    }


    void IdleFixed() //legal transitions: Walking, WalkingBackwards, Crouching, Rolling, Jumping, incoyotetime
    {
        SlopeCheck();
        PlayerRigidbody2D.velocity = new Vector2(0, PlayerRigidbody2D.velocity.y);
        if (Horizontal != 0) //is the player trying to walk?
        {
            hitboxManager.DefaultHitbox();
            if (Horizontal == transform.localScale.x)
            {
                
                PlayerState = PlayerStates.Walking; //check walking
            }                      
            PlayerState = PlayerStates.WalkingBackwards;       //check walkingbackwards    
        }

        else if(IsCrouching) //is the player crouching?
        {
            hitboxManager.CrouchHitbox();
            PlayerState = PlayerStates.Crouching;
        }

        else if (IsJumping) //is the player jumping
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Jumping;
        }

        else if (IsDodge) //is player dodging
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
            {
                PlayerState = PlayerStates.Rolling;
            }
        }

        else if (Mathf.Sign(PlayerRigidbody2D.velocity.y) == -1 && !IsGrounded()) //is player falling activate coyote
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.InCoyoteTime;
        }

    }

    void WalkingBackwards()//legal transitions: walking, jumping, incoyotetime, sliding, roll, idle
    {
        SlopeCheck();
        xMovement = (Horizontal * MovementSpeed) * movementSpeedBuff;
        PlayerRigidbody2D.velocity = new Vector2(xMovement, PlayerRigidbody2D.velocity.y);

        if (Horizontal == 0) //check idle
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Idle;
        }
        else if (Horizontal == transform.localScale.x) //go to idle or walking backwards
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Walking;
        }

        else if (IsJumping) //check jump
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Jumping;
        }

        else if (IsDodge) //check dodge
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
                PlayerState = PlayerStates.Rolling;
        }

        else if (IsCrouching) //check Sliding
        {
            hitboxManager.SlideHitbox();
            PlayerState = PlayerStates.Sliding;
        }

        else if (Mathf.Sign(PlayerRigidbody2D.velocity.y) == -1 && !IsGrounded()) //check if falling and activate coyote
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.InCoyoteTime;
        }
    }



    void WalkingFixed() //legal transitions: walking backwards, jumping, incoyotetime, sliding, roll, idle
    {
        SlopeCheck();
        xMovement = Horizontal * MovementSpeed * movementSpeedBuff;
        PlayerRigidbody2D.velocity = new Vector2(xMovement, PlayerRigidbody2D.velocity.y);


        if (Horizontal != transform.localScale.x) //go to idle or walking backwards
        {
            hitboxManager.DefaultHitbox();
            if (Horizontal == 0)
            {
                PlayerState = PlayerStates.Idle;
            }
            PlayerState = PlayerStates.WalkingBackwards;
        }

        else if (IsJumping) //check jumping
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.Jumping;
        }

        else if (IsDodge) //check dodge
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
                PlayerState = PlayerStates.Rolling;
        }

        else if (IsCrouching) //check Sliding
        {
            hitboxManager.SlideHitbox();
            PlayerState = PlayerStates.Sliding;
        }

        else if (Mathf.Sign(PlayerRigidbody2D.velocity.y) == -1 && !IsGrounded()) //check falling and activate coyote
        {
            hitboxManager.DefaultHitbox();
            PlayerState = PlayerStates.InCoyoteTime;
        }

    }

    void JumpFixed() //legal transitions:falling, roll, idle
    {
        InAirMovement();


        if (!Jumping) //Jump force
        {
            hitboxManager.DefaultHitbox();
            Jumping = true;
            SetGravityAir();
            PlayerRigidbody2D.velocity = new Vector2(PlayerRigidbody2D.velocity.x, JumpForce * jumpForceBuff);
        }

        //Check to Leave State
        else if (IsDodge)
        {
            if (LastRollTime <= Time.time - RollCooldown - rollCooldownBuff)
            {
                Jumping = false;
                PlayerState = PlayerStates.Rolling;
            }
        }
        else if (Mathf.Sign(PlayerRigidbody2D.velocity.y) == -1)
        {
            hitboxManager.DefaultHitbox();
            Jumping = false;
            PlayerState = PlayerStates.Falling;
        }
        else if (IsGrounded())
        {
            hitboxManager.DefaultHitbox();
            Jumping = false;
            PlayerState = PlayerStates.Idle;
        }
    }


    void InAirMovement()
    {;
        xMovement = PlayerRigidbody2D.velocity.x + Horizontal * InAirInertia * movementSpeedBuff; //in air movement
        xMovement = Mathf.Clamp(xMovement, -InAirMaxSpeed * movementSpeedBuff, InAirMaxSpeed * movementSpeedBuff);

        PlayerRigidbody2D.velocity = new Vector2(xMovement, PlayerRigidbody2D.velocity.y);
    }

    private bool IsGrounded() //grounded check returns a bool of true or false if the player is grounded or not using a raycast
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckOrigin.transform.position, Vector2.down, GroundCheckRaycastDistance, GroundLayerMask);

        Debug.DrawRay(GroundCheckOrigin.transform.position, Vector2.down * GroundCheckRaycastDistance, Color.red);

        if (hit.collider != null)
        {
            Jumping = false;
            TimeLastOnGround = Time.time;
            PlayerRigidbody2D.gravityScale = GravityOnGround;
            return true;
        }
        else
        {
            LandedTime = Time.time;
            SetGravityAir();
            return false;
        }
    }

    private void ToggleHideArmsAndWeapon(bool toggle)
    {
        weaponSpriteRenderer.enabled = toggle;
        armFrontSpriteRenderer.enabled = toggle;
        armbackSpriteRenderer.enabled = toggle;
        wepcontroller.canShoot = toggle;

        if (toggle)
            if (!wepcontroller.reloading)
            {
                wepcontroller.ResetHandPositionsForNewFirearm();
            }
    }

    private void SlopeCheck() //checks the angle of the ground the player is stood and and adjusts gravity direction accordingly, this makes going up and down hills much smoother and  the player will stick to the slopes
    {
        RaycastHit2D GradientRay = Physics2D.Raycast(transform.position, Vector2.down, SlopeGradientRayDistance, GroundLayerMask);

        Debug.DrawRay(transform.position, Vector2.down * SlopeGradientRayDistance, Color.yellow);
        Debug.DrawRay(transform.position, -GradientRay.normal * SlopeGradientRayDistance, Color.blue);


        GravityMultiplier = new Vector2(-GradientRay.normal.x * GravityRampMultiplier * 9.8f, -9.8f);
        PlayerRigidbody2D.AddForce(GravityMultiplier);
    }
    
    [ContextMenu("Update bridge list")]
    public void GetBridgeList() //fills a list of all bridges in the scene
    {
        AllBridges = new List<PlatformEffector2D>();

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Bridge"))
        {
            AllBridges.Add(x.GetComponent<PlatformEffector2D>());
        }
    }

    private float GetPlayerDirection() //gets the player direction of movement but if the player is idle get the direction they are currently facing, returns 1 or -1
    {
        if (Horizontal == 0)
        {
            return transform.localScale.x * idleRollDirection;
        }
        return Mathf.Sign(Horizontal);

    }

    private void SetGravityAir() //Sets gravity to the player's in air gravity scale and resets gravity direction incase the player is jumping off a ramp
    {
        PlayerRigidbody2D.gravityScale = GravityInAir;
    }

    private void PlatformCheck() //checks to see if the player wants to drop through a platform
    {
        if (Input.GetButtonDown("BridgeDrop"))
        {
            DropPlayerThroughPlatform();
        }
        else if (Input.GetButtonUp("BridgeDrop"))
        {
            StartCoroutine(WaitForBridgeDrop());
        }
    }
    private void DropPlayerThroughPlatform() //removes player from platformEffectors layer mask.  bascially makes player able to drop through platforms
    {
        foreach (PlatformEffector2D x in AllBridges)
        {
            x.colliderMask -= PlayerLayer;
        }
    }

    private IEnumerator WaitForBridgeDrop() //after the player releases the bridge drop button this will make them able to walk on them again after the cooldown has finished
    {
        yield return new WaitForSeconds(BridgeDropCooldown);

        foreach (PlatformEffector2D x in AllBridges)
        {
            x.colliderMask += PlayerLayer;
        }

    }

    private void PlayerLookingDirection() //Makes sure the player is always looking towards the player's mouse, this is done by changing scale to 1, -1 which flips the sprite
    {
        if (Cursor.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1, 1);
            WeapoTransform.localScale = new Vector3(1f, 1, 1);

        }
        else
        {
            transform.localScale = new Vector3(-1f, 1, 1);
            if (wepcontroller.reloading == false)
            {
                WeapoTransform.localScale = new Vector3(-1f, -1, 1);
            }
        }
    }
    
    public void UpdateMovementSpeedBuff()
    {
        movementSpeedBuff = 1 + (float)itemManager.movementSpeedBuff;
    }

    public void UpdateJumpBuff()
    {
        jumpForceBuff = 1 + (float)itemManager.jumpForceBuff;
    }
    
    public void UpdateRollCooldownBuff()
    {
        rollCooldownBuff = RollCooldown - RollCooldown * (1 + itemManager.rollCooldownBuff);
    }
}




