using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour 
{
	public float MinScale = 0.25f;
	public float ScaleSpeed = 3;
	public bool IsCollided = false;

	void OnTriggerEnter(Collider Collision)
	{
		if (Collision.gameObject.tag == "PhysX") 
		{
			Player Obj = Collision.gameObject.GetComponent<Player> ();
			if (Obj)
				StartCoroutine (AddPoint (Obj));
		}
	}
	void Update()
	{
		if (IsCollided)
			SetToMinScale (new Vector3 (MinScale, MinScale, MinScale), ScaleSpeed);
	}
	public IEnumerator AddPoint(Player Script)
	{
		IsCollided = true;
		Script.SetColor (Script.OnPointAddColor);
        Script.CanColorBeChanged = false;

        if (Script.Source && Script.OnAddPoint.Count > 0)
        {
            Script.Source.clip = Script.OnAddPoint[Random.Range(0, Script.OnAddPoint.Count - 1)];
            Script.Source.Play();
        }

		yield return new WaitForSeconds (Script.PointTime);
		
        Script.CanColorBeChanged = true; Script.PointCount++;
		Script.SetColor (Script.IsColliding ? Script.GroundColor : Script.AirColor);
		gameObject.SetActive(false); Destroy (gameObject);
	}
	void SetToMinScale(Vector3 Scale, float Speed)
	{
		float X = transform.localScale.x;
		if (X > Scale.x)
			X -= Speed * Time.deltaTime;
		else X = Scale.x;

		float Y = transform.localScale.y;
		if (Y > Scale.y)
			Y -= Speed * Time.deltaTime;
		else Y = Scale.y;

		float Z = transform.localScale.z;
		if (Z > Scale.z)
			Z -= Speed * Time.deltaTime;
		else Z = Scale.z;

		transform.localScale = new Vector3(Mathf.Abs(X), Mathf.Abs(Y), Mathf.Abs(Z));
	}
}
