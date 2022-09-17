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
	[SerializeField] TextMeshProUGUI Magazines;
	[SerializeField] Image Icon;
	[SerializeField] Image Selector;
	[SerializeField] Image Reload;

	internal void Init(WeaponConfig pConfig)
	{
		Name.text = pConfig.Name;
		Icon.sprite = pConfig.Icon;
		SetReloading(false);
	}

	//public void SetAmmo(int pValue)
	//{
	//	ammo.text = pValue.ToString();
	//}

	//public void SetMagazines(int pValue)
	//{
	//	magazines.text = pValue.ToString();
	//}

	internal void SetReloading(bool pValue)
	{
		Reload.enabled = pValue;
	}

	internal void SetAvailable(bool pValue)
	{
		Debug.Log("SetAvailable " + pValue);
		SetAlpha(pValue ? 1 : 0.1f);
	}

	internal void Refresh(WeaponBase pWeapon)
	{
		Ammo.text = pWeapon.HasInfinityAmmo ? "" : pWeapon.Ammo.ToString();
		Magazines.text = pWeapon.HasInfinityAmmo ? "" : pWeapon.Magazines.ToString();
	}

	internal void SetSelected(bool pValue)
	{
		if(Selector != null)
			Selector.enabled = pValue;
	}
}
