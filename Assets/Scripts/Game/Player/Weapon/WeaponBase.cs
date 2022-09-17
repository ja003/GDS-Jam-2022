using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
	public int Ammo;
	public int Cooldown;

	public abstract void Use();
}
