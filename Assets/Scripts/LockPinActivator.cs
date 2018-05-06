using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockPinActivator : ButtonActivation {
    public GameObject[] Pistons;
    public GameObject[] Pillars;
    public float[] UnlockCombination;
    public float dropDelay = 1.0f;

    public delegate void OnUnlocked();
    public OnUnlocked Unlocked;

    public delegate void OnFailedUnlock();
    public OnFailedUnlock FailedUnlock;
    
    private int pinsDropped = 0;
    private bool isUnlocked = false;
    
    public override void OnActivated( Switch source ) {
        for ( int i = 0; i < Pistons.Length; ++i ) {
            Pistons[i].GetComponent<PositionResetter>().StorePosition();
            Pillars[i].GetComponent<PositionResetter>().StorePosition();
        }

        Invoke( "DropPins", dropDelay );
        source.SetAllowToggle( false );

		BeamActivator[] beams = FindObjectsOfType<BeamActivator> ();
		foreach (BeamActivator beam in beams) {
			beam.Reset ();
		}
    }

    private void Start() {
        for ( int i = 0; i < Pistons.Length; ++i ) {
            GridMoveToMax component = Pistons[i].GetComponent<GridMoveToMax>();

            component.MoveCompleted += PinDropped;
        }

        Pillars[0].GetComponent<PositionResetter>().WasReset += () => GetComponent<Switch>().SetAllowToggle( true );
    }
    
    private void DropPins() {
        if ( !isUnlocked ) {
            for (int i = 0; i < Pistons.Length; ++i) {
                Pistons[i].GetComponent<GridMoveToMax>().Move(new Vector3(0.0f, -1.0f, 0.0f));
            }
        }
    }

    public void ResetLock() { 
        if ( !isUnlocked ) {
            for (int i = 0; i < Pistons.Length; ++i) {
                Pistons[i].GetComponent<PositionResetter>().DoReset();
                Pillars[i].GetComponent<PositionResetter>().DoReset();
            }
        }
    }

    public void PinDropped() {
        pinsDropped++;

        if ( pinsDropped == Pillars.Length ) {
            pinsDropped = 0;
            isUnlocked = IsUnlocked();

            if ( isUnlocked ) {
                if (Unlocked != null ) {
                    Unlocked();
                }
            }
            else {
                Invoke("ResetLock", dropDelay);

                if (Unlocked != null) {
                    FailedUnlock();
                }
            }
        }
    }

    public bool IsUnlocked() {
        bool result = true;

        for ( int i = 0; i < Pillars.Length; ++i ) {
            GameObject pillar = Pillars[i];
            float unlockStep = UnlockCombination[i];
            if ( Math.Abs(pillar.transform.position.y - unlockStep) > 0.1f ) {
                result = false;
                break;
            }
        }

        return result;
    }
}
