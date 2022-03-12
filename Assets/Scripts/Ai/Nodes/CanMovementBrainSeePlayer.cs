using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMovementBrainSeePlayer : Node
{
    ShootingBrain shootingBrain;
    MovementAndSensesBrain movementBrain;

    public CanMovementBrainSeePlayer(MovementAndSensesBrain movementBrain)
    {
        this.movementBrain = movementBrain;
    }

    public override NodeState Evaluate()
    {
        if (movementBrain.canSeePlayer) return NodeState.Success;
        return NodeState.Failure;
    }
}
