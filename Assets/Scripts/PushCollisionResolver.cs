using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCollisionResolver : GridCollisionResolver {
    public override bool CanMoveInDirection( Vector3 moveDirection ) {
        // Calculate ray start based off move direction and collider size
        RaycastHit[] hits = GetCollisionsInPath( moveDirection );

        for ( int i = 0; i < hits.Length; ++i ) {
            if ( hits[i].transform.gameObject != gameObject ) {
                GridMover mover = hits[i].transform.gameObject.GetComponent<GridMover>();

                if ( mover != null && mover.GetComponent<GridCollisionResolver>().CanMoveInDirection( moveDirection ) ) {
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

        return Physics.RaycastAll( rayStartPosition, moveDirection, 1.0f );
    }

    public override void StartedMoveInDirection( Vector3 moveDirection ) {
        RaycastHit[] hits = GetCollisionsInPath( moveDirection );
        Debug.Log( "Called started move" + moveDirection.ToString() );
        for ( int i = 0; i < hits.Length; ++i ) {
            if ( hits[i].transform.gameObject != gameObject ) {
                GridMover mover = hits[i].transform.gameObject.GetComponent<GridMover>();

                if ( mover != null ) {
                    mover.Move( moveDirection );
                }
            }
        }
    }
}
