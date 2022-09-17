using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : GameBehaviour
{
	public int Ammo;
	public int Cooldown;
	UIWeaponInfo UI;
	public bool IsUnlocked;

	public abstract void Use(Vector3 pDirection);
	
	protected void DecreaseAmmo()
	{
		Ammo--;
		UI.SetAmmo(Ammo);
	}

	internal void SetUI(UIWeaponInfo pUIWeaponInfo)
	{
		UI = pUIWeaponInfo;
		UI.gameObject.SetActive(IsUnlocked);
	}

	public void SetUnlocked()
	{
		IsUnlocked = true;
		UI.gameObject.SetActive(IsUnlocked);
	}
}
