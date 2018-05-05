using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveToMax : MonoBehaviour {
    public delegate void OnMoveComplete();
    public OnMoveComplete MoveCompleted;

    private Vector3 moveDirection;
    private bool isMoving = false;
    private GridMover currentMover;

	// Use this for initialization
	void Start () {
        currentMover = GetComponent<GridMover>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( isMoving && !currentMover.IsMoving() ) {
            if ( currentMover.CanMoveInDirection( moveDirection, true ) ) {
                currentMover.Move( moveDirection, true );
            } else {
                isMoving = false;
                MoveCompleted();
            }
        }
	}

    public void Move( Vector3 direction ) {
        isMoving = true;
        moveDirection = direction;
    }
}
