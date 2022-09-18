using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPPulseWeapon : WeaponBase
{
	[SerializeField] ParticleSystem particles;
	[SerializeField] int Damage = 1;
	[SerializeField] int Radius = 1;
	[SerializeField] LayerMask ProbeLayer;

	AudioSource audioPlayer;
    private void Start()
    {
		audioPlayer = GetComponent<AudioSource>();
    }

	private bool isInUse = false;

    protected override void Use(Vector3 pDirection)
	{
		//Debug.Log("USE");
		particles.enableEmission = true;
		isInUse = true;
		audioPlayer?.Play();


		DoInTime(() =>
		{
			isInUse = false;
			particles.enableEmission = false;
			audioPlayer?.StopAndReset();
		}, Config.AnimationDuration);

		DecreaseAmmo();
		DamageNearby();
	}

	private void DamageNearby()
	{
		var overlaps = Physics.OverlapSphere(transform.position, Radius, ProbeLayer);
		foreach (var overlap in overlaps)
		{
			overlap.transform.GetComponent<IDamagable>()?.OnHit(Damage);
		}
	}

}
