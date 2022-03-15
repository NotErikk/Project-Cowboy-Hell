using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBrain : MonoBehaviour
{
    Selector topNode;
    [SerializeField] FirearmSO myWeapon;
    [SerializeField] FirearmSO[] allAvailableWeps;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject gun;
    GameObject player;
    MovementAndSensesBrain movementBrain;
    [HideInInspector] public float firerate;
    [HideInInspector] public float timeSinceLastShot;
    public Transform firePoint;
    

    public void Awake()
    {
        if (myWeapon == null)
        {
            int randomWepId = Random.Range(0, allAvailableWeps.Length);
            myWeapon = allAvailableWeps[randomWepId];
        }
        firePoint.localPosition = myWeapon.FirePointCords;
        
        sprite.sprite = myWeapon.GunSprite;

        movementBrain = GetComponent<MovementAndSensesBrain>();
        player = GameObject.FindGameObjectWithTag("Player");

        firerate = myWeapon.Firerate;
        timeSinceLastShot = Time.time;
    }

    public void Start()
    {
        constructBehaviourTree();
    }

    private void constructBehaviourTree()
    {
        //leafs
        CanMovementBrainSeePlayer canISeePlayer = new CanMovementBrainSeePlayer(movementBrain);
        AimAtPlayer aimAtPlayer = new AimAtPlayer(player, gun);
        FirerateCheck firerateCheck = new FirerateCheck(this);
        ShootAtPlayer shootAtPlayer = new ShootAtPlayer(this, myWeapon);

        //composite nodes
        Sequence engagePlayer = new Sequence(new List<Node> { canISeePlayer, aimAtPlayer, firerateCheck, shootAtPlayer });


        topNode = new Selector(new List<Node>{ engagePlayer });
    }
    
    void Update()
    {
        topNode.Evaluate();
    }
}
