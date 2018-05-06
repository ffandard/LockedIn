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

	private float lerpProgress;

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
			lerpProgress += Time.deltaTime / lookSpeed;
			cinematicCamera.transform.rotation = Quaternion.Lerp( cinematicCamera.transform.rotation, playerCamera.transform.rotation, (lerpProgress*lerpProgress + (1-(1-lerpProgress)*(1-lerpProgress)))*0.5f);

            if ( cinematicCamera.transform.rotation == playerCamera.transform.rotation ) {
                isReseting = false;
                isMoving = false;

                EnableMovement();
            }
        } else if ( isMoving ) {
			lerpProgress += Time.deltaTime / lookSpeed;
            Quaternion targetLook = Quaternion.LookRotation( targetLookAt.position - cinematicCamera.transform.position );
			cinematicCamera.transform.rotation = Quaternion.Lerp( cinematicCamera.transform.rotation, targetLook, (lerpProgress*lerpProgress + (1-(1-lerpProgress)*(1-lerpProgress)))*0.5f);
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
		lerpProgress = 0f;
        isMoving = true;
    }

    public void EnableMovement() {
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        playerCamera.enabled = true;
        cinematicCamera.enabled = false;
    }

    private void ResetCamera() {
		lerpProgress = 0f;
        isReseting = true;
    }
}
