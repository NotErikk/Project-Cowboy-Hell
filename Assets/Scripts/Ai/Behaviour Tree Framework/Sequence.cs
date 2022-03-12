using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    protected List<Node> nodesInMySequence = new List<Node>();

    public Sequence(List<Node> nodesInMySequence)
    {
        this.nodesInMySequence = nodesInMySequence;
    }

    public override NodeState Evaluate() //all of my nodes need to return success in order for me to return success
    {
         bool isThisNodeRunning = false;
         foreach (var node in nodesInMySequence)
         {
             switch (node.Evaluate())
             {
                 case NodeState.Running:
                     isThisNodeRunning = true;
                     _nodeState = NodeState.Running;
                     return _nodeState;

                 case NodeState.Success:
                     break;

                 case NodeState.Failure:
                     _nodeState = NodeState.Failure;
                     return _nodeState;

                 default:
                     break;
             }
         }

         /*if (isThisNodeRunning)
         {
             _nodeState = NodeState.Running;
         }
         else
         {
             _nodeState = NodeState.Success;
         }
         */

         _nodeState = isThisNodeRunning ? NodeState.Running : NodeState.Success;

         return _nodeState;
        

    }
}
