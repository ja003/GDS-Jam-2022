using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]

public class WeaponConfig : ScriptableObject
{
	public EWeapon ID;
	public Sprite Icon;
	public string Name;
	public int TotalAmmo;
	public int AmmoPerMagazine;
	public int Cooldown;
	public float AnimationDuration;
	public bool HasInfinityAmmo;
	public int XPRequired;
}
