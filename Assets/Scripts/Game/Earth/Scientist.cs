using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scientist : GameBehaviour, IDamagable
{
    [SerializeField] float Force = 0.2f;

    [SerializeField] public int MinXp = 5;
    [SerializeField] public int MaxXp = 10;
    [SerializeField] public int ReduceDetection = 10;

    Vector3 spawnPosition;
    private void Awake()
    {
        spawnPosition = transform.position;

    }
    bool isBeingAbducted = false;
    internal void AddForce(Vector3 pForce)
    {
        rb.AddForce(pForce);
        isBeingAbducted = true;
        //Debug.Log("AddForce = " + pForce);
    }

    private void FixedUpdate()
    {
        Vector3 dir = (spawnPosition - transform.position).normalized;
        rb.AddForce(dir * Force);
    }

	internal void Abduct()
	{
        if (!isBeingAbducted) return;

        game.Player.Stats.AddXP(Random.Range(MinXp, MaxXp));
        game.Player.Stats.AddDetection(-ReduceDetection);

        Destroy(gameObject);
    }

    public void OnHit(int pDamage)
    {
        game.Player.Stats.AddDetection(5);
    }
}
