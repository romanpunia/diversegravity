using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
	public GameObject Target = null;
	public Vector3 MinPosition = new Vector3(0, 0, 0);
	public Vector3 MaxPosition = new Vector3(0, 0, 0);
	public Vector3 MinRotation = new Vector3(0, 0, 0);
	public Vector3 MaxRotation = new Vector3(0, 0, 0);
	public Color OnStay = new Color(1, 1, 1, 1);
	public float PositionSpeed = 1;
	public float OpenPositionSpeed = 1;
	public float ClosePositionSpeed = 1;
	public float RotationSpeed = 1;
	public float OpenRotationSpeed = 1;
	public float CloseRotationSpeed = 1;
	public bool Active = false;
	public bool MaxIsLessThanMin = false;
	public bool UseTwoSpeedValsForPosition = false;
	public bool UseTwoSpeedValsForRotation = false;
	private Shading Shading = null;
	private Color Default = new Color();
	private PlatformFix Fix = null;

	void Awake()
	{
		Shading = GetComponent<Shading> ();
		Default = Shading.Emission;

		Fix = Target.GetComponent<PlatformFix> ();
	}
	void OnCollisionExit(Collision Collision)
	{
		Shading.Emission = Default;
		Active = false;
	}
	void OnCollisionStay(Collision Collision)
	{
		if (Collision.gameObject.tag == "PhysX") 
		{
			Shading.Emission = OnStay;
			Active = true;
		}
	}
	void Update () 
	{
		Vector3 Rotation = Target.transform.localRotation.eulerAngles;
		if (Active)
		{
			if (Fix)
			{
				Fix.MinPosition = MinPosition;
				Fix.MaxPosition = MaxPosition;
				Fix.PositionSpeed = PositionSpeed * (MaxIsLessThanMin ? -1 : 1);
				Fix.Position = Active;
			}
			Vector3 Last = Target.transform.localPosition;

			if (MaxIsLessThanMin)
				Target.transform.localPosition = LerpMin (Target.transform.localPosition, MaxPosition, MinPosition, UseTwoSpeedValsForPosition ? OpenPositionSpeed : PositionSpeed);
			else
				Target.transform.localPosition = LerpMax (Target.transform.localPosition, MinPosition, MaxPosition, UseTwoSpeedValsForPosition ? OpenPositionSpeed : PositionSpeed);
			Rotation = LerpMax (Rotation, MinRotation, MaxRotation, UseTwoSpeedValsForRotation ? OpenRotationSpeed : RotationSpeed);

			if (Last == Target.transform.localPosition)
				Fix.PositionSpeed = 0;
		}
		else
		{	
			if (Fix)
			{
				Fix.MinPosition = MinPosition;
				Fix.MaxPosition = MaxPosition;
				Fix.PositionSpeed = PositionSpeed * (MaxIsLessThanMin ? -1 : 1);
				Fix.Position = Active;
			}
			Vector3 Last = Target.transform.localPosition;

			if (!MaxIsLessThanMin)
				Target.transform.localPosition = LerpMin (Target.transform.localPosition, MinPosition, MaxPosition, UseTwoSpeedValsForPosition ? ClosePositionSpeed : PositionSpeed);
			else
				Target.transform.localPosition = LerpMax (Target.transform.localPosition, MaxPosition, MinPosition, UseTwoSpeedValsForPosition ? ClosePositionSpeed : PositionSpeed);
			Rotation = LerpMin (Rotation, MinRotation, MaxRotation, UseTwoSpeedValsForRotation ? CloseRotationSpeed : RotationSpeed);

			if (Last == Target.transform.localPosition)
				Fix.PositionSpeed = 0;
		}
		Target.transform.localRotation = Quaternion.Euler (Rotation);
	}	
	Vector3 LerpMax (Vector3 A, Vector3 Min, Vector3 Max, float Speed)
	{
		float SpeedA = Speed * Time.deltaTime;
		Vector3 Lerp = A;

		if (Lerp.x < Max.x && Min.x != Max.x)
			Lerp.x += SpeedA;
		else if (Lerp.x > Max.x && Min.x != Max.x)
			Lerp.x = Max.x;
		
		if (Lerp.y < Max.y && Min.y != Max.y)
			Lerp.y += SpeedA;
		else if (Lerp.y > Max.y && Min.y != Max.y)
			Lerp.y = Max.y;
		
		if (Lerp.z < Max.z && Min.z != Max.z)
			Lerp.z += SpeedA;
		else if (Lerp.z > Max.z && Min.z != Max.z)
			Lerp.z = Max.z;
		
		return Lerp;
	}
	Vector3 LerpMin (Vector3 A, Vector3 Min, Vector3 Max, float Speed)
	{
		float SpeedA = Speed * Time.deltaTime;	
		Vector3 Lerp = A;

		if (Lerp.x > Min.x && Min.x != Max.x)
			Lerp.x -= SpeedA;
		else if (Lerp.x < Min.x && Min.x != Max.x)
			Lerp.x = Min.x;
		
		if (Lerp.y > Min.y && Min.y != Max.y)
			Lerp.y -= SpeedA;
		else if (Lerp.y < Min.y && Min.y != Max.y)
			Lerp.y = Min.y;

		if (Lerp.z > Min.z && Min.z != Max.z)
			Lerp.z -= SpeedA;
		else if (Lerp.z < Min.z && Min.z != Max.z)
			Lerp.z = Min.z;
		
		return Lerp;
	}
}
