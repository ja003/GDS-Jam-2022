using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
	public UIWeaponHUD Weapon;

	[SerializeField] TextMeshProUGUI XP;
	[SerializeField] TextMeshProUGUI DetectionMeter;

	internal void SetXP(int pValue)
	{
		XP.text = pValue + " XP";
	}

	internal void SetDetectionMeter(int pValue)
	{
		DetectionMeter.text = pValue + "/10";
	}
}
