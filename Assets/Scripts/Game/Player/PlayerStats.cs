using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : PlayerBehaviour
{
	public float DetectionMeter;
	public int XP;

	public float[] XPForNextStage;
	public SoundtrackManager SoundtrackManager;

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
		TryProgressStage();
	}

	private void TryProgressStage()
    {
		while(XPForNextStage.Length > 0 && XP >= XPForNextStage[0])
        {
			SoundtrackManager?.AdvanceToNextStage();
			XPForNextStage = XPForNextStage[1..];
        }
    }

	public void AddDetection(float pValue)
	{
		DetectionMeter += pValue;
		DetectionMeter = Mathf.Clamp(DetectionMeter, 0, 100);

		game.HUD.SetDetectionMeter(DetectionMeter);

		if(DetectionMeter >= 100f)
		{
			game.EndGame();
		}

    }

}