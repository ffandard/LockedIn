using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonActivation : MonoBehaviour {
    public abstract void OnActivated();
    public abstract void OnDeactivated();
}
