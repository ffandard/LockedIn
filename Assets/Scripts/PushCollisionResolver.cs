using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCollisionResolver : GridCollisionResolver {
    public override bool CanMoveInDirection( Vector3 moveDirection ) {
        // Calculate ray start based off move direction and collider size
        Bounds colliderBounds = GetComponent<Collider>().bounds;
        Vector3 rayStartPosition = colliderBounds.center;

        rayStartPosition.x += ( moveDirection.x * ( colliderBounds.extents.x - 0.04f ) );
        rayStartPosition.y += ( moveDirection.y * ( colliderBounds.extents.y - 0.04f ) );
        rayStartPosition.z += ( moveDirection.z * ( colliderBounds.extents.z - 0.04f ) );

        RaycastHit[] hits = Physics.RaycastAll( rayStartPosition, moveDirection, 1.0f );

        for ( int i = 0; i < hits.Length; ++i ) {
            if ( hits[i].transform.gameObject != gameObject ) {
                GridMover mover = hits[i].transform.gameObject.GetComponent<GridMover>();

                if ( mover != null && mover.GetComponent<GridCollisionResolver>().CanMoveInDirection( moveDirection ) ) {
                    mover.Move( moveDirection );

                    return true;
                }

                return false;
            }
        }

        return true;
    }
}
