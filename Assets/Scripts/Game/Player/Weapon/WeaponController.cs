using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[SerializeField] List<WeaponBase> WeaponPrefabs;

	List<WeaponBase> Weapons = new List<WeaponBase>();

	public int ActiveRangeWeapon = 1;

	private void Awake()
	{
		foreach(var weaponPrefab in WeaponPrefabs)
		{
			var newInst = Instantiate(weaponPrefab.gameObject, transform);
			Debug.Log($"Spawn {newInst.ToString()}");
			Weapons.Add(newInst.GetComponent<WeaponBase>());
		}
	}

	public void UseMeleeWeapon()
	{
		if(Weapons.Count == 0)
		{
			Debug.LogError("No weapons");
			return;
		}
		Weapons[0].Use(Vector3.zero);
	}

	public void UseRangeWeapon(Vector3 pDirection)
	{
		if(Weapons.Count <= 1)
		{
			Debug.LogError("No ranged weapons");
			return;
		}
		Weapons[ActiveRangeWeapon].Use(pDirection);
	}
}
