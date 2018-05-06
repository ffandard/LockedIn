using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockPinActivator : ButtonActivation {
    public GameObject[] Pistons;
    public GameObject[] Pillars;
    public float[] UnlockCombination;

    public float dropDelay = 1.0f;
    
    public override void OnActivated( Switch source ) {
        Invoke( "DropPins", dropDelay );
        source.SetAllowToggle( false );
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

    public bool IsUnlocked() {
        bool result = true;

        for (int i = 0; i < Pillars.Length; ++i) {
            GameObject pillar = Pillars[i];
            float unlockStep = UnlockCombination[i];
            if (Math.Abs(pillar.transform.position.y - unlockStep) < 0.5) {
                result = false;
                break;
            }
        }

        return result;
    }
}
