using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInteraction : MonoBehaviour {
    // This is the object that is being dragged
    private GridMover toMove = null;

    public float distanceTarget = 1.5f;
    public float breakDistance = 5.0f;
    public float breakAngle = 0.5f;

    void Start () {
		
	}
	
	void Update () {
        if ( toMove != null ) {
            Vector3 toObject = toMove.transform.position - transform.position;
            toObject.Normalize();

            if ( Vector3.Distance( transform.position, toMove.transform.position ) > breakDistance || Vector3.Dot( transform.forward, toObject ) < breakAngle || Input.GetKeyUp( KeyCode.Mouse0 ) ) {
                toMove = null;
            } else {
                // Figure out if this thing needs to move
                Vector3 zeroedY = new Vector3( transform.forward.x, 0.0f, transform.forward.z );
                zeroedY.Normalize();

                Vector3 targetedPosition = transform.position + ( zeroedY * distanceTarget );
                float xDiff = targetedPosition.x - toMove.transform.position.x;
                float zDiff = targetedPosition.z - toMove.transform.position.z;

                if ( Mathf.Abs( xDiff ) > 0.5f ) {
                    if ( xDiff < 0 ) {
                        toMove.Move( Vector3.left, false, false );
                    } else {
                        toMove.Move( Vector3.right, false, false );
                    }
                } else if ( Mathf.Abs( zDiff ) > 0.5f ) {
                    if ( zDiff < 0 ) {
                        toMove.Move( Vector3.back, false, false );
                    } else {
                        toMove.Move( Vector3.forward, false, false );
                    }
                }
            }
        }
    }

    public void SetObjectToMove(GridMover movable) {
        toMove = movable;        
    }

    public bool IsDragging() {
        return toMove != null;
    }
}
