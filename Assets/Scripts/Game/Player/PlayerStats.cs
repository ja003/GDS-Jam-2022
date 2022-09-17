using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : PlayerBehaviour
{
	public int DetectionMeter;
	public int XP;

	private void Awake()
	{
		AddXP(0);
	}

	public void AddXP(int pValue)
	{
		XP += pValue;
		game.HUD.SetXP(XP);
		Player.WeaponController.TryUnlockWeapon(XP);
	}
}
