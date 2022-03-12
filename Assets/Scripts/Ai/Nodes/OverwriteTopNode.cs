using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwriteTopNode : Node
{
    MovementAndSensesBrain brain;

    public OverwriteTopNode(MovementAndSensesBrain brain)
    {
        this.brain = brain;
    }

    public override NodeState Evaluate()
    {
        brain.ConstructOffensiveBehaviourTree();
        return NodeState.Success;
    }
}
