using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematic : MonoBehaviour {
    public Transform targetLookAt;
    public GameObject player;
    public Camera playerCamera;
    public Camera cinematicCamera;

    public float lookSpeed;

    public float successDelay = 2.0f;
    public float failDelay = 1.0f;

    private bool isMoving = false;
    private bool isReseting = false;

    private void Start() {
        Switch targetSwitch = GetComponent<Switch>();
        if ( targetSwitch != null ) {
            targetSwitch.OnSwitchPressed += RunCinematic;
        }

        LockPinActivator lockActivator = GetComponent<LockPinActivator>();

        lockActivator.FailedUnlock += () => Invoke( "ResetCamera", failDelay );
        lockActivator.Unlocked += () => Invoke( "ResetCamera", successDelay );
    }

    public void Update() {
        if ( isReseting ) {
            cinematicCamera.transform.rotation = Quaternion.Lerp( cinematicCamera.transform.rotation, playerCamera.transform.rotation, lookSpeed );

            if ( cinematicCamera.transform.rotation == playerCamera.transform.rotation ) {
                isReseting = false;
                isMoving = false;

                EnableMovement();
            }
        } else if ( isMoving ) {
            Quaternion targetLook = Quaternion.LookRotation( targetLookAt.position - cinematicCamera.transform.position );
            cinematicCamera.transform.rotation = Quaternion.Lerp( cinematicCamera.transform.rotation, targetLook, lookSpeed );
        }
    }

    public void RunCinematic() {
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        playerCamera.enabled = false;
        cinematicCamera.enabled = true;

        Transform playerCamTransform = playerCamera.gameObject.transform;
        Transform cinematicTransform = cinematicCamera.gameObject.transform;

        cinematicTransform.position = playerCamTransform.position;
        cinematicTransform.rotation = playerCamTransform.rotation;

        isMoving = true;
    }

    public void EnableMovement() {
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        playerCamera.enabled = true;
        cinematicCamera.enabled = false;
    }

    private void ResetCamera() {
        isReseting = true;
    }
}
