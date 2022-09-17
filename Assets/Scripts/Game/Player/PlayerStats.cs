using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : PlayerBehaviour
{
	public float DetectionMeter;
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
		game.Earth.OnXPChanged(XP);

	}

	public void AddDetection(float pValue)
	{
		DetectionMeter += pValue;
        game.HUD.SetDetectionMeter(DetectionMeter);

		if(DetectionMeter >= 100f)
		{
			game.EndGame();
		}

    }

}