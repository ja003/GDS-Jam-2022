using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanAbductorWeapon : WeaponBase
{
	[SerializeField] float range = 2;
	[SerializeField] float Force = 1f;
	[SerializeField] float NeededDist = 1;
	[SerializeField] LayerMask ScientistLayer;
	[SerializeField] GameObject Ray;

	Scientist targetedScientist;

	private void Awake()
	{
		Ray.SetActive(false);
	}

	protected override void Use(Vector3 pDirection)
	{
		Debug.DrawLine(transform.position, transform.position + pDirection.normalized * range, Color.red, 1);
		Debug.Log("ABDUCT");

		RaycastHit hit;

		Ray.SetActive(true);

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

		Vector3 dir = transform.position - targetedScientist.transform.position;
		float dist = dir.magnitude;
		if (dist < NeededDist)
		{
			game.Player.Stats.AddXP(Random.Range(targetedScientist.MinXp, targetedScientist.MaxXp));
			game.Player.Stats.AddDetection(-targetedScientist.ReduceDetection);

			Destroy(targetedScientist.gameObject);
			return;
		}


        targetedScientist.AddForce(dir.normalized * Force);

    }

	internal void StopUse()
	{
		targetedScientist = null;
		Ray.SetActive(false);
	}
}
