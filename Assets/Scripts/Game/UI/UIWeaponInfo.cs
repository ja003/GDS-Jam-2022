using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : GameBehaviour
{
	[SerializeField] TextMeshProUGUI Ammo;
	CanvasGroup canvasGroup;

	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	internal void Init(WeaponConfig pConfig)
	{
		SetReloading(false);
	}

	//protected override void SetAlpha(float pAlpha)
	//{
	//	canvasGroup.alpha = pAlpha;
	//}

	internal void SetReloading(bool pValue, float pCooldown = 0)
	{
		if(!pValue)
		{
			SetAlpha(1, .01f, null);
		}
		else
		{
			SetAlpha(0, .1f, () => { SetAlpha(1, pCooldown, null); });
			//SetAlpha(1, pCooldown, null);
		}
	}

	internal void SetAvailable(bool pValue)
	{
		//Debug.Log("SetAvailable " + pValue);
//		SetAlpha(pValue ? 1 : 0.1f);
	}

	internal void Refresh(WeaponBase pWeapon)
	{
		if(!pWeapon.Config.HasInfinityAmmo)
			Ammo.text = pWeapon.TotalAmmo.ToString();

        SetAvailable(pWeapon.HasAmmo());

    }

}
