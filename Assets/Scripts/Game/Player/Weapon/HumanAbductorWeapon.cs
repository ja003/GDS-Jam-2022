using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAbductorWeapon : WeaponBase
{
	[SerializeField] float range = 2;
	[SerializeField] float Force = 1f;
	[SerializeField] LayerMask ScientistLayer;

	Scientist targetedScientist;



	protected override void Use(Vector3 pDirection)
	{
		Debug.DrawLine(transform.position, transform.position + pDirection.normalized * range, Color.red, 1);
		Debug.Log("ABDUCT");

		RaycastHit hit;


        var overlaps = Physics.OverlapSphere(transform.position, range, ScientistLayer);
		foreach(var overlap in overlaps)
		{
			targetedScientist = overlap.GetComponent<Scientist>();
			if (targetedScientist != null)
				break;

			
		}
	}

	private void FixedUpdate()
	{
		if (targetedScientist == null) 
			return;

        Vector3 dir = (transform.position - targetedScientist.transform.position).normalized;
        targetedScientist.AddForce(dir * Force);

    }

	internal void StopUse()
	{
		targetedScientist = null;

    }
}
