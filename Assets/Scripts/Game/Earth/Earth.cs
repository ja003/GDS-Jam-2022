using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour, IDamagable
{
	[SerializeField] float RotationSpeed = 1;

	public void OnHit(int pDamage)
	{
	}

	private void FixedUpdate()
	{
		transform.Rotate(Vector3.up, -RotationSpeed);
	}
}
