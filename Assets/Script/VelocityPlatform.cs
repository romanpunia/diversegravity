using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPlatform : MonoBehaviour 
{	public enum DirectionType
	{
		DIRECTION_NONE,
		DIRECTION_UP,
		DIRECTION_DOWN,
		DIRECTION_LEFT,
		DIRECTION_RIGHT
	}
	public Vector3 Direction = new Vector3(1, 0, 0);
	public float Speed = 25.0f;
	public bool VelocityMove = false;
	public DirectionType Type = DirectionType.DIRECTION_UP;
	private Vector3 IDirection = new Vector3();

	void Awake()
	{
		switch (Type) 
		{
		case DirectionType.DIRECTION_DOWN:
			IDirection = -transform.up;
			break;
		case DirectionType.DIRECTION_UP:
			IDirection = transform.up;
			break;
		case DirectionType.DIRECTION_LEFT:
			IDirection = -transform.forward;
			break;
		case DirectionType.DIRECTION_RIGHT:
			IDirection = transform.up;
			break;
		}
	}
	void OnCollisionStay(Collision Collision)
	{
		if (Collision.gameObject.tag == "PhysX")
		{
			Vector3 LastDirection = Direction;
			if (Type != DirectionType.DIRECTION_NONE)
				Direction = new Vector3(Direction.x * IDirection.x, Direction.y * IDirection.y, Direction.z * IDirection.z);

			if (VelocityMove)
			{
				Player PlayerA = Collision.gameObject.GetComponent<Player> ();
				if (!PlayerA) 
				{
					Collision.transform.position = new Vector3 (
						Collision.transform.position.x + Direction.x * Speed * Time.deltaTime,
						Collision.transform.position.y + Direction.y * Speed * Time.deltaTime, 
						Collision.transform.position.z + Direction.z * Speed * Time.deltaTime);
				} 
				else
					PlayerA.Direction += Direction * Speed;		
			} 
			else 
			{
				Collision.transform.position = new Vector3 (
					Collision.transform.position.x + Direction.x * Speed * Time.deltaTime,
					Collision.transform.position.y + Direction.y * Speed * Time.deltaTime, 
					Collision.transform.position.z + Direction.z * Speed * Time.deltaTime);
			}

			Direction = LastDirection;
		}
	}
}
