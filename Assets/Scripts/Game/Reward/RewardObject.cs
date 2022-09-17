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

    private void Awake()
    {
        if (Amount < 0)
            Debug.LogError("Bad Amount");
    }
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
