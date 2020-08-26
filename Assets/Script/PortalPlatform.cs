using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlatform : MonoBehaviour
{
	public enum DirectionType
	{
		DIRECTION_UP,
		DIRECTION_DOWN,
		DIRECTION_LEFT,
		DIRECTION_RIGHT
	}
	public PortalPlatform Link = null;
	public List<GameObject> OnEnterInput = null;
	public bool OnEnterState = false;
	public int EnterTime = 1;
	public DirectionType Type = DirectionType.DIRECTION_UP;
	private Vector3 Direction = new Vector3();
	private List<GameObject> Ignores = new List<GameObject>();

	void Awake()
	{
		switch (Type) 
		{
		case DirectionType.DIRECTION_DOWN:
			Direction = -transform.up;
			break;
		case DirectionType.DIRECTION_UP:
			Direction = transform.up;
			break;
		case DirectionType.DIRECTION_LEFT:
			Direction = -transform.forward;
			break;
		case DirectionType.DIRECTION_RIGHT:
			Direction = transform.up;
			break;
		}
	}
	void OnTriggerEnter(Collider In) 
	{
		for (int i = 0; i < Ignores.Count; i++)
		{
			if (In.gameObject == Ignores [i])
				return;
		}

		if (In.gameObject.tag == "PhysX" && Link != null) 
		{
			In.gameObject.transform.position = Link.transform.position;
			Player Script = In.gameObject.GetComponent<Player> ();
			if (Script)
			{
				float Length = Script.Body.velocity.magnitude;
				Script.Body.velocity = Link.Direction * Length;
				Script.Direction = Link.Direction * Script.Direction.magnitude;
			} 
			else 
			{
				PhysicalObject Obj = In.gameObject.GetComponent<PhysicalObject> ();
				if (Obj)
				{
					float Length = Obj.Body.velocity.magnitude;
					Obj.Body.velocity = Link.Direction * Length;
					Obj.Direction = Link.Direction * Obj.Direction.magnitude;
				}
			}

			EnterTime--;
			if (EnterTime <= 0) 
			{
				for (int i = 0; i < OnEnterInput.Count; i++)
					OnEnterInput [i].SetActive (OnEnterState);
			}

			Link.AddIgnorable (In.gameObject);
		}
	}
	void OnTriggerExit(Collider In) 
	{
		for (int i = 0; i < Ignores.Count; i++)
		{
			if (Ignores [i] == In.gameObject) 
			{
				Ignores.RemoveAt (i);
				i--;
			}
		}
	}
	public void AddIgnorable(GameObject Obj)
	{
		Ignores.Add (Obj);
	}
}
