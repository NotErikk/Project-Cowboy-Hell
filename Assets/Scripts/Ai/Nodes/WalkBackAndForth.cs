using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBackAndForth : Node
{
    float currentDestination;
    MovementAndSensesBrain brain;
    Rigidbody2D myrb;

    public WalkBackAndForth(MovementAndSensesBrain brain)
    {
        this.brain = brain;
        myrb = brain.GetComponent<Rigidbody2D>();
        currentDestination = brain.transform.position.x;
    }

    public override NodeState Evaluate()
    {
        if (brain.transform.position.x >= currentDestination-brain.wonderPillow && brain.transform.position.x <= currentDestination+brain.wonderPillow)
        {
            currentDestination = Random.Range(brain.pathLeftX, brain.pathRightX);
            if (currentDestination > brain.transform.position.x) brain.transform.localScale = new Vector3(1, 1, 1);
            else brain.transform.localScale = new Vector3(-1, 1, 1);
            myrb.velocity = ((new Vector2(currentDestination, 0) - new Vector2(myrb.position.x, 0)).normalized / 10) * brain.wonderMovementSpeed;
            return NodeState.Success;
        }
        myrb.velocity = ((new Vector2(currentDestination, 0) - new Vector2(myrb.position.x,0)).normalized/10) * brain.wonderMovementSpeed;

        //brain.transform.position = Vector2.MoveTowards(brain.transform.position, new Vector2(currentDestination, myrb.transform.position.y), brain.movementSpeed);
        return NodeState.Running;

        

    }
}
