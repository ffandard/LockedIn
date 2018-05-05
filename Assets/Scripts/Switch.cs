using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    public ButtonActivation ActivationHandler;

    private bool canToggle = true;
    private bool isActive = false;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void Toggle() {
        isActive = !isActive;

        if ( ActivationHandler != null ) {
            ActivationHandler.OnActivated( this );
        }
    }

    public void SetAllowToggle( bool allow ) {
        canToggle = allow;
    }

    public bool CanBeToggled() {
        return canToggle;
    }
}
