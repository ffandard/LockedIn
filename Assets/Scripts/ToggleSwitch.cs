using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour {
    public ButtonActivation ActivationHandler;

    private bool canToggle = true;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void Toggle() {
        canToggle = false;
    }

    public bool CanBeToggled() {
        return canToggle;
    }
}
