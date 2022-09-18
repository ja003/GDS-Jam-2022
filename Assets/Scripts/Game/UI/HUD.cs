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

	internal void Init()
	{
		SetXP(0);
		SetDetectionMeter(0);
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

	internal void Hide()
	{
		animator.Play("A_HUD_Hide");
	}

	internal void Show()
	{
		animator.Play("A_HUD_Show");
	}
}
