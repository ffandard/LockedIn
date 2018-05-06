using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGround : MonoBehaviour {
    private Vector3 targetUp;
    private float targetYOffset = 0.0f;
    private bool interpToMatchGround = false;
    public float moveSpeed = 0.1f;
    private float currentY = 0.0f;

    void Update() {
        if ( interpToMatchGround ) {
            transform.up = Vector3.MoveTowards( transform.up, targetUp, moveSpeed );
            transform.position = Vector3.MoveTowards( transform.position, new Vector3( transform.position.x, targetYOffset, transform.position.z ), moveSpeed );
        }
    }

    public Vector3 WillMoveTo( Vector3 target ) {
        RaycastHit hit;
        if ( Physics.Raycast( target + new Vector3( 0.0f, 1.0f, 0.0f ), Vector3.down, out hit, 2.0f ) ) {
            return new Vector3( target.x, target.y + ( 1.0f - hit.distance ), target.z );
        }

        return target;
    }

    public Vector3 GetUpVector( Vector3 target ) {
        RaycastHit hit;
        if ( Physics.Raycast( target + new Vector3( 0.0f, 1.0f, 0.0f ), Vector3.down, out hit, 2.0f ) ) {
            return hit.normal;
        }

        return Vector3.up;
    }
}
