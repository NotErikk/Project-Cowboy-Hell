using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    protected List<Node> nodesInMySelector = new List<Node>();

    public Selector(List<Node> nodesInMySelector)
    {
        this.nodesInMySelector = nodesInMySelector;
    }

    public override NodeState Evaluate() //if only one of my nodes return success return my own success
    {

        foreach (var node in nodesInMySelector)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    _nodeState = NodeState.Running;
                    return _nodeState;

                case NodeState.Success:
                    _nodeState = NodeState.Success;
                    return _nodeState;

                case NodeState.Failure:
                    break;

                default:
                    break;
            }
        }

        _nodeState = NodeState.Failure;
        return _nodeState;

    }
}
