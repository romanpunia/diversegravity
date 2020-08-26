using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshLayer : MonoBehaviour
{
	public int Layer = 5;

	void Start () 
	{
		MeshRenderer Renderer = gameObject.GetComponent<MeshRenderer> ();
		if (Renderer)
			Renderer.sortingOrder = Layer;
	}
}
