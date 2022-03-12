using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForCover : Node
{
    MovementAndSensesBrain brain;
    float checkDirection = 1;
    LayerMask coverLayer = 1 << 13;
    LayerMask playerLayer = 1 << 7;
    LayerMask maskLayer;

    public CheckForCover(MovementAndSensesBrain brain)
    {
        this.brain = brain;
        maskLayer = coverLayer | playerLayer;
    }

    public override NodeState Evaluate()
    {
        if (Vector2.Distance(brain.transform.position, brain.lastKnownPlayerPosition) > brain.coverDitchDistance) return NodeState.Failure;

        if (brain.lastKnownPlayerPosition.x > brain.transform.position.x) //right
        {
            checkDirection = 1;
        }
        else //left
        {
            checkDirection = -1;
        }

        Debug.DrawRay(brain.transform.position, new Vector2(checkDirection, 0) * 5, Color.magenta, 3f) ;
        RaycastHit2D hitData = Physics2D.Raycast(brain.transform.position, new Vector2(checkDirection, 0), 20, maskLayer.value);

        if (hitData.collider != null)
        {
            if (hitData.collider.CompareTag("Cover"))
            {
                brain.knownCoverPosition = new Vector2(hitData.transform.position.x - (checkDirection * brain.consideredInCoverRange), brain.transform.position.y);
                Debug.DrawLine(brain.transform.position, brain.knownCoverPosition, Color.cyan, 2f);
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }
}
