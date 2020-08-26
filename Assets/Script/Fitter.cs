using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitter : MonoBehaviour 
{
	void Awake () 
    {
        Screen.SetResolution(800, 480, true);
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
	}
}
