using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionResetter : MonoBehaviour {
    private Vector3 targetPosition;
    private bool resetPostion = false;
    private GridMover mover;

    public delegate void OnReset();
    public OnReset WasReset;

    void Start() {
        mover = GetComponent<GridMover>();
    }

    void Update() {
        if ( resetPostion ) {
            transform.position = Vector3.MoveTowards( transform.position, targetPosition, mover.moveSpeed );

            if ( transform.position == targetPosition ) {
                if ( WasReset != null ) {
                    WasReset();
                }

                resetPostion = false;
            }
        }
    }

    public void DoReset() {
        resetPostion = true;
    }

    public void StorePosition() {
        targetPosition = transform.position;
    }
}
