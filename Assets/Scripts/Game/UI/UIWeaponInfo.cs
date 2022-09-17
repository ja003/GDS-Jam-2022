using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : GameBehaviour
{
	[SerializeField] TextMeshProUGUI Name;
	[SerializeField] TextMeshProUGUI Ammo;
	[SerializeField] Image Icon;
	[SerializeField] Image Reload;

	internal void Init(WeaponConfig pConfig)
	{
		Name.text = pConfig.Name;
		Icon.sprite = pConfig.Icon;
		SetReloading(false);
	}

	internal void SetReloading(bool pValue)
	{
		Reload.enabled = pValue;
	}

	internal void SetAvailable(bool pValue)
	{
		//Debug.Log("SetAvailable " + pValue);
		SetAlpha(pValue ? 1 : 0.1f);
	}

	internal void Refresh(WeaponBase pWeapon)
	{
		if(!pWeapon.Config.HasInfinityAmmo && pWeapon.TotalAmmo < 0)
		{
			Debug.LogError("bad ammo");
		}
		Ammo.text = pWeapon.Config.HasInfinityAmmo ? "" : pWeapon.TotalAmmo.ToString();
        SetAvailable(pWeapon.HasAmmo());

    }

}
