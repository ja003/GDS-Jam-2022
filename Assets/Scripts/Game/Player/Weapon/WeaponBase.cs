using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : GameBehaviour
{
	public WeaponConfig Config;
	public int LeftInMagazine { get; private set; }
	public int TotalAmmo { get; private set; }

	UIWeaponInfo UI;
	public bool IsUnlocked;

	bool IsReloading;

	public void Init()
	{
		LeftInMagazine = Config.AmmoPerMagazine;
        TotalAmmo = Config.TotalAmmo;
        
        UI = game.HUD.Weapon.CreateWeaponInfoUI(Config);
		UI.gameObject.SetActive(IsUnlocked);
		UI.Refresh(this);
	}

	public void TryUse(Vector3 pDirection = new Vector3())
	{
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
		return LeftInMagazine > 0 || Config.HasInfinityAmmo;
	}

	protected abstract void Use(Vector3 pDirection);

	protected void DecreaseAmmo()
	{
		TotalAmmo--;
		LeftInMagazine--;
		UI.Refresh(this);
		//Debug.Log("DecreaseAmmo " + Ammo);

		if (TotalAmmo <= 0 || LeftInMagazine <= 0)
		{
			//even infinity ammo weapon has to reaload
			if(TotalAmmo > 0 || Config.HasInfinityAmmo)
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

	public void AddAmmo(int pValue)
	{
		TotalAmmo += pValue;
        UI.Refresh(this);
		if (LeftInMagazine <= 0)
			Reload();
    }


	void Reload()
	{
		LeftInMagazine = Math.Min(Config.AmmoPerMagazine, TotalAmmo);
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
