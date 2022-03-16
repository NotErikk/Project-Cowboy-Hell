using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class HitboxSizeManager : MonoBehaviour
{
    
    [Tooltip("Assign appropriate 2d box collider")]
    [SerializeField] private BoxCollider2D headCollider, bodyCollider, legsCollider;
    
    [Tooltip("How far does the head+body move down when crouched?")]
    [SerializeField] private float distanceToMoveDownOnCrouch;

    [Tooltip("How far does the head+body move down when sliding?")] 
    [SerializeField] private float distanceToMoveDownOnSlide;
    
    private Vector2 defaultOffsetHead, defaultOffsetBody, defaultOffsetLegs;
    private Vector2 crouchingOffsetHead, crouchingOffsetBody;
    private Vector2 slidingOffsetHead, slidingOffsetBody;

    private void Awake()
    {
        //default
        defaultOffsetHead = headCollider.offset;
        defaultOffsetBody = bodyCollider.offset;
        defaultOffsetLegs = legsCollider.offset;
        
        //crouching
        crouchingOffsetHead = new Vector2(defaultOffsetHead.x, defaultOffsetHead.y - distanceToMoveDownOnCrouch);
        crouchingOffsetBody = new Vector2(defaultOffsetBody.x, defaultOffsetBody.y - distanceToMoveDownOnCrouch);
        
        //sliding
        slidingOffsetHead = new Vector2(defaultOffsetHead.x, defaultOffsetHead.y - distanceToMoveDownOnSlide);
        slidingOffsetBody = new Vector2(defaultOffsetBody.x, defaultOffsetBody.y - distanceToMoveDownOnSlide);
        
    }


    [ContextMenu("Crouch")]
    public void CrouchHitbox()
    {
        headCollider.offset = crouchingOffsetHead;
        bodyCollider.offset = crouchingOffsetBody;
    }

    [ContextMenu("Slide")]
    public void SlideHitbox()
    {
        headCollider.offset = slidingOffsetHead;
        bodyCollider.offset = slidingOffsetBody;
    }

    [ContextMenu("Default Stance")]
    public void DefaultHitbox()
    {
        headCollider.offset = defaultOffsetHead;
        bodyCollider.offset = defaultOffsetBody;
        legsCollider.offset = defaultOffsetLegs;
    }

    public void ToggleHitbox(bool toggle)
    {
        headCollider.enabled = toggle;
        bodyCollider.enabled = toggle;
        legsCollider.enabled = toggle;
    }
}
