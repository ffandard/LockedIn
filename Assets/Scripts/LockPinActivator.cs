using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPinActivator : ButtonActivation {
    public GameObject[] Pistons;
    public GameObject[] Pillars;

    public float dropDelay = 1.0f;
    
    public override void OnActivated( Switch source ) {
        Invoke( "DropPins", dropDelay );
        source.SetAllowToggle( false );
    }

    private void Update() {
        if ( Input.GetKeyDown( KeyCode.P ) ) {
            DropPins();
        }

        if ( Input.GetKeyDown( KeyCode.R ) ) {
            ResetLock();
        }
    }

    private void DropPins() {
        for ( int i = 0; i < Pistons.Length; ++i ) {
            Pistons[i].GetComponent<PositionResetter>().StorePosition();
            Pillars[i].GetComponent<PositionResetter>().StorePosition();

            Pistons[i].GetComponent<GridMoveToMax>().Move( new Vector3( 0.0f, -1.0f, 0.0f ) );
        }
    }

    public void ResetLock() { 
        for ( int i = 0; i < Pistons.Length; ++i ) {
            Pistons[i].GetComponent<PositionResetter>().DoReset();
            Pillars[i].GetComponent<PositionResetter>().DoReset();
        }
    }
}
