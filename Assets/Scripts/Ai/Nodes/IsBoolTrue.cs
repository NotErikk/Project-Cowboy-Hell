using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBoolTrue : Node
{
    bool value;
    public IsBoolTrue(ref bool value)
    {
        this.value = value;
    }

    public override NodeState Evaluate()
    {
        if (value) return NodeState.Success;

        return NodeState.Failure;
    }

}
