using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : Node
{
    MovementAndSensesBrain brain;

    public LookAtPlayer(MovementAndSensesBrain brain)
    {
        this.brain = brain;
    }

    public override NodeState Evaluate()
    {
        if (brain.lastKnownPlayerPosition.x > brain.transform.position.x) brain.transform.localScale = new Vector3(1, 1, 1);
        else brain.transform.localScale = new Vector3(-1, 1, 1);
        return NodeState.Success;
    }
}
