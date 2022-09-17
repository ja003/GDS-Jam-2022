using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPPulseWeapon : WeaponBase
{
	[SerializeField] ParticleSystem particles;
	protected override void Use(Vector3 pDirection)
	{
		Debug.Log("USE");
		particles.enableEmission = true;

		DoInTime(() =>
		{
			particles.enableEmission = false;
		}, Cooldown);

		DecreaseAmmo();
	}

}
