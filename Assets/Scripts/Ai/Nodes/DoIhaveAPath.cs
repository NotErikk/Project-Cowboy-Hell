using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoIhaveAPath : Node
{
    MovementAndSensesBrain brain;

    public DoIhaveAPath(MovementAndSensesBrain brain)
    {
        this.brain = brain;
    }

    public override NodeState Evaluate()
    {
        if (brain.hasPath) return NodeState.Success;
        return NodeState.Failure;
    }
}
