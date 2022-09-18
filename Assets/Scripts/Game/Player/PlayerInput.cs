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
		if(!game.IsInGame || Time.timeScale < 0.1f)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(-Vector3.forward, Vector3.zero);
		float dist;
		plane.Raycast(ray, out dist);
		Vector3 dir = ray.GetPoint(dist) - transform.position;
		
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

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			game.PauseGame();
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
