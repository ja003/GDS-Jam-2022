using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : GameBehaviour
{
	[SerializeField] EMPPulseWeapon EMPPulse;
	[SerializeField] EMPRocketWeapon EMPRocket;
	[SerializeField] HumanAbductorWeapon HumanAbductor;

	private void Awake()
	{
		EMPPulse.Init();
        EMPRocket.Init();
        HumanAbductor.Init();
	}

	public void UseMeleeWeapon()
	{
        EMPPulse.TryUse(Vector3.zero);
	}

	public void UseEMPRocket(Vector3 pDirection)
	{
        EMPRocket.TryUse(pDirection);
	}

    public void UseHumanAbductor()
    {
        HumanAbductor.TryUse();
    }

    internal void TryUnlockWeapon(int pXP)
	{
		HumanAbductor.TryUnlock(pXP);

    }

	internal void StopUseHumanAbductor()
	{
		HumanAbductor.StopUse();

    }

	internal void AddAmmo(int amount)
	{
		EMPRocket.AddAmmo(amount);
    }
}
