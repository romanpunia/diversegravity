using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerPlatform : MonoBehaviour 
{
	void OnTriggerEnter(Collider Collision)
	{
		if (Collision.gameObject.tag == "PhysX")
		{
			Player Script = Collision.gameObject.GetComponent<Player>();
			if (Script)
				Script.Kill ();
			else 
			{
				PhysicalObject Obj = Collision.gameObject.GetComponent<PhysicalObject> ();
				if (Obj) 
				{
					if (!Obj.AllowDestroyerCI)
						return;
					
					Obj.Direction = -Obj.Direction;
					Obj.Body.velocity -= Obj.Direction * 10.0f * Time.deltaTime;
				}
			}
		}
	}
}
