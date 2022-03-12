using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MovementAndSensesBrain))]
public class AiEditor : Editor
{
    public void OnSceneGUI()
    {
        MovementAndSensesBrain brain = (MovementAndSensesBrain)target;
        Handles.color = Color.white;        Handles.DrawWireArc(brain.transform.position, new Vector3(0,0,1), Vector2.right, 360, brain.visionRange);

        Handles.color = Color.red;
        Handles.DrawWireArc(brain.transform.position, new Vector3(0, 0, 1), Vector2.right, 360, brain.instantDetectionRadius);

        Handles.color = Color.blue;
        float myVisionAngle = brain.visionAngle * brain.transform.localScale.x;

        Vector3 viewAngleA = new Vector3(Mathf.Sin((-myVisionAngle + 180)/2 * Mathf.Deg2Rad), Mathf.Cos((-myVisionAngle + 180) / 2 * Mathf.Deg2Rad), 0);
        Vector3 viewAngleB = new Vector3(Mathf.Sin((myVisionAngle + 180)/2 * Mathf.Deg2Rad), Mathf.Cos((myVisionAngle + 180) / 2 * Mathf.Deg2Rad), 0);

        Handles.DrawLine(brain.transform.position, brain.transform.position + viewAngleA * myVisionAngle);
        Handles.DrawLine(brain.transform.position, brain.transform.position + viewAngleB * myVisionAngle);

        Handles.color = Color.green;
        Handles.DrawLine(brain.pathSearcherGO.transform.position, new Vector3(0, -brain.searcherHeight, 0) + brain.pathSearcherGO.transform.position);
        Handles.DrawLine(brain.pathSearcherGO.transform.position, new Vector3(brain.searcherHeight, 0, 0) + brain.pathSearcherGO.transform.position);
        Handles.DrawLine(brain.pathSearcherGO.transform.position, new Vector3(-brain.searcherHeight, 0, 0) + brain.pathSearcherGO.transform.position);   

    }
}
