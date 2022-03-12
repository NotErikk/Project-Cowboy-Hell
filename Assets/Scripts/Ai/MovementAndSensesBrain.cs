using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndSensesBrain : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Brainwaves Per second")]
    [SerializeField] float frequency;
    WaitForSeconds waitForBrainwaves;

    [Range(1, 50)]
    public float visionRange;
    
    [Tooltip("angle of vision in degrees, 30 = 30 degrees of vertical vision")]
    [Range(1,360)]
    public float visionAngle;

    [Tooltip("Range at which the player is instantly spotted if entering this range")]
    [Range(0,10)]
    public float instantDetectionRadius;

    public float wonderMovementSpeed;
    
    public float combatMovementSpeed;

    [Tooltip("How close does the ai need to be next to cover to be considered 'in cover'")]
     public float consideredInCoverRange;

    [Tooltip("what distance should I be to the player when shooting")]
    public float preferedShootingRange;

    [Tooltip("Max Distance of a generated path, 0 = infinite range")]
    public float maxPathDistance;

    [Tooltip("when searcher creates path how close to the edge of the platform/wall is the path?  5 = 5 units away from the edge")]
    public float pathSearcherPillow;

    [Tooltip("height of the searcher")]
    public float searcherHeight;

    [Tooltip("PathFinder game object")]
    public GameObject pathSearcherGO;

    [Tooltip("How much leanience is allowed when wondering?  i can be within 0.5m on my wonder target for it to count")]
    public float wonderPillow;
    public float pathRightX, pathLeftX;

    [Tooltip("Ditch my cover if the player is this distance")]
    public float coverDitchDistance;
    public Vector3 knownCoverPosition;

    GameObject pathSearcher;

    Transform player;

    ShootingBrain shootingBrain;

    Animator myAnimator;

    Rigidbody2D myRigidbody;

    Selector topNode;

    bool knowledgeOfPlayer;

    public bool hasPath;

    public bool canSeePlayer;

    public Vector3 lastKnownPlayerPosition;

    private bool tree2 = false;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        shootingBrain = GetComponent<ShootingBrain>();
        myAnimator = GetComponent<Animator>();
        waitForBrainwaves = new WaitForSeconds(frequency);
    }

   

    public void Start()
    {
        ConstructBehaviourTree();

        StartCoroutine(think());
    }

    private void ConstructBehaviourTree()
    {
        //offensive tree leafs
        IsXInVision checkIfICanSeePlayer = new IsXInVision(this);
        EnableShootingBrain turnOnShootingBrain = new EnableShootingBrain(shootingBrain);
        OverwriteTopNode enableOffensiveBrain = new OverwriteTopNode(this);

        //offensive tree composite nodes

        Sequence beginAttack = new Sequence(new List<Node> { turnOnShootingBrain, enableOffensiveBrain });
        Sequence offensiveTree = new Sequence(new List<Node> {checkIfICanSeePlayer, beginAttack });

        //wonder tree nodes
        DoIhaveAPath checkIfIHavePath = new DoIhaveAPath(this);
        GeneratePath generatePath = new GeneratePath(this, pathSearcherGO);
        WalkBackAndForth walkBackAndForth = new WalkBackAndForth(this);

        //wonder tree composite nodes
        Selector getPath = new Selector(new List<Node> { checkIfIHavePath, generatePath});
        Sequence wonderTree = new Sequence(new List<Node> { getPath, walkBackAndForth});

        topNode = new Selector(new List<Node> { offensiveTree, wonderTree });
    }

    public void ConstructOffensiveBehaviourTree()
    {
        tree2 = true;
        //leafs
        IsXInVisionSaveLocation canISeePlayerSaveLocation = new IsXInVisionSaveLocation(this);
        CheckForCover checkForCover = new CheckForCover(this);
        LookAtPlayer lookAtPlayer = new LookAtPlayer(this);
        GoToCover goToCover = new GoToCover(this);
        GoToLastKnownPosition goToPlayerPos = new GoToLastKnownPosition(this);
        GoToPreferedShootingRange gotoPreferedShootingRange = new GoToPreferedShootingRange(this);

        //composite nodes
        Sequence gotoCover = new Sequence(new List<Node> { checkForCover, goToCover });
        Selector coverOrMoveTo = new Selector(new List<Node> { gotoCover, gotoPreferedShootingRange});
        Sequence checkVisionAndMoveToPlayerOrCover = new Sequence(new List<Node> { canISeePlayerSaveLocation,lookAtPlayer ,coverOrMoveTo });

        topNode = new Selector(new List<Node> { checkVisionAndMoveToPlayerOrCover, goToPlayerPos });
    }


    IEnumerator think()
    {
        topNode.Evaluate();
        yield return waitForBrainwaves;


        StartCoroutine(think());
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawIcon(lastKnownPlayerPosition, "IconSight.png", true);
        Gizmos.DrawIcon(knownCoverPosition, "IconCover.png", true);
        if (!tree2)
        {
            Gizmos.DrawIcon(new Vector2(pathRightX, transform.position.y), "IconWonderPointRight.png", true);
            Gizmos.DrawIcon(new Vector2(pathLeftX, transform.position.y), "IconWonderPointLeft.png", true);
        }
    }

}
