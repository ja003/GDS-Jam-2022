using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameBehaviour
{
	int damage;
	public void Shoot(Vector3 pForce, int pDamage)
	{
		rb.AddForce(pForce);
		damage = pDamage;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("OnCollisionEnter " + collision.gameObject.name);
		IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
			damagable?.OnHit(damage);
		


		Destroy(gameObject);
	}
}
