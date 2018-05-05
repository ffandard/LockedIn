using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamActivator : ButtonActivation {
    public GridMoveToMax targetBeam;
    public Vector3 moveDirection;
    public PositionResetter resetter;

    private Switch targetSwitch;
    private bool beamExtended = false;

    void Start() {
        resetter = targetBeam.GetComponent<PositionResetter>();
        targetBeam.MoveCompleted += BeamExtended;
        resetter.WasReset += BeamRetracted;
    }

    private void BeamRetracted() {
        targetSwitch.SetAllowToggle( true );
    }

    private void BeamExtended() {
        targetSwitch.SetAllowToggle( true );
    }

    public override void OnActivated( Switch source ) {
        targetSwitch = source;
        source.SetAllowToggle( false );

        if ( beamExtended ) {
            resetter.DoReset();
        } else {
            targetBeam.GetComponent<PositionResetter>().StorePosition();
            targetBeam.Move( moveDirection );
        }
        beamExtended = !beamExtended;
    }
}
