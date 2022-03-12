using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsXInVision : Node
{
    float visionRange;
    float visionAngle;
    Transform player, ai;
    float instantDetectionRadius;

    LayerMask playerLayer = 1 << 7;
    LayerMask enviroment = 1 << 8;
    LayerMask mask;


    public IsXInVision(MovementAndSensesBrain brain)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ai = brain.gameObject.GetComponent<Transform>();

        visionAngle = brain.visionAngle;
        visionRange = brain.visionRange;
        instantDetectionRadius = brain.instantDetectionRadius;
        mask = playerLayer | enviroment;

    }

    public override NodeState Evaluate()
    {
        Vector3 myForward = new Vector3(ai.right.x * ai.localScale.x, 0, 0);

        Vector2 lines = ai.position + (myForward * visionRange);


        float distance = Vector2.Distance(player.position, ai.position);

        if (distance > visionRange) return NodeState.Failure;
        if (distance < instantDetectionRadius) return NodeState.Success;

        Vector2 toTarget = player.position - ai.position;

        float angle = Vector2.Angle(myForward, toTarget);
        if (angle > visionAngle) return NodeState.Failure;


        RaycastHit2D hitData = Physics2D.Raycast(ai.position, (player.position - ai.position).normalized, visionRange, mask.value);
        Debug.DrawRay(ai.position, (player.position - ai.position).normalized * visionRange, Color.red, 0.2f);

        if (hitData.collider != null)
        {
            if (hitData.collider.CompareTag("Player"))
            {
                return NodeState.Success;
            }
        }
        return NodeState.Failure;


    }
}
