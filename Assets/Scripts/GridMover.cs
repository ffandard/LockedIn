using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour {
    public float xSnapCoodrinate = 0.5f;
    public float ySnapCoodrinate = 0.5f;
    public float zSnapCoordinate = 0.5f;

    public float moveSpeed = 0.15f;

    private Vector3 targetPosition = new Vector3();

	void Start () {
        SnapToGrid();

        targetPosition = transform.position;
	}
	
	void Update () {
	    if ( targetPosition != transform.position ) {
            transform.position = Vector3.MoveTowards( transform.position, targetPosition, moveSpeed );

            if ( targetPosition == transform.position ) {
                SnapToGrid();
            }
        }

        if ( Input.GetKeyDown( KeyCode.I ) ) {
            Move( new Vector3( 1.0f, 0.0f, 0.0f ) );
        } else if ( Input.GetKeyDown( KeyCode.J ) ) {
            Move( new Vector3( 0.0f, 0.0f, -1.0f ) );
        } else if ( Input.GetKeyDown( KeyCode.K ) ) {
            Move( new Vector3( -1.0f, 0.0f, 0.0f ) );
        } else if ( Input.GetKeyDown( KeyCode.L ) ) {
            Move( new Vector3( 0.0f, 0.0f, 1.0f ) );
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

    public void Move(Vector3 moveDirection) {
        if ( transform.position == targetPosition && CanMoveInDirection(moveDirection) ) {
            targetPosition = targetPosition + moveDirection;
        }
    }

    private bool CanMoveInDirection(Vector3 moveDirection) {
        RaycastHit[] hits = Physics.RaycastAll( transform.position, moveDirection, 1.0f );

        for ( int i = 0; i < hits.Length; ++i ) {
            if ( hits[i].transform.gameObject != gameObject ) {
                return false;
            }
        }

        return true;
    }
}
