using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shading : MonoBehaviour
{
    public Color Emission = new Color(1, 1, 1, 1);
    private MeshRenderer Renderer = null;

    void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
    }
	void Update()
    {
        Renderer.material.color = Emission;
		Renderer.material.SetColor("_EmissionColor", Emission);
    }
}