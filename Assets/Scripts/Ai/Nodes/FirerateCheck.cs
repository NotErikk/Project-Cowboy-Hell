using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirerateCheck : Node
{
    ShootingBrain brain;

    public FirerateCheck(ShootingBrain brain)
    {
        this.brain = brain;
    }

    public override NodeState Evaluate()
    {
        if (Time.time >= brain.timeSinceLastShot + 2) return NodeState.Success;
        
        return NodeState.Failure;
    }
}
