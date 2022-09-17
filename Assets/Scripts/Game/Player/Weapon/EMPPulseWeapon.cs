using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPPulseWeapon : WeaponBase
{
	[SerializeField] ParticleSystem particles;
	[SerializeField] int Damage = 1;
	[SerializeField] int Radius = 1;
	[SerializeField] LayerMask ProbeLayer;

	protected override void Use(Vector3 pDirection)
	{
		//Debug.Log("USE");
		particles.enableEmission = true;

		DoInTime(() =>
		{
			particles.enableEmission = false;
		}, Config.Cooldown);

		DecreaseAmmo();

		var overlaps = Physics.OverlapSphere(transform.position, Radius, ProbeLayer);
		foreach(var overlap in overlaps)
		{
			overlap.transform.GetComponent<IDamagable>()?.OnHit(Damage);
		}
	}

}
