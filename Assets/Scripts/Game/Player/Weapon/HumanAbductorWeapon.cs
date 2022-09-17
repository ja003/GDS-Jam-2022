using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAbductorWeapon : WeaponBase
{
	[SerializeField] float range = 2;
	[SerializeField] LayerMask ScientistLayer;
	protected override void Use(Vector3 pDirection)
	{
		Debug.DrawLine(transform.position, transform.position + pDirection.normalized * range, Color.red, 1);
		Debug.Log("ABDUCT");

		RaycastHit hit;

		if(Physics.Raycast(new Ray(transform.position, pDirection.normalized * range), out hit, range, (LayerMask)ScientistLayer))
		{
			Destroy(hit.transform.gameObject);
		}
	}
}
