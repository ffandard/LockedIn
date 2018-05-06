using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematic : MonoBehaviour {
    public Transform targetLookAt;
    public GameObject player;
    public Camera playerCamera;
    public Camera cinematicCamera;

    public float lookSpeed;
    public float disableTime;

    private bool isMoving = false;

    private void Start() {
        Switch targetSwitch = GetComponent<Switch>();
        if ( targetSwitch != null ) {
            targetSwitch.OnSwitchPressed += RunCinematic;
        }
    }

    public void Update() {
        if ( isMoving ) {
            Quaternion targetLook = Quaternion.LookRotation( targetLookAt.position - cinematicCamera.transform.position );
            cinematicCamera.transform.rotation = Quaternion.Lerp( cinematicCamera.transform.rotation, targetLook, lookSpeed );
        }
    }

    public void RunCinematic() {
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        playerCamera.enabled = false;
        cinematicCamera.enabled = true;
        cinematicCamera.gameObject.transform.rotation = playerCamera.gameObject.transform.rotation;

        Invoke( "EnableMovement", disableTime );
        isMoving = true;
    }

    public void EnableMovement() {
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        playerCamera.enabled = true;
        cinematicCamera.enabled = false;
    }
}
