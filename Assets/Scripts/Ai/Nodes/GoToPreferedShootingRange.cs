using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPreferedShootingRange : Node
{
    MovementAndSensesBrain brain;
    Rigidbody2D myrb;
    float destination;

    public GoToPreferedShootingRange(MovementAndSensesBrain brain)
    {
        this.brain = brain;
        myrb = brain.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        if (brain.lastKnownPlayerPosition.x > brain.transform.position.x) destination = brain.lastKnownPlayerPosition.x - brain.preferedShootingRange;
        else destination = brain.lastKnownPlayerPosition.x + brain.preferedShootingRange;

        if (brain.transform.position.x >= destination - brain.wonderPillow && brain.transform.position.x <= destination + brain.wonderPillow)
        {
            myrb.velocity = new Vector2(0, myrb.velocity.y);
            return NodeState.Success;
        }
        myrb.velocity = ((new Vector2(destination, myrb.velocity.y) - new Vector2(myrb.position.x, myrb.velocity.y)).normalized / 10) * brain.combatMovementSpeed;
        return NodeState.Running;
    }
}
