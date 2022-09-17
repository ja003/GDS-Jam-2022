using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EReward
{
    XP,
    Ammo
}

public class RewardObject : GameBehaviour, IDamagable
{
    public int Amount;
    public EReward Type;

    public void OnHit(int pDamage)
    {
        Destroy(gameObject);

    }

    internal void Take()
    {
        game.Player.AddReward(this);

        Destroy(gameObject);

    }
}
