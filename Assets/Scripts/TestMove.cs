using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {

    public Vector3 moveDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if ( Input.GetKeyDown( KeyCode.T ) ) {
            GetComponent<GridMover>().Move( moveDirection );
        }
	}
}
