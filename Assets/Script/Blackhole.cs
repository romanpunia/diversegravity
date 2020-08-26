using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public float Radius = 10.0f;
    public float Speed = 3.0f;
    public AudioSource Source = null;

    void Awake()
    {
        Source = GetComponent<AudioSource>();
        if (!Source)
            return;

        Source.loop = true;
        Source.Play();
    }
    void FixedUpdate()
    {
        GameObject[] Obj = GameObject.FindGameObjectsWithTag("PhysX");
        for (int i = 0; i < Obj.Length; i++)
        {
            Rigidbody Body = Obj[i].GetComponent<Rigidbody>();
            if (!Body)
                continue;

            float Distance = Vector3.Distance(Obj[i].transform.position, transform.position);
            if (Distance <= Radius)
                Body.velocity -= (Obj[i].transform.position - transform.position).normalized * Time.deltaTime * Speed;
        }
    }
}