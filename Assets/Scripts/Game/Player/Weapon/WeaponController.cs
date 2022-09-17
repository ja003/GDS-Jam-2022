using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : GameBehaviour
{
	[SerializeField] List<WeaponBase> WeaponPrefabs;

	List<WeaponBase> Weapons = new List<WeaponBase>();

	public int ActiveRangeWeapon = 1;


	private void Awake()
	{
		for(int i = 0; i < WeaponPrefabs.Count; i++)
		{
			WeaponBase weaponPrefab = WeaponPrefabs[i];
			var newInst = Instantiate(weaponPrefab.gameObject, transform);
			Debug.Log($"Spawn {newInst.ToString()}");
			WeaponBase newWeapon = newInst.GetComponent<WeaponBase>();
			Weapons.Add(newWeapon);

			newWeapon.Init(game.HUD.WeaponInfos[i]);
		}
	}

	public void UseMeleeWeapon()
	{
		if(Weapons.Count == 0)
		{
			Debug.LogError("No weapons");
			return;
		}
		Weapons[0].TryUse(Vector3.zero);
	}

	internal void SetNextWeaponActive()
	{
		ActiveRangeWeapon++;
		if(ActiveRangeWeapon >= Weapons.Count || !Weapons[ActiveRangeWeapon].IsUnlocked)
			ActiveRangeWeapon = 1;
		Debug.Log($"Active weapon = {ActiveRangeWeapon} = {Weapons[ActiveRangeWeapon]}");
	}

	public void UseRangeWeapon(Vector3 pDirection)
	{
		if(Weapons.Count <= 1)
		{
			Debug.LogError("No ranged weapons");
			return;
		}
		Weapons[ActiveRangeWeapon].TryUse(pDirection);
	}

	internal void TryUnlockWeapon(int pXP)
	{
		foreach(var weapon in Weapons)
		{
			if(weapon.IsUnlocked) continue;
			weapon.TryUnlock(pXP);
		}
	}
}
