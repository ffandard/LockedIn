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

    public void WillMoveTo( Vector3 target ) {
        RaycastHit hit;
        if ( Physics.Raycast( target + new Vector3( 0.0f, 1.0f, 0.0f ), Vector3.down, out hit, 2.0f ) ) {
            targetUp = hit.normal;
            targetYOffset = transform.position.y + ( 1.0f - hit.distance );
            interpToMatchGround = true;
        }
    }
}
