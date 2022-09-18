using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : GameBehaviour
{
	public UIWeaponHUD Weapon;

	[SerializeField] TextMeshProUGUI XP;
	[SerializeField] Slider DetectionMeter;
	[SerializeField] public PauseMenu PauseMenu;


	internal void Init()
	{
		SetXP(0);
		SetDetectionMeter(0);
		PauseMenu.gameObject.SetActive(false);
	}

	internal void SetXP(int pValue)
	{
		XP.text = pValue.ToString();
	}

	internal void SetDetectionMeter(float pValue)
	{
		DetectionMeter.value = pValue / 100f;
	}

	internal void OnEndGame()
	{
		gameObject.SetActive(false);
	}

}
