using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    public ButtonActivation ActivationHandler;

    public delegate void SwitchPressed();
    public SwitchPressed OnSwitchPressed;

    private bool canToggle = true;
    private bool isActive = false;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void ActivateSwitch() {
        isActive = !isActive;

        if ( ActivationHandler != null ) {
            ActivationHandler.OnActivated( this );
        }

        if ( OnSwitchPressed != null ) {
            OnSwitchPressed();
        }
    }

    private void NotifyActivation() {
    }

    public void SetAllowToggle( bool allow ) {
        canToggle = allow;
    }

    public bool CanBeToggled() {
        return canToggle;
    }
}
