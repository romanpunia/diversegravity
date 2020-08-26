using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public GameObject Target = null;
    public Color Emission = new Color(1, 1, 1, 1);
    public float Power = 0.25f;
    private SpriteRenderer Renderer = null;

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }
    void OnTriggerExit(Collider Collision)
    {
        if (Collision.gameObject == Target)
        {
            Player Script = Target.GetComponent<Player>();
			Script.Kill ();
        }
    }
	void Update()
    {
        Renderer.material.color = (Emission + GetRandom() * Power) / (1.0f + Power);
        Renderer.material.SetColor("_EmissionColor", (Emission + GetRandom() * Power) / (1.0f + Power));
    }
    Color GetRandom()
    {
        return new Color(Random.Range(0, 255) / 255.0f, Random.Range(0, 255) / 255.0f, Random.Range(0, 255) / 255.0f, 1);
    }
}