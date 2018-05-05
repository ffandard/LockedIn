using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveToMax : MonoBehaviour {
    public delegate void OnMoveComplete();

    private Vector3 moveDirection;
    private bool isMoving = false;
    private GridMover currentMover;
    private GridCollisionResolver collisionResolver;

	// Use this for initialization
	void Start () {
        currentMover = GetComponent<GridMover>();
        collisionResolver = GetComponent<GridCollisionResolver>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( isMoving && !currentMover.IsMoving() ) {
            if ( collisionResolver.CanMoveInDirection( moveDirection ) ) {
                currentMover.Move( moveDirection );
            } else {
                isMoving = false;
            }
        }
	}

    public void Move( Vector3 direction ) {
        isMoving = true;
        moveDirection = direction;
        currentMover.Move( direction );
    }
}
