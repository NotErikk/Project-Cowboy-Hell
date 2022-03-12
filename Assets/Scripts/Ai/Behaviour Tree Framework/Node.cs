using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public abstract class Node //node class
{
    public enum NodeState {Success,Failure,Running}; //node state enum
    protected NodeState _nodeState; //protected nodestate enum type

    public NodeState nodeState { get { return _nodeState; } } //public nodestate enum with set _nodestate

    public abstract NodeState Evaluate(); //abstract evaluate func
}
