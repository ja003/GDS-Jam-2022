using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public WeaponController WeaponController;
	public PlayerStats Stats;

    internal void AddReward(RewardObject pReward)
    {
		switch (pReward.Type)
		{
			case EReward.XP:
				Stats.AddXP(pReward.Amount);
				break;
			case EReward.Ammo:
				WeaponController.AddAmmo(pReward.Amount);
				break;
			default:
				break;
		}
	}

    private void OnCollisionEnter(Collision collision)
	{
		var probe = collision.gameObject.GetComponent<Probe>();
		if (probe != null)
		{
			probe.OnHit(666);
			Stats.AddDetection(10);
		}

        RewardObject rewardObject = collision.gameObject.GetComponent<RewardObject>();
        if (rewardObject != null)
        {
            rewardObject.Take();
        }
    }

}
