using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    public Vector3 Direction = new Vector3(0, -1, 0);
    public float Speed = 25.0f;
    public float TorqueForce = 0;
    public bool UseCollisionInversion = false;
    public bool AllowDestroyerCI = false;
    public List<AudioClip> OnFall = new List<AudioClip>();
    public Rigidbody Body = null;
    public AudioSource Source = null;

    void Awake()
    {
        gameObject.tag = "PhysX";
        Body = GetComponent<Rigidbody>();
        Body.useGravity = false;
        Direction *= Speed;
        Source = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision Collision)
    {
        if (Source && OnFall.Count > 0)
        {
            Source.clip = OnFall[Random.Range(0, OnFall.Count - 1)];
            Source.Play();
        }
    }
    void OnCollisionStay(Collision Collision)
    {
        if (UseCollisionInversion)
        {
            Direction = -Direction;
            Body.velocity -= Direction * Speed * Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (TorqueForce != 0.0f)
            Body.angularVelocity += new Vector3(0, 0, TorqueForce * Random.Range(-100, 100) / 100.0f);
        Body.velocity += Direction * Time.deltaTime;
    }
}