using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameBehaviour
{
	[SerializeField] Vector3 angleOffset = new Vector3(90,0,0);
	int damage;

	public void Shoot(Vector3 pForce, int pDamage)
	{
		rb.AddForce(pForce);
		damage = pDamage;

		transform.Rotate(Quaternion.LookRotation(pForce.normalized).eulerAngles + angleOffset);
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("OnCollisionEnter " + collision.gameObject.name);
		IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
			damagable?.OnHit(damage);
		


		Destroy(gameObject);
	}
}
