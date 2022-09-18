using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;
using Random = UnityEngine.Random;

public class AsteroidGenerator : GameBehaviour
{
    public float Radius = 100;
    public float EarthOffset = 2;
    public Earth Earth;
    public Asteroid Asteroid;
    public int Frequency = 10;

    private void Awake()
    {
        Generate();
    }

    List<Asteroid> Asteroids = new List<Asteroid>();

    private void Generate()
    {
        for (int i = Asteroids.Count - 1; i >= 0; i--)
        {
            Asteroid ast = Asteroids[i];
            if(ast == null)
                continue;

            float dist = Vector3.Distance(Earth.transform.position, ast.gameObject.transform.position);
            if (dist > Radius)
            {
                Asteroids.RemoveAt(i);
                Destroy(ast.gameObject);
            }
        }

        

        Vector3 randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        Vector3 spawnPos = Earth.transform.position + randDir.normalized * Radius;

        randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        Vector3 direction = (Earth.transform.position + randDir.normalized * EarthOffset) - spawnPos;

        var asteroid = Instantiate(Asteroid, spawnPos, Quaternion.identity, game.AsteroidsHolder);
        Asteroids.Add(asteroid);
        asteroid.Spawn(direction);

        DoInTime(Generate, Frequency);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Earth.transform.position, Radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Earth.transform.position, EarthOffset);

    }
}
