using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMechanism : MonoBehaviour {
    public LockPinActivator lockPinActivator;
    public Animator lockAnimator;

    public Transform reparentTarget;
    public GameObject[] parentOnSuccess;

    private void Start()
    {
        lockPinActivator.FailedUnlock += FailedUnlock;
        lockPinActivator.Unlocked += Unlocked;
    }

    public void Unlocked()
    {
        for ( int i = 0; i < parentOnSuccess.Length; ++i ) {
            parentOnSuccess[i].transform.parent = reparentTarget;
        }

        lockAnimator.SetTrigger("Open");
    }

    public void FailedUnlock()
    {
        lockAnimator.SetTrigger("FailOpen");
    }
}
