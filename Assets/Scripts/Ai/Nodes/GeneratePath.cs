using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePath : Node
{
    GameObject pathFinder;
    MovementAndSensesBrain brain;
    LayerMask enviroment = 1 << 8;
    Vector2 currentSearchDirection = Vector2.right;
    int currentMoveDirection = 1;
    int currentDistance = 0;

    public GeneratePath(MovementAndSensesBrain brain, GameObject pathFinder)
    {
        this.pathFinder = pathFinder;
        this.brain = brain;
        pathFinder.transform.position = brain.transform.position;
    }


    public override NodeState Evaluate()
    {
        if (Mathf.Abs(currentDistance) >= brain.maxPathDistance && brain.maxPathDistance != 0)
        {
            if (currentSearchDirection == Vector2.left)
            {
                brain.pathLeftX = pathFinder.transform.position.x + brain.pathSearcherPillow;
                brain.hasPath = true;
                pathFinder.transform.position = brain.transform.position;
                return NodeState.Success;
            }


            currentDistance = 0;
            brain.pathRightX = pathFinder.transform.position.x - brain.pathSearcherPillow;
            currentSearchDirection = Vector2.left;
            currentMoveDirection = -1;
            pathFinder.transform.position = brain.transform.position;
            return NodeState.Running;
        }


       
        //shoot a ray down
        RaycastHit2D hitData = Physics2D.Raycast(pathFinder.transform.position, Vector2.down, brain.searcherHeight, enviroment.value);


        if (hitData.collider != null)
        {
            hitData = Physics2D.Raycast(pathFinder.transform.position, currentSearchDirection, brain.searcherHeight, enviroment.value);
            if (hitData.collider == null)
            {
                pathFinder.transform.position += new Vector3(currentMoveDirection, 0, 0);
                currentDistance += currentMoveDirection;
                return NodeState.Running;
            }
            
        }

        //apply pillow, set left/right

        if (currentSearchDirection == Vector2.right)
        {
            currentDistance = 0;
            brain.pathRightX = pathFinder.transform.position.x - brain.pathSearcherPillow;
            currentSearchDirection = Vector2.left;
            currentMoveDirection = -1;
            pathFinder.transform.position = brain.transform.position;
            return NodeState.Running;
        }

        brain.pathLeftX = pathFinder.transform.position.x + brain.pathSearcherPillow;
        brain.hasPath = true;
        pathFinder.transform.position = brain.transform.position;
        return NodeState.Success;
    }

    private void complete()
    {

    }




}


