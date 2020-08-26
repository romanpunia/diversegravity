using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDraw : MonoBehaviour
{
	public int MaxPoints = 360;
	private TextMesh Text = null;
	private int PointCount = 0;

	void Awake()
	{
		Text = GetComponent<TextMesh> ();
		PointCount = PlayerPrefs.GetInt ("DATA_POINT_COUNT");
	}
	void Update ()
	{
		Text.text = PointCount + " / " + MaxPoints;
	}
}
