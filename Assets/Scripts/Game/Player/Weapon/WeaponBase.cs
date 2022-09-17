using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : GameBehaviour
{
	public WeaponConfig Config;
	public int Ammo { get; private set; }
	public int Magazines { get; private set; }

	UIWeaponInfo UI;
	public bool IsUnlocked;

	bool IsSelected;
	bool IsReloading;

	public void Init()
	{
		Ammo = Config.AmmoPerMagazine;
		UI = game.HUD.Weapon.CreateWeaponInfoUI(Config);
		UI.gameObject.SetActive(IsUnlocked);
		UI.Refresh(this);
		SetSelected(false);
	}

	public void TryUse(Vector3 pDirection = new Vector3())
	{
		if(!IsSelected)
		{
			Debug.LogError("Not selected!");
			return;
		}

		if(IsReloading)
		{
			//Debug.Log("reloading");
			return;
		}

		if(!HasAmmo())
		{
			//Debug.Log("No ammmo");
			return;
		}
		Use(pDirection);
	}

	public bool HasAmmo()
	{
		return Ammo > 0 || Config.HasInfinityAmmo;
	}

	protected abstract void Use(Vector3 pDirection);

	protected void DecreaseAmmo()
	{
		Ammo--;
		UI.Refresh(this);
		//Debug.Log("DecreaseAmmo " + Ammo);

		if (Ammo <= 0)
		{
			//even infinity ammo weapon has to reaload
			if(Magazines > 0 || Config.HasInfinityAmmo)
			{
				UI.SetReloading(true);
				IsReloading = true;
				DoInTime(Reload, Config.Cooldown);
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
		Ammo = Config.AmmoPerMagazine;
		Magazines--;
		UI.Refresh(this);
		UI.SetReloading(false);
		IsReloading = false;
	}

	internal void TryUnlock(int pXP)
	{
		if(pXP >=  Config.XPRequired)
		{
			IsUnlocked = true;
			UI.Refresh(this);

			UI.gameObject.SetActive(IsUnlocked);
		}
	}
}
