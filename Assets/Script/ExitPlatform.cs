using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlatform : MonoBehaviour
{
    public GameObject Target = null;

    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject == Target)
        {
            Player Script = Target.GetComponent<Player>();
			Script.PassLevel (gameObject);
        }
    }
}