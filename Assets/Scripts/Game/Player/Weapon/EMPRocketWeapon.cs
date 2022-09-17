using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPRocketWeapon : WeaponBase
{
	[SerializeField] GameObject ProjectilePrefab;
	[SerializeField] float Power = 10;
	[SerializeField] int Damage = 1;

	[SerializeField] float ProjectileSpawnOffset;

	protected override void Use(Vector3 pDirection)
	{
		Debug.Log("USE rocket");
		var projectileInst = Instantiate(ProjectilePrefab, transform.position + pDirection.normalized * ProjectileSpawnOffset, Quaternion.identity);
		projectileInst.GetComponent<Projectile>().Shoot(pDirection.normalized * Power, Damage);
		projectileInst.transform.SetParent(game.ProjectilesHolder);

		DecreaseAmmo();
	}

}
