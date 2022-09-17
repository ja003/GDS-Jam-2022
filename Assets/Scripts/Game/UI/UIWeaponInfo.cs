using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI ammo;
	[SerializeField] TextMeshProUGUI magazines;

	public void SetAmmo(int pValue)
	{
		ammo.text = pValue.ToString();
	}

	public void SetMagazines(int pValue)
	{
		magazines.text = pValue.ToString();
	}

}
