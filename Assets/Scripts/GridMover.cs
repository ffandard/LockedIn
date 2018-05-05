using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour {
    public float xSnapCoodrinate = 0.5f;
    public float ySnapCoodrinate = 0.5f;
    public float zSnapCoordinate = 0.5f;

    public Vector3 collisionTestOffset = new Vector3( 0.0f, 0.5f, 0.0f );

    public float moveSpeed = 0.15f;
    private bool shouldMove = false;

    private Vector3 targetPosition = new Vector3();

	void Start () {
        SnapToGrid();

        targetPosition = transform.position;
	}
	
	void Update () {
	    if ( IsMoving() ) {
            transform.position = Vector3.MoveTowards( transform.position, targetPosition, moveSpeed );

            if ( targetPosition == transform.position ) {
                SnapToGrid();
                shouldMove = false;
            }
        }
    }

    private void SnapToGrid() {
        transform.position = new Vector3(
            Mathf.Sign( transform.position.x ) * ( Mathf.Abs( ( int )transform.position.x ) + xSnapCoodrinate ),
            Mathf.Sign( transform.position.y ) * ( Mathf.Abs( ( int )transform.position.y ) + ySnapCoodrinate),
            Mathf.Sign( transform.position.z ) * ( Mathf.Abs( ( int )transform.position.z ) + zSnapCoordinate )
        );

        targetPosition = transform.position;
    }

    public void Move( Vector3 moveDirection ) {
        if ( !shouldMove && GetComponent<GridCollisionResolver>().CanMoveInDirection(moveDirection) ) {
            shouldMove = true;
            targetPosition = transform.position + moveDirection;
            GetComponent<GridCollisionResolver>().StartedMoveInDirection( moveDirection );
        }
    }

    public bool IsMoving() {
        return shouldMove;
    }
}
