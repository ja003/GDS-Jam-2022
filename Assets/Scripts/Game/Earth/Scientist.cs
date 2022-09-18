using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : GameBehaviour
{
    [SerializeField] float Force = 0.2f;

    Vector3 spawnPosition;
    private void Awake()
    {
        spawnPosition = transform.position;

    }
    internal void AddForce(Vector3 pForce)
    {
        rb.AddForce(pForce);
        //Debug.Log("AddForce = " + pForce);
    }

    private void FixedUpdate()
    {
        Vector3 dir = (spawnPosition - transform.position).normalized;
        rb.AddForce(dir * Force);
    }
}
