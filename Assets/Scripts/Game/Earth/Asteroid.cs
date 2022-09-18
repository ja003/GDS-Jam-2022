using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameBehaviour
{
    [SerializeField] float Force = 10;

    internal void Spawn(Vector3 pDirection)
    {
        rb.AddForce(pDirection.normalized * Force);
    }

}
