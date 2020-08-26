using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour 
{
	public Vector3 Offset;
	public Vector4 Limits = new Vector4 (-(float)1e+10, (float)1e+10, -(float)1e+10, (float)1e+10);
	public bool UseXXLimit = false;
	public bool UseYYLimit = false;
	public GameObject Target = null;

	void Update ()
	{
		if (Target) 
		{
			transform.position = Target.transform.position + Offset;

			if (UseXXLimit || UseYYLimit)
			{
				transform.position = new Vector3 (
					UseXXLimit ? Mathf.Min (Mathf.Max (transform.position.x, Limits.x), Limits.y) : transform.position.x,
					UseYYLimit ? Mathf.Min (Mathf.Max (transform.position.y, Limits.z), Limits.w) : transform.position.y, transform.position.z);
			}
		}	
	}
}
