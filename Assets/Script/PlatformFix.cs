using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFix : MonoBehaviour
{
	public Vector3 MinPosition = new Vector3 ();
	public Vector3 MaxPosition = new Vector3 ();
	public float PositionSpeed = 1;
	public bool Position = false;

	void OnCollisionStay (Collision Collision) 
	{
		if (Collision.gameObject.tag == "PhysX")
		{
			Collision.transform.position = new Vector3 (
				Collision.transform.position.x + (MaxPosition.x != MinPosition.x ? (Position ? 1 : -1) * PositionSpeed * Time.deltaTime : 0),
				Collision.transform.position.y + (MaxPosition.y != MinPosition.y ? (Position ? 1 : -1) * PositionSpeed * Time.deltaTime : 0), 
				Collision.transform.position.z + (MaxPosition.z != MinPosition.z ? (Position ? 1 : -1) * PositionSpeed * Time.deltaTime : 0));
		}
	}
}
