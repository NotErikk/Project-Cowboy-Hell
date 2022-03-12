using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : Node
{
    ShootingBrain brain;

    public ShootAtPlayer(ShootingBrain brain)
    {
        this.brain = brain;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("bang" + brain.timeSinceLastShot);
        brain.timeSinceLastShot = Time.time;
        return NodeState.Success;
    }
}
