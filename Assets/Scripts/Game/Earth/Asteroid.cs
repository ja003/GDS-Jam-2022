using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GameBehaviour, IDamagable
{
    [SerializeField] float Force = 10;
    [SerializeField] int Damage = 5;
    [SerializeField] AudioClip SoundOnDestroyed;

    public void OnHit(int pDamage)
    {
        Die();
    }

    internal void Spawn(Vector3 pDirection)
    {
        rb.AddForce(pDirection.normalized * Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
            damagable?.OnHit(Damage);
        
        Die();
    }

    private void Die()
    {
        Utils.PlayOneShotIndependently(SoundOnDestroyed);
        Destroy(gameObject);
    }
}
