using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : GameBehaviour
{
	public WeaponConfig Config;
	public int AmmoPerMagazine;
	public int Magazines;
	public int Ammo { get; private set; }
	public int Cooldown;
	UIWeaponInfo UI;
	public bool IsUnlocked;
	[SerializeField] int XPRequired;
	public bool HasInfinityAmmo;

	bool IsSelected;

	public void Init()
	{
		Ammo = AmmoPerMagazine;
		UI = game.HUD.Weapon.CreateWeaponInfoUI(Config);
		UI.gameObject.SetActive(IsUnlocked);
		UI.Refresh(this);
		SetSelected(false);
	}

	public void TryUse(Vector3 pDirection)
	{
		if(!IsSelected)
		{
			Debug.LogError("Not selected!");
			return;
		}

		if(!HasAmmo())
		{
			Debug.Log("No ammmo");
			return;
		}
		Use(pDirection);
	}

	public bool HasAmmo()
	{
		return Ammo > 0 || HasInfinityAmmo;
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

	internal void SetSelected(bool pValue)
	{
		IsSelected = true;
		UI.SetSelected(pValue);
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
