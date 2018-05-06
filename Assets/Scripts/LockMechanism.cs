using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMechanism : MonoBehaviour {
    public LockPinActivator lockPinActivator;
    public Animator lockAnimator;

    private void Start()
    {
        lockPinActivator.FailedUnlock += FailedUnlock;
        lockPinActivator.Unlocked += Unlocked;
    }

    public void Unlocked()
    {
        lockAnimator.SetTrigger("Open");
    }

    public void FailedUnlock()
    {
        lockAnimator.SetTrigger("FailOpen");
    }
}
