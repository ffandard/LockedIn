using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleHighlight : MonoBehaviour {

	public static Color highlightColour = new Color (0.5f, 0.6f, 0.9f);
	public static float time = 0.05f;

	private List<Material> mat = new List<Material>();
	private bool highlighted = false;
	private float lerpProgress;

	void Start (){
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in renderers) {
			mat.Add (r.material);
		}
	}

	// Update is called once per frame
	void Update () {
		if (highlighted) 
		{
			if (lerpProgress < 1) {
				lerpProgress += Time.deltaTime/time;
			}
			highlighted = false;
		}
		else if (lerpProgress > 0) {
			lerpProgress -= Time.deltaTime/time;


		}
		foreach (Material m in mat) {
			m.color = Color.Lerp (Color.black, highlightColour, (lerpProgress*lerpProgress + (1-(1-lerpProgress)*(1-lerpProgress)))*0.5f);
		}
	}

	public void Highlight()
	{
		highlighted = true;
	}
		
}
