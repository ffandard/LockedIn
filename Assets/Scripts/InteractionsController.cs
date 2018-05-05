using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour {
    public float interactionDistance = 2.0f;

    private bool isDisplayingInstructions = false;
    private string instructionalText = "";

	void Start () {
		
	}
	
	void Update () {
        int layerMask = ( 1 << LayerMask.NameToLayer( "Interactable" ) );
        RaycastHit raycastHit;

        if ( Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out raycastHit, interactionDistance, layerMask ) ) {
            GridMover mover = raycastHit.transform.gameObject.GetComponent<GridMover>();
            // This is a cube that can be moved
            if ( mover != null ) {
                isDisplayingInstructions = !Input.GetKey( KeyCode.Mouse0 );
                instructionalText = "Hold Left Mouse Button to move";
            }
        } else {
            isDisplayingInstructions = false;
        }
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
