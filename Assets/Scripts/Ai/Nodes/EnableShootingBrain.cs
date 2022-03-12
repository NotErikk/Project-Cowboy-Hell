using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShootingBrain : Node
{
    ShootingBrain shootingBrain;

    public EnableShootingBrain(ShootingBrain shootingBrain)
    {
        this.shootingBrain = shootingBrain;
    }

    public override NodeState Evaluate()
    {
        shootingBrain.enabled = true;
        return NodeState.Success;
    }
}
