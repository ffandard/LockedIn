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
				InteractibleHighlight highlight = raycastHit.transform.gameObject.GetComponent<InteractibleHighlight> ();
				if (highlight != null) {
					highlight.Highlight ();
				}
                GridMover mover = raycastHit.transform.gameObject.GetComponent<GridMover>();
                // This is a cube that can be moved
                if ( mover != null ) {
                    if ( Input.GetKeyDown( KeyCode.Mouse0 ) ) {
						if (highlight != null) {
							highlight.Highlight ();
						}
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
                        if ( Vector3.Dot( toggle.gameObject.transform.forward, transform.forward ) > 0.0f ) {
                            isDisplayingInstructions = true;
                            instructionalText = "Press 'E' to activate";

                            if ( Input.GetKeyDown( KeyCode.E ) ) {
                                toggle.ActivateSwitch();
                                isDisplayingInstructions = false;
                            }
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
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = 30;
            centeredStyle.normal.textColor = Color.white;
            centeredStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 0f, 0f, 1.5f ) );

            GUI.Label( new Rect( Screen.width * 0.5f - 300, Screen.height - 80, 600, 50 ), instructionalText, centeredStyle );
        }
    }

    private Texture2D MakeTex( int width, int height, Color col ) {
        Color[] pix = new Color[width * height];
        for ( int i = 0; i < pix.Length; ++i ) {
            pix[i] = col;
        }
        Texture2D result = new Texture2D( width, height );
        result.SetPixels( pix );
        result.Apply();
        return result;
    }
}
