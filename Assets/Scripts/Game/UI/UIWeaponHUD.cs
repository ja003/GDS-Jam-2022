using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponHUD : MonoBehaviour
{
	[SerializeField] UIWeaponInfo weaponInfoPrefab;

	public List<UIWeaponInfo> WeaponInfos;

	internal UIWeaponInfo CreateWeaponInfoUI(WeaponConfig pWeaponConfig)
	{
		UIWeaponInfo newWeaponInfo = Instantiate(weaponInfoPrefab, transform);
		newWeaponInfo.Init(pWeaponConfig);
		return newWeaponInfo;
	}
}
