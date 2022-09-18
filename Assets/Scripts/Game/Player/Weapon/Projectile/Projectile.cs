using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameBehaviour
{
	[SerializeField] Vector3 angleOffset = new Vector3(90,0,0);
	[SerializeField] float speed = 1;
	int damage;

	Vector3 Direction;

    public void Shoot(Vector3 pForce, int pDamage)
	{
		Direction = pForce;

        //rb.AddForce(pForce);
		damage = pDamage;

		transform.Rotate(Quaternion.LookRotation(pForce.normalized).eulerAngles + angleOffset);
	}

	private void Update()
	{
		transform.position += Direction.normalized * speed;
	}

	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("OnTriggerEnter " + other.gameObject.name);
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();

		if (damagable != null)
		{
			damagable?.OnHit(damage);
            Destroy(gameObject);

        }



    }

	//private void OnCollisionEnter(Collision collision)
	//{
	//	//Debug.Log("OnCollisionEnter " + collision.gameObject.name);
	//	IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

 //       if (damagable != null)
	//		damagable?.OnHit(damage);
		


	//	Destroy(gameObject);
	//}
}
