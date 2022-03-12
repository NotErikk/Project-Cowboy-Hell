using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastKnownPosition : Node
{
    MovementAndSensesBrain brain;
    Rigidbody2D myrb;

    public GoToLastKnownPosition(MovementAndSensesBrain brain)
    {
        this.brain = brain;
        myrb = brain.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        if (brain.transform.position.x >= brain.lastKnownPlayerPosition.x - brain.wonderPillow && brain.transform.position.x <= brain.lastKnownPlayerPosition.x + brain.wonderPillow)
        {
            myrb.velocity = new Vector2(0, myrb.velocity.y);
            return NodeState.Success;
        }
        myrb.velocity = ((new Vector2(brain.lastKnownPlayerPosition.x, myrb.velocity.y) - new Vector2(myrb.position.x, myrb.velocity.y)).normalized / 10) * brain.combatMovementSpeed;
        return NodeState.Running;
    }
}
