using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class PsychoShader : MonoBehaviour 
{
	[SerializeField]
	private RenderTexture rendTex1;
	[SerializeField]
	private RenderTexture rendTex2;
	private Material mat;

	private const string shaderName = "Unlit/PsychadelicWorld";

	private bool odd = false;


	void OnEnable () 
	{
		if (rendTex1 == null || rendTex2== null) 
		{
			return;
		}
		Shader shader = Shader.Find(shaderName);

		if (shader != null) 
		{
			mat = new Material (shader);
		}

		Graphics.Blit (rendTex2, rendTex1, mat, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (rendTex1 == null || rendTex2== null) 
		{
			return;
		}

		Graphics.Blit (rendTex1, rendTex2, mat,1);
		Graphics.Blit (rendTex2, rendTex1, mat,1);

	}
}
