using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour {
    public float xSnapCoodrinate = 0.5f;
    public float ySnapCoodrinate = 0.5f;
    public float zSnapCoordinate = 0.5f;

    public bool ignoreY = false;

    public float moveSpeed = 0.15f;
    private bool shouldMove = false;

    private Vector3 targetPosition = new Vector3();
    private Vector3 targetUp = new Vector3();

    void Start () {
        SnapToGrid();

        targetPosition = transform.position;
	}
	
	void Update () {
	    if ( shouldMove ) {
            transform.position = Vector3.MoveTowards( transform.position, targetPosition, moveSpeed );
            transform.up = Vector3.MoveTowards( transform.up, targetUp, moveSpeed );

            if ( targetPosition == transform.position ) {
                SnapToGrid();
                shouldMove = false;
            }
        }
    }

    private void SnapToGrid() {
        transform.position = new Vector3(
            Mathf.Sign( transform.position.x ) * ( Mathf.Abs( ( int )transform.position.x ) + xSnapCoodrinate ),
            ignoreY ? transform.position.y : Mathf.Sign( transform.position.y ) * ( Mathf.Abs( ( int )transform.position.y ) + ySnapCoodrinate),
            Mathf.Sign( transform.position.z ) * ( Mathf.Abs( ( int )transform.position.z ) + zSnapCoordinate )
        );

        targetPosition = transform.position;
    }

    public void Move( Vector3 moveDirection, bool pushAdjecent ) {
        if ( !shouldMove && CanMoveInDirection( moveDirection, pushAdjecent ) ) {
            shouldMove = true;
            targetPosition = transform.position + moveDirection;
            StartedMoveInDirection( moveDirection, pushAdjecent );

            MatchGround matchGround = GetComponent<MatchGround>();
            if ( matchGround != null ) {
                targetPosition = matchGround.WillMoveTo( targetPosition );
                targetUp = matchGround.GetUpVector( targetPosition );
            }
        }
    }

    public bool IsMoving() {
        return shouldMove;
    }

    public bool CanMoveInDirection( Vector3 moveDirection, bool pushAdjecent ) {
        // Calculate ray start based off move direction and collider size
        RaycastHit[] hits = GetCollisionsInPath( moveDirection );

        for ( int i = 0; i < hits.Length; ++i ) {
            if ( hits[i].transform.gameObject != gameObject ) {
                GridMover mover = hits[i].transform.gameObject.GetComponent<GridMover>();

                if ( mover != null && pushAdjecent && mover.CanMoveInDirection( moveDirection, pushAdjecent ) ) {
                    return true;
                }

                return false;
            }
        }

        return true;
    }

    public RaycastHit[] GetCollisionsInPath( Vector3 moveDirection ) {
        Bounds colliderBounds = GetComponent<Collider>().bounds;
        Vector3 rayStartPosition = colliderBounds.center;

        rayStartPosition.x += ( moveDirection.x * ( colliderBounds.extents.x - 0.04f ) );
        rayStartPosition.y += ( moveDirection.y * ( colliderBounds.extents.y - 0.04f ) );
        rayStartPosition.z += ( moveDirection.z * ( colliderBounds.extents.z - 0.04f ) );

        return Physics.RaycastAll( rayStartPosition, transform.TransformDirection( moveDirection ), 1.0f );
    }

    public void StartedMoveInDirection( Vector3 moveDirection, bool pushAdjecent ) {
        RaycastHit[] hits = GetCollisionsInPath( moveDirection );

        for ( int i = 0; i < hits.Length; ++i ) {
            if ( hits[i].transform.gameObject != gameObject ) {
                GridMover mover = hits[i].transform.gameObject.GetComponent<GridMover>();

                if ( mover != null ) {
                    mover.Move( moveDirection, pushAdjecent );
                }
            }
        }
    }
}
