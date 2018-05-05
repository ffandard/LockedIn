using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour {
    public float interactionDistance = 2.0f;

    private bool isDisplayingInstructions = false;
    private string instructionalText = "";

    private DragInteraction dragger = null;

	void Start () {
        dragger = GetComponent<DragInteraction>();
	}
	
	void Update () {
        if ( !IsInteracting() ) {
            int layerMask = ( 1 << LayerMask.NameToLayer( "Interactable" ) );
            RaycastHit raycastHit;

            if ( Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out raycastHit, interactionDistance, layerMask ) ) {
                GridMover mover = raycastHit.transform.gameObject.GetComponent<GridMover>();
                // This is a cube that can be moved
                if ( mover != null ) {
                    if ( Input.GetKeyDown( KeyCode.Mouse0 ) ) {
                        isDisplayingInstructions = false;
                        dragger.SetObjectToMove( mover );
                    } else {
                        isDisplayingInstructions = true;
                        instructionalText = "Hold Left Mouse Button to move";
                    }
                } else {
                    // Try if this is a switch
                    Switch toggle = raycastHit.transform.gameObject.GetComponent<Switch>();
                    if ( toggle != null && toggle.CanBeToggled() ) {
                        isDisplayingInstructions = true;
                        instructionalText = "Press 'E' to activate";

                        if ( Input.GetKeyDown( KeyCode.E ) ) {
                            toggle.Toggle();
                            isDisplayingInstructions = false;
                        }
                    }
                }
            } else {
                isDisplayingInstructions = false;
            }
        }
    }

    private bool IsInteracting() {
        return dragger.IsDragging();
    }

    void OnGUI() {
        if ( isDisplayingInstructions ) {
            var centeredStyle = GUI.skin.GetStyle( "Label" );
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.fontSize = 30;

            GUI.Label( new Rect( Screen.width * 0.5f - 300, Screen.height - 60, 600, 50 ), instructionalText, centeredStyle );
        }
    }
}
