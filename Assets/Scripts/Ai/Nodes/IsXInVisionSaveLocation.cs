using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsXInVisionSaveLocation :Node
{
    float visionRange;
    float visionAngle;
    Transform player, ai;
    float instantDetectionRadius;
    MovementAndSensesBrain brain;

    LayerMask playerLayer = 1 << 7;
    LayerMask enviroment = 1 << 8;
    LayerMask mask;


    public IsXInVisionSaveLocation(MovementAndSensesBrain brain)
    {
        this.brain = brain;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ai = brain.gameObject.GetComponent<Transform>();

        visionAngle = 360;
        visionRange = 100;
        instantDetectionRadius = brain.instantDetectionRadius;
        mask = playerLayer | enviroment;

    }


    public override NodeState Evaluate()
    {
        Vector3 myForward = new Vector3(ai.right.x * ai.localScale.x, 0, 0);

        Vector2 lines = ai.position + (myForward * visionRange);


        float distance = Vector2.Distance(player.position, ai.position);

        if (distance < instantDetectionRadius)
        {
            brain.lastKnownPlayerPosition = player.transform.position;
            brain.canSeePlayer = true;
            return NodeState.Success;
        }

        Vector2 toTarget = player.position - ai.position;

        float angle = Vector2.Angle(myForward, toTarget);
        if (angle > visionAngle)
        {
            brain.canSeePlayer = false;
            return NodeState.Failure;
        }


        RaycastHit2D hitData = Physics2D.Raycast(ai.position, (player.position - ai.position).normalized, visionRange, mask.value);
        Debug.DrawRay(ai.position, (player.position - ai.position).normalized * visionRange, Color.red, 0.2f);

        if (hitData.collider != null)
        {
            if (hitData.collider.CompareTag("Player"))
            {
                brain.lastKnownPlayerPosition = player.transform.position;
                brain.canSeePlayer = true;
                return NodeState.Success;
            }
        }
        brain.canSeePlayer = false;
        return NodeState.Failure;
    }
}
