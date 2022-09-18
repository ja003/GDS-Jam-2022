using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : PlayerBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		if(game.IsInGame)
			return;

		Vector3 playerViewportPos = Camera.main.WorldToViewportPoint(transform.position);
		float mouseX = (Input.mousePosition.x / Screen.width);
		float mouseY = (Input.mousePosition.y / Screen.height);
		Vector3 dir = (new Vector3(mouseX, mouseY, playerViewportPos.z) - playerViewportPos).normalized;

		//Debug.Log(dir.normalized);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			Player.WeaponController.UseHumanAbductor();
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			Player.WeaponController.StopUseHumanAbductor();
		}
		//if(Input.GetKeyDown(KeyCode.E))
		//{
		//	Player.WeaponController.SetNextWeaponActive();
		//}

		if(Input.GetMouseButtonDown(0))
		{
			Player.WeaponController.UseEMPRocket(dir);
		}
		if(Input.GetMouseButtonDown(1))
		{
			Player.WeaponController.UseMeleeWeapon();
		}

		// debug
		if(Input.GetKeyDown(KeyCode.Q))
		{
			Player.Stats.AddXP(1);
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			Player.Stats.AddDetection(25);
		}
	}
}
