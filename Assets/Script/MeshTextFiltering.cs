using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTextFiltering : MonoBehaviour 
{
	public FilterMode Mode = FilterMode.Point;

	void Start () 
	{
		TextMesh Mesh = gameObject.GetComponent<TextMesh> ();
		Mesh.font.material.mainTexture.filterMode = Mode;
	}
}
