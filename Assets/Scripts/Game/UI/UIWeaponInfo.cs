using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : GameBehaviour
{
	[SerializeField] TextMeshProUGUI ammo;
	[SerializeField] TextMeshProUGUI magazines;

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
		ammo.text = "reloading";
	}

	internal void SetAvailable(bool pValue)
	{
		Debug.Log("SetAvailable " + pValue);
		SetAlpha(pValue ? 1 : 0.1f);
	}

	internal void Refresh(WeaponBase pWeapon)
	{
		ammo.text = pWeapon.Ammo.ToString();
		magazines.text = pWeapon.Magazines.ToString();
	}
}
