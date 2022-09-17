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

	internal void Init(WeaponConfig pConfig)
	{
		Name.text = pConfig.Name;
		Icon.sprite = pConfig.Icon;
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
		Ammo.text = "reloading";
	}

	internal void SetAvailable(bool pValue)
	{
		Debug.Log("SetAvailable " + pValue);
		SetAlpha(pValue ? 1 : 0.1f);
	}

	internal void Refresh(WeaponBase pWeapon)
	{
		Ammo.text = pWeapon.Ammo.ToString();
		Magazines.text = pWeapon.Magazines.ToString();
	}

	internal void SetSelected(bool pValue)
	{
		if(Selector != null)
			Selector.enabled = pValue;
	}
}
