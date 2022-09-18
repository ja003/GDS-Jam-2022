using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EReward
{
	XP,
	Ammo
}

public class RewardObject : GameBehaviour, IDamagable
{
	[SerializeField] int RotationSpeed = 1;
	[SerializeField] float MaxInitForce = 20;
	public int Amount;
	public EReward Type;

	internal void OnSpawn()
	{
		Vector3 force = new Vector3(Random.Range(-MaxInitForce, MaxInitForce), Random.Range(-MaxInitForce, MaxInitForce), 0);
		rb.AddForce(force);
		Debug.Log(force);
	}

	private void FixedUpdate()
	{
		transform.Rotate(Vector3.up, RotationSpeed);
	}

	public void OnHit(int pDamage)
	{
		Destroy(gameObject);

	}

	internal void Take()
	{
		game.Player.AddReward(this);

		Destroy(gameObject);

	}

}
