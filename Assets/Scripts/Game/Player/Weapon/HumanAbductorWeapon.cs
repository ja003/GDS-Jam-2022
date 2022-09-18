using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanAbductorWeapon : WeaponBase
{
	[SerializeField] float Range = 2;
	[SerializeField] float Force = 1f;
	[SerializeField] float NeededDist = 1;
	[SerializeField] float RayScaleMultiplier = 1;
	
	[SerializeField] LayerMask ScientistLayer;
	[SerializeField] Transform RayPivot;
	[SerializeField] GameObject Ray;

	Scientist targetedScientist;

	private void Awake()
	{
		Ray.SetActive(false);
	}

	
	AudioSource audioPlayer;
    private void Start()
    {
		audioPlayer = GetComponent<AudioSource>();
    }

    protected override void Use(Vector3 pDirection)
	{
		var overlaps = Physics.OverlapSphere(transform.position, Range, ScientistLayer);
		foreach(var overlap in overlaps)
		{
			targetedScientist = overlap.GetComponent<Scientist>();
			if (targetedScientist != null)
            {
				audioPlayer?.Play();
				break;
            }
		}

		if(targetedScientist != null)
			Ray.SetActive(true);
	}

	private void FixedUpdate()
	{
		if (targetedScientist == null) 
			return;

		Vector3 dir = transform.position - targetedScientist.transform.position;
		Ray.transform.SetPositionAndRotation(Ray.transform.position, Quaternion.LookRotation(-dir, Vector3.up));
		
		float dist = dir.magnitude;
		Ray.transform.localScale = Vector3.one * dist * RayScaleMultiplier;

		if(dist > Range + 1)
		{
			StopUse();
			return;
		}

		if (dist < NeededDist)
		{
			targetedScientist.Abduct();			
			return;
		}


        targetedScientist.AddForce(dir.normalized * Force);

    }

	internal void StopUse()
	{
		targetedScientist = null;
		Ray.SetActive(false);
		audioPlayer?.StopAndReset();
    }
}
