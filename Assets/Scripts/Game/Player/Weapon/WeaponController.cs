using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : GameBehaviour
{
	[SerializeField] List<WeaponBase> WeaponPrefabs;

	//1st weapon is always melee and is always active
	List<WeaponBase> Weapons = new List<WeaponBase>();

	WeaponBase SelectedRangeWeapon => Weapons[SelectedRangeWeaponIndex];

	public int SelectedRangeWeaponIndex = 1;


	private void Awake()
	{
		for(int i = 0; i < WeaponPrefabs.Count; i++)
		{
			WeaponBase weaponPrefab = WeaponPrefabs[i];
			var newInst = Instantiate(weaponPrefab.gameObject, transform);
			Debug.Log($"Spawn {newInst.ToString()}");
			WeaponBase newWeapon = newInst.GetComponent<WeaponBase>();
			Weapons.Add(newWeapon);

			newWeapon.Init();
		}

		SelectedRangeWeapon.SetSelected(true);
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
		SelectedRangeWeapon.SetSelected(false);
		SelectedRangeWeaponIndex++;
		if(SelectedRangeWeaponIndex >= Weapons.Count)
			SelectedRangeWeaponIndex = 1;

		bool hasSelected = false;
		for(int i = SelectedRangeWeaponIndex; i < Weapons.Count; i++)
		{
			WeaponBase weapon = Weapons[i];
			SelectedRangeWeaponIndex = i;
			if(weapon.HasAmmo() && weapon.IsUnlocked)
			{
				hasSelected = true;
				break;
			}
		}
		if(!hasSelected)
			SelectedRangeWeaponIndex = 1;

		Debug.Log($"Active weapon = {SelectedRangeWeaponIndex} = {SelectedRangeWeapon}");

		SelectedRangeWeapon.SetSelected(true);

	}

	public void UseRangeWeapon(Vector3 pDirection)
	{
		if(Weapons.Count <= 1)
		{
			Debug.LogError("No ranged weapons");
			return;
		}
		SelectedRangeWeapon.TryUse(pDirection);
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
