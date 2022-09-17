using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameBehaviour
{
	public void Shoot(Vector3 pForce)
	{
		rb.AddForce(pForce);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("OnCollisionEnter " + collision.gameObject.name);
		Destroy(gameObject);
	}
}
