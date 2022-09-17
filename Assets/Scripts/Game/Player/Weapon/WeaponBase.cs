using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : GameBehaviour
{
	public int AmmoPerMagazine;
	public int Magazines;
	public int Ammo { get; private set; }
	public int Cooldown;
	UIWeaponInfo UI;
	public bool IsUnlocked;
	[SerializeField] int XPRequired;
	public bool HasInfinityAmmo;

	public void Init(UIWeaponInfo pUIWeaponInfo)
	{
		Ammo = AmmoPerMagazine;
		UI = pUIWeaponInfo;
		UI.gameObject.SetActive(IsUnlocked);
		UI.Refresh(this);
	}

	public void TryUse(Vector3 pDirection)
	{
		if(Ammo <= 0 && !HasInfinityAmmo)
		{
			Debug.Log("No ammmo");
			return;
		}
		Use(pDirection);
	}

	protected abstract void Use(Vector3 pDirection);

	protected void DecreaseAmmo()
	{
		Ammo--;
		UI.Refresh(this);
		Debug.Log("DecreaseAmmo " + Ammo);

		if (Ammo <= 0)
		{
			if(Magazines > 0)
			{
				UI.SetReloading(true);
				DoInTime(Reload, Cooldown);
			}
			else
			{
				UI.SetAvailable(false);
			}
		}
	}

	void Reload()
	{
		Ammo = AmmoPerMagazine;
		Magazines--;
		UI.Refresh(this);
	}

	internal void TryUnlock(int pXP)
	{
		if(pXP >= XPRequired)
		{
			IsUnlocked = true;
			UI.Refresh(this);

			UI.gameObject.SetActive(IsUnlocked);
		}
	}
}
