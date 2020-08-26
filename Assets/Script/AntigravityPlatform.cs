using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntigravityPlatform : MonoBehaviour 
{
	void OnCollisionStay(Collision Collision)
	{
		if (Collision.gameObject.tag == "PhysX")
		{
			Player Script = Collision.gameObject.GetComponent<Player>();
			if (Script)
			{
				Script.Direction = -Script.Direction;
				Script.Body.velocity -= Script.Direction * 10.0f * Time.deltaTime;
			}
			else
			{
				PhysicalObject Obj = Collision.gameObject.GetComponent<PhysicalObject>();
				if (Obj) 
				{
					Obj.Direction = -Obj.Direction;
					Obj.Body.velocity -= Obj.Direction * 10.0f * Time.deltaTime;
				}
			}
		}
	}
}
