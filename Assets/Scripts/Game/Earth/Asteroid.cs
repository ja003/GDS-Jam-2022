using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameBehaviour
{
    [SerializeField] float Force = 10;
    [SerializeField] int Damage = 5;
    [SerializeField] AudioClip SoundOnDestroyed;

    internal void Spawn(Vector3 pDirection)
    {
        rb.AddForce(pDirection.normalized * Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
            damagable?.OnHit(Damage);


        Utils.PlayOneShotIndependently(SoundOnDestroyed);
        Destroy(gameObject);
    }

}
