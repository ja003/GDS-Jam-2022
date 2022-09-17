using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : GameBehaviour
{
	public int Ammo;
	public int Cooldown;

	public abstract void Use(Vector3 pDirection);
}
