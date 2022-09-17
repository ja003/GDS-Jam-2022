using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public WeaponController WeaponController;
	public PlayerStats Stats;


	private void OnCollisionEnter(Collision collision)
	{
		var probe = collision.gameObject.GetComponent<BasicProbe>();
		if (probe == null)
			return;

		probe.OnHit(666);
		Stats.AddDetection(10);
	}

}
